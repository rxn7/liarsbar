using Godot;

public partial class GameScreen : Control {
	protected GameManager m_game;

	public void Init(GameManager game) {
		m_game = game;
	}

	public virtual void Enter() {}
	public virtual void Exit() {}
}
