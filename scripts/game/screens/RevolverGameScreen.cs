using Godot;

[GlobalClass]
public partial class RevolverGameScreen : GameScreen {
	public Player ShootingPlayer { get; set; } = null;

	private RevolverCylinder m_cylinder;
	private TextureButton m_revolverButton;
	private Label m_chancesLabel;
	private Label m_shootingPlayerLabel;

	public override void _Ready() {
		m_chancesLabel = GetNode<Label>("ChancesLabel");
		m_shootingPlayerLabel = GetNode<Label>("ShootingPlayerLabel");
		m_cylinder = GetNode<RevolverCylinder>("Cylinder");

		m_revolverButton = GetNode<TextureButton>("Revolver");
		m_revolverButton.Pressed += Fire;
	}

	public override void Enter() {
		if(ShootingPlayer == null) {
			GD.PushError("Shooting player idx is -1");
			return;
		}

		m_revolverButton.Disabled = false;

		m_shootingPlayerLabel.Text = $"Strzela: {ShootingPlayer.Name}";
		UpdateCylinder();
	}

	public override void Exit() {
		ShootingPlayer = null;
	}

	private void Fire() {
		m_revolverButton.Disabled = true;

		ShootingPlayer.Fire();
		UpdateCylinder();

		SceneTreeTimer timer = GetTree().CreateTimer(2f);
		timer.Timeout += () => {
			GameContext.NextPlayer();
			m_game.SetScreen(m_game.ActionScreen);
		};
	}

	private void UpdateCylinder() {
		m_cylinder.UpdateForPlayer(ShootingPlayer);
		m_chancesLabel.Text = ShootingPlayer.FireChanceText();
	}
}
