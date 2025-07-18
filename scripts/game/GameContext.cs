using Godot;
using System.Collections.Generic;

public static class GameContext {
	public const int CHAMBER_COUNT = 6;

	public static List<Player> Players { get; private set; } = new();
	public static int PlayerCount => Players.Count;

	public static int CurrentPlayerIndex { get; private set; }
	public static Player CurrentPlayer {
		get => Players[CurrentPlayerIndex];
		set => CurrentPlayerIndex = Players.IndexOf(value);
	}

	public static Player WinnerPlayer { get; private set; } = null;

	public static void Reset() {
		Players.Clear();
		WinnerPlayer = null;
	}

	public static void AddPlayer(string name) {
		int chamberWithBullet = GD.RandRange(0, CHAMBER_COUNT - 1);

		Player player = new Player(name, chamberWithBullet);
		Players.Add(player);
	}
	
	public static void Start() {
		CurrentPlayerIndex = GD.RandRange(0, PlayerCount - 1);
	}

	public static void NextPlayer() {
		if(CheckForWin()) {
			return;
		}

		CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerCount;

		if(CurrentPlayer.IsDead) {
			NextPlayer();
		}
	}

	public static bool CheckForWin() {
		List<Player> alivePlayers = Players.FindAll(p => !p.IsDead);

		if(alivePlayers.Count == 1) {
			WinnerPlayer = alivePlayers[0];
			return true;
		}

		return false;
	}
}
