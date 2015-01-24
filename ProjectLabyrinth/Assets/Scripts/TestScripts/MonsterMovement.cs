using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	public float SPEED = 0.1f;

	public MazeGeneratorController mazeGen;
	private Square[,] walls;
	float xdir;
	float zdir;
	bool canTurn = false;

	// Use this for initialization
	void Start () {
		walls = mazeGen.getWalls ();
		int initRow = (int) Mathf.Round (transform.position.x / mazeGen.wallSize);
		int initCol = (int) Mathf.Round (transform.position.z / mazeGen.wallSize);

		Square initSqr = walls [initRow, initCol];

		bool[] sides = {initSqr.hasNorth, initSqr.hasSouth, initSqr.hasEast, initSqr.hasWest};

		int direction = 0;

		bool found = false;
		while (!found) {
			int side = Random.Range (0, 4); // Anhquan thinks this is gonna be a problem, if he's right then he wins
			found = !sides [side];
		
			if (found) {
				direction = side;
			}
		}

		turn (direction);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (xdir, 0, zdir);

		if (canTurn && isInCenter () && isFork ()) {
				}
	}

	bool isInCenter() {
		if (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 &&
		    Mathf.Round (transform.position.x) % 10 == 0 && 
		    Mathf.Abs (transform.position.z - Mathf.Round (transform.position.z)) < .2 &&
		    Mathf.Round (transform.position.z) % 10 == 0 && canTurn) {
			
			xdir *= -1;
		}
		
		else if (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 &&
		         Mathf.Round (transform.position.x) % 10 == 5){
			canTurn = true;
		}
		return false;
	}

	bool isFork() {}

	void turn(int direction) {
		xdir = 0;
		zdir = 0;
		if (direction == 0) {
			xdir = SPEED;
		}

		if (direction == 1) {
			xdir = -SPEED;
		}

		if (direction == 2) {
			zdir = -SPEED;
		}

		if (direction == 3) {
			zdir = SPEED;
		}
	}
}
