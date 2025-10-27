using Godot;
using System.Linq;
using System.Collections.Generic;

public static class GameContext {
	public const int CHAMBER_COUNT = 6;

	public static event System.Action<Player> OnPlayerWon;

	public static bool IsFirstTurnOfRound { get; private set; } = true;

	public static List<Player> Players { get; private set; } = new();
	public static int PlayerCount => Players.Count;

	public static int CurrentPlayerIndex { get; private set; }
	public static Player CurrentPlayer {
		get => Players[CurrentPlayerIndex];
		set => CurrentPlayerIndex = Players.IndexOf(value);
	}

	public static void Reset() {
		CurrentPlayerIndex = 0;
		IsFirstTurnOfRound = true;
		Players.Clear();
	}

	public static void AddPlayer(string name) {
		int chamberWithBullet = GD.RandRange(0, CHAMBER_COUNT - 1);

		Player player = new Player(name, chamberWithBullet);
		Players.Add(player);
	}
	
	public static void StartNewRound() {
		IsFirstTurnOfRound = true;

		do {
			CurrentPlayerIndex = GD.RandRange(0, PlayerCount - 1);
		} while(CurrentPlayer.IsDead);

		foreach(Player player in Players) {
			GD.Print($"{player.Name}: {player.ChamberWithBullet}");
		}
	}

	public static Player GetPreviousAlivePlayer() {
		int idx = (CurrentPlayerIndex - 1 + PlayerCount) % PlayerCount;

		while(Players[idx].IsDead) {
			idx = (idx - 1 + PlayerCount) % PlayerCount;
		}

		return Players[idx];
	}

	public static void NextAlivePlayer() {
		CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayerCount;

		if(CurrentPlayer.IsDead) {
			NextAlivePlayer();
		}

		IsFirstTurnOfRound = false;
	}

	public static void EndRound() {
		IsFirstTurnOfRound = true;
	}

	public static bool CheckForWin() {
		IEnumerable<Player> alivePlayers = Players.Where(p => !p.IsDead);

		if(alivePlayers.Count() > 1) {
			return false;
		}

		OnPlayerWon?.Invoke(alivePlayers.First());
		GameContext.Reset();

		return true;
	}
}
