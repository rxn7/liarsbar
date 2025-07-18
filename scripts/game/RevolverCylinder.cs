using Godot;

public partial class RevolverCylinder : Control {
	private Control m_slotContainer;

	public override void _Ready() {
		m_slotContainer = GetNode<Control>("SlotContainer");
	}

	public void UpdateForPlayer(Player player) {
		for(int i=0; i<player.ShootCount; ++i) {
			GetSlot(i).Visible = false;
		}

		for(int i=player.ShootCount; i<GameContext.CHAMBER_COUNT; ++i) {
			GetSlot(i).Visible = true;
		}
	}

	private Control GetSlot(int idx) {
		return m_slotContainer.GetChild<Control>(idx);
	}
}
