using Godot;

public partial class GameManager : Node {
	[Export] public ActionGameScreen ActionScreen { get; private set; }
	[Export] public CallOutGameScreen CallOutScreen { get; private set; }
	[Export] public RevolverGameScreen RevolverScreen { get; private set; }

	private Control m_screenContainer;

	public override void _Ready() {
		m_screenContainer = GetNode<Control>("ScreenContainer");

		foreach(GameScreen s in m_screenContainer.GetChildren()) {
			s.Init(this);
		}

		GameContext.OnPlayerWon += (Player winner) => {
			OS.Alert("Wygrał gracz " + winner.Name);
			GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
		};

		SetScreen(ActionScreen);
	}

	public void SetScreen(GameScreen screen) {
		foreach(GameScreen s in m_screenContainer.GetChildren()) {
			if(s.Visible) {
				s.Exit();
			}
			s.Visible = false;
		}

		screen.Visible = true;
		screen.Enter();
	}
}
