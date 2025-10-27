using Godot;

[GlobalClass]
public partial class ActionGameScreen : GameScreen {
	private Button m_continueButton;
	private Button m_callOutButton;
	private Label m_playerTurnLabel;

	public override void _Ready() {
		m_playerTurnLabel = GetNode<Label>("PlayerTurnLabel");
		m_callOutButton = GetNode<Button>("%CallOutButton");
		m_continueButton = GetNode<Button>("%ContinueButton");

		m_callOutButton.Pressed += () => {
			m_game.SetScreen(m_game.CallOutScreen);
		};

		m_continueButton.Pressed += () => {
			GameContext.NextAlivePlayer();
			m_game.SetScreen(this);
		};
	}

	public override void Enter() {
		m_playerTurnLabel.Text = $"Kolej: {GameContext.CurrentPlayer.Name}";

		if(GameContext.IsFirstTurnOfRound) {
			m_callOutButton.Disabled = true;
			m_callOutButton.Text = $"";
			m_callOutButton.Visible = false;
		} else {
			m_callOutButton.Disabled = false;
			m_callOutButton.Text = $"Sprawdź gracza {GameContext.GetPreviousAlivePlayer().Name}";
			m_callOutButton.Visible = true;
		}
	}
}
