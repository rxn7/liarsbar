using Godot;

public partial class LobbyManager : Node {
	private static readonly PackedScene GAME_SCENE = GD.Load<PackedScene>("res://scenes/game.tscn");

	[Export] private int m_maxPlayers = 4;
	[Export] private PackedScene m_playerNameEntryScene;

	private FlowContainer m_playerNameContainer;
	private Button m_addPlayerButton;
	private Button m_startGameButton;

	public int PlayerCount => m_playerNameContainer.GetChildren().Count;

	public override void _Ready() {
		GameContext.Reset();

		m_playerNameContainer = GetNode<FlowContainer>("%PlayerNameContainer");

		m_startGameButton = GetNode<Button>("%StartGameButton");
		m_startGameButton.Pressed += StartGame;

		m_addPlayerButton = GetNode<Button>("%AddPlayerButton");
		m_addPlayerButton.Pressed += AddPlayer;
	}
	
	public override void _UnhandledInput(InputEvent ev) {
		if(ev is InputEventKey keyEvent && keyEvent.Keycode == Key.F1 && keyEvent.Pressed && !keyEvent.Echo) {
			GameContext.Reset();

			GameContext.AddPlayer("Player1");
			GameContext.AddPlayer("Player2");
			GameContext.AddPlayer("Player3");
			GameContext.AddPlayer("Player4");

			GetTree().ChangeSceneToPacked(GAME_SCENE);
			GameContext.StartNewRound();
		}
	}

	public override void _PhysicsProcess(double delta) {
		m_addPlayerButton.Disabled = PlayerCount >= m_maxPlayers;
	}

	private void AddPlayer() {
		if(PlayerCount >= m_maxPlayers)
			return;

		LineEdit playerNameEntry = m_playerNameEntryScene.Instantiate<LineEdit>();
		m_playerNameContainer.AddChild(playerNameEntry);

		playerNameEntry.GrabFocus();
	}

	private void StartGame() {
		GameContext.Reset();

		if(PlayerCount < 2) {
			OS.Alert("Przynajmniej 2 graczy musi być w grze");
			return;
		}

		foreach(PlayerNameEntry entry in m_playerNameContainer.GetChildren()) {
			if(entry.Text.Length == 0) {
				GameContext.Players.Clear();
				OS.Alert("Nie wszyscy gracze podali swoje imie");
				return;
			}

			GameContext.AddPlayer(entry.Text);
		}

		GetTree().ChangeSceneToPacked(GAME_SCENE);
		GameContext.StartNewRound();
	}
}
