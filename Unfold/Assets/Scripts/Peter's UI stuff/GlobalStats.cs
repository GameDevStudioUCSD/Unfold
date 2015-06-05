using UnityEngine;

public class GlobalStats : MonoBehaviour {

	public PlayerCharacter winner { get; private set; }

	public PlayerCharacter[] notWinners { get; private set; }

	private float starttime;

	private float timeSpent;

	public void Start() {
		this.starttime = Time.time;
	}

	public void collectData(PlayerCharacter winner) {
		this.winner = winner;
		this.notWinners = (PlayerCharacter[])Object.FindObjectsOfType<PlayerCharacter>();
	}
}
