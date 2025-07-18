using Godot;

[GlobalClass]
public partial class NetworkManager : Node {
	private string m_ipAddress = "";

	public override void _Ready() {
		switch(OS.GetName()) {
		case "Android":
			m_ipAddress = IP.GetLocalAddresses()[0];
			break;
		default:
			m_ipAddress = IP.GetLocalAddresses()[3];
			break;
		}
	}
}
