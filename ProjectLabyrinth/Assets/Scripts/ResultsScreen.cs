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
	/// 
	//public Text playerScore;

	public Text playerWinLose;

	void Start() {
		// Format the time in seconds into an appropriate string
		TimeSpan timeSpan = TimeSpan.FromSeconds ((int)Time.time);
		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}",
		                                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

		this.timeWasted.text = "Time: " + timeText;
		this.playerWinLose.text = "Thank you for playing our game!";

		if (this.player) {
			//this.playerScore.text = "Score: " + this.player.calculateScore();
			//this.playerWinLose.text = (player.data.win) ? "Huzzah! You Hath Bested Fellow Squids!":
			//											"Derp... You lost.";
		}
	}
}
