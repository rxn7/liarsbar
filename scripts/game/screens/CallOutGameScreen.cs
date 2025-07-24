using Godot;

[GlobalClass]
public partial class CallOutGameScreen : GameScreen {
	public Player CalledOutPlayer { get; private set; }
	private Button m_lieButton;
	private Button m_truthButton;
	private Label m_callingPlayerLabel;
	private Label m_calledPlayerLabel;

	public override void _Ready() {
		m_callingPlayerLabel = GetNode<Label>("%CallingPlayerLabel");
		m_calledPlayerLabel = GetNode<Label>("%CalledPlayerLabel");

		m_lieButton = GetNode<Button>("%LieButton");
		m_lieButton.Pressed += () => EnterRevolverScreen(CalledOutPlayer);

		m_truthButton = GetNode<Button>("%TruthButton");
		m_truthButton.Pressed += () => EnterRevolverScreen(GameContext.CurrentPlayer);
	}

	public override void Enter() {
		CalledOutPlayer = GameContext.GetPreviousPlayer();

		m_callingPlayerLabel.Text = $"Sprawdza: {GameContext.CurrentPlayer.Name}";
		m_calledPlayerLabel.Text = $"Czy gracz {CalledOutPlayer.Name} mówił prawdę?";
	}

	private void EnterRevolverScreen(Player shooter) {
		m_game.RevolverScreen.ShootingPlayer = shooter;
		m_game.SetScreen(m_game.RevolverScreen);
	}
}
