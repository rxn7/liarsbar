using Godot;

[GlobalClass]
public partial class CallOutResultGameScreen : GameScreen {
	public Player CallingOutPlayer { get; set; }
	
	private Button m_lieButton;
	private Button m_truthButton;
	private Label m_callingPlayerLabel;
	private Label m_calledPlayerLabel;

	public override void _Ready() {
		m_callingPlayerLabel = GetNode<Label>("%CallingPlayerLabel");
		m_calledPlayerLabel = GetNode<Label>("%CalledPlayerLabel");

		m_lieButton = GetNode<Button>("%LieButton");
		m_lieButton.Pressed += () => EnterRevolverScreen(GameContext.CurrentPlayer);

		m_truthButton = GetNode<Button>("%TruthButton");
		m_truthButton.Pressed += () => EnterRevolverScreen(CallingOutPlayer);
	}

	public override void Enter() {
		m_callingPlayerLabel.Text = $"Sprawdza: {CallingOutPlayer.Name}";
		m_calledPlayerLabel.Text = $"Czy gracz {GameContext.CurrentPlayer.Name} mówił prawdę?";
	}

	private void EnterRevolverScreen(Player shooter) {
		m_game.RevolverScreen.ShootingPlayer = shooter;
		m_game.SetScreen(m_game.RevolverScreen);
	}
}
