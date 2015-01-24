using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	public MazeGeneratorController mazeGen;
	private Square[,] walls;
	float dir = .1f;
	bool canTurn = false;

	// Use this for initialization
	void Start () {
		walls = mazeGen.getWalls ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (dir, 0, 0);
		if (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 &&
			Mathf.Round (transform.position.x) % 10 == 0 && canTurn) {

			dir *= -1;
		}

		else if (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 &&
		         Mathf.Round (transform.position.x) % 10 == 5){
			canTurn = true;
		}
	}

	bool isInCenter() {
		return false;
	}
}
