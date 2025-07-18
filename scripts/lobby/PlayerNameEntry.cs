using Godot;

public partial class PlayerNameEntry : LineEdit {
	public override void _Ready() {
	}

	public override void _GuiInput(InputEvent ev) {
		if(ev is InputEventMouseButton mouseEvent) {
			if(mouseEvent.ButtonIndex == MouseButton.Right) {
				QueueFree();
			}
		}
	}
}
