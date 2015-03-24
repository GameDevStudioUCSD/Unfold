using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for UI display behavior in the Results Scene.
/// </summary>
public class ResultsScreen : MonoBehaviour {

	/// <summary>
	/// TextUI displaying total amount of time spent on this game.
	/// </summary>
	public Text timeWasted;

	/// <summary>
	/// The player viewing this results screen.
	/// </summary>
	public PlayerCharacter player;

	/// <summary>
	/// TextUI elements displaying player statistics.
	/// </summary>
	public Text playerMaxHealth;
	public Text playerDamage;

	void Start() {
		// Format the time in seconds into an appropriate string
		TimeSpan timeSpan = TimeSpan.FromSeconds ((int)Time.time);
		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}",
		                                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

		this.timeWasted.text = "Time wasted on this game: " + timeText;

		if (this.player) {
			this.playerMaxHealth.text = "Your max health: " + this.player.maxHealth;
			this.playerDamage.text = "Your damage: " + this.player.maxDamage;
		}
	}
}
