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

	/// <summary>
	/// TextUI elements displaying player statistics.
	/// </summary>
	/// 
	//public Text playerScore;

	public Text playerWinLose;
	public GameObject sk;
    private GameObject scoreKeeper;

	void Start() {
        // Disconnect the player from the server to ensure their model is
        // removed from the game
        Network.Disconnect();
		// Format the time in seconds into an appropriate string
        scoreKeeper = GameObject.Find("ScoreKeeper");
        double finishTime = Time.time;
        if (scoreKeeper != null)
        {
            finishTime -= scoreKeeper.GetComponent<ScoreKeeper>().GetStartTime();
        }
		TimeSpan timeSpan = TimeSpan.FromSeconds ((int)finishTime);
		string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}",
		                                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

		this.timeWasted.text = "Time: " + timeText;
		this.playerWinLose.text = "";

		if (this.sk) {
			//this.playerScore.text = "Score: " + this.player.calculateScore();
			this.playerWinLose.text = (sk.GetComponent<ScoreKeeper>().stats[0].win)
				? "Huzzah! You Hath Bested Fellow Squids!":"Derp... You lost.";
		}
	}
}
