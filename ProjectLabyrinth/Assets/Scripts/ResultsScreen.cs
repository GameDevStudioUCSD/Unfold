using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for UI display behavior in the Results Scene.
/// </summary>
public class ResultsScreen : MonoBehaviour {

	public Text timeWasted;

	public PlayerCharacter player;
	public Text playerMaxHealth;
	public Text playerDamage;

	void Start() {
		TimeSpan timeSpan = TimeSpan.FromSeconds ((int)Time.time);
		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}",
		                                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		this.timeWasted.text = "Time wasted on this game: " + timeText;

		if (this.player) {
			this.playerMaxHealth.text = "Your max health: " + this.player.maxHealth;
			this.playerDamage.text = "Your damage: " + this.player.damage;
		}
	}
}
