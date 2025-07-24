using Godot;

public class Player {
	public string Name { get; private set; }
	public int ShootCount = 0;
	public int ChamberWithBullet { get; private set; }
	public bool IsDead => (ShootCount - 1) >= ChamberWithBullet; 

	public Player(string name, int chamberWithBullet) {
		Name = name;
		ChamberWithBullet = chamberWithBullet;
	}

	public string FireChanceText() {
		if(IsDead) {
			return "Nie żyjesz!";
		}

		return $"{ShootCount}/{GameContext.CHAMBER_COUNT} | {Mathf.RoundToInt(1f / (GameContext.CHAMBER_COUNT - ShootCount) * 100)}%";
	}

	public void Fire() {
		if(IsDead) {
			return;
		}

		++ShootCount;
	}
}
