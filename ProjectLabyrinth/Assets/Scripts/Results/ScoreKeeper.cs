using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public const int NUMBER_OF_PLAYERS = 6;
	public PlayerData[] stats = new PlayerData[NUMBER_OF_PLAYERS];
	public int numPlayers = 0;
    private double startTime;

    public void Start()
    {
        startTime = Time.time;
    }
    public double GetStartTime() { return startTime; }
	public void addPlayer(string name)
	{
		stats [numPlayers].name = name;
		numPlayers++;
	}
}
