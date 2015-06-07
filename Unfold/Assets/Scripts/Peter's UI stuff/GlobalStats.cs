using UnityEngine;

public class GlobalStats : MonoBehaviour {

	public bool gameOver { get; set; }

	public PlayerCharacter winner { get; private set; }

	public PlayerCharacter[] notWinners { get; private set; }

	private float starttime;

	private float timeSpent;

	public void OnEnable() {
		this.gameOver = false;
		this.starttime = Time.time;
	}

	public void collectData(PlayerCharacter winner) {
		this.timeSpent = Time.time - this.starttime;
		this.winner = winner;
		this.notWinners = (PlayerCharacter[])Object.FindObjectsOfType<PlayerCharacter>();
		Object.DontDestroyOnLoad(this.gameObject);
	}
}
