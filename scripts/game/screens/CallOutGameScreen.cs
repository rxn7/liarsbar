using Godot;

[GlobalClass]
public partial class CallOutGameScreen : GameScreen {
	private static readonly PackedScene PLAYER_ENTRY = GD.Load<PackedScene>("res://scenes/game/player_entry.tscn");

	private Button m_goBackButton;
	private Container m_playerContainer;
	private Label m_calledOutPlayerLabel;

	public override void _Ready() {
		m_calledOutPlayerLabel = GetNode<Label>("CalledOutLabel");
		m_goBackButton = GetNode<Button>("GoBackButton");

		m_goBackButton.Pressed += () => {
			m_game.SetScreen(m_game.ActionScreen);
		};

		m_playerContainer = GetNode<Container>("PlayerContainer");
	}

	public override void Enter() {
		m_calledOutPlayerLabel.Text = $"Kto sprawdza gracza {GameContext.CurrentPlayer.Name}?";

		foreach(Control c in m_playerContainer.GetChildren()) {
			c.Free();
		}

		foreach(Player p in GameContext.Players) {
			if(p == GameContext.CurrentPlayer || p.IsDead) {
				continue;
			}

			Button playerButton = PLAYER_ENTRY.Instantiate<Button>();
			playerButton.Text = p.Name;

			playerButton.Pressed += () => {
				m_game.CallOutResultScreen.CallingOutPlayer = p;
				m_game.SetScreen(m_game.CallOutResultScreen);
			};

			m_playerContainer.AddChild(playerButton);
		}
	}
}
