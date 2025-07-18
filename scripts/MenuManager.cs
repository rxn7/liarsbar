using Godot;

public partial class MenuManager : Node {
	private Control m_screensContainer;
	[Export] private Control m_mainMenuScreen;
	[Export] private Control m_playerSelectScreen;

	public override void _Ready() {
		m_screensContainer = GetNode<Control>("%Screens");
		m_mainMenuScreen = m_screensContainer.GetNode<Control>("MainMenu");
		m_playerSelectScreen = m_screensContainer.GetNode<Control>("PlayerSelect");
	}

	private void EnterPlayerSelectScreen() {
		m_mainMenuScreen.Visible = false;
	}
}
