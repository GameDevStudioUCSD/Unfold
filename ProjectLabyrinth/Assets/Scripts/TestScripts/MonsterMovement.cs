using UnityEngine;
using System.Collections;

abstract public class MonsterMovement : MonoBehaviour {

	public float SPEED = 0.1f;

	public MazeGeneratorController mazeGen;
	private Square[,] walls;
	protected float xdir;
	protected float zdir;
	protected bool canTurn = false;
	protected int direction;

	// Use this for initialization
	void Start () {
		walls = mazeGen.getWalls ();
		Square initSqr = getCurrSquare (transform.position.x, transform.position.z);

		bool[] sides = getSides (initSqr);

		direction = 3;

		bool found = false;
		while (!found) {
			int side = Random.Range (0, 4); // Anhquan thinks this is gonna be a problem, if he's right then he wins
			found = !sides [side];
		
			if (found) {
				turn (side);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		AI ();
		maneuver ();
	}

	abstract public void maneuver ();
	abstract public void AI ();

	protected bool isInCenter() {
		if (Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .25 &&
		    Mathf.Round (transform.position.x) % 10 == 0 && 
		    Mathf.Abs (transform.position.z - Mathf.Round (transform.position.z)) < .25 &&
		    Mathf.Round (transform.position.z) % 10 == 0) {
			
			return true;
		}

		return false;
	}

	protected bool isFork(bool[] sides) {
		int falseCount = sideCount (sides);

		return falseCount > 2;
	}

	protected bool isCorner(bool[] sides) {

		if (sideCount(sides) == 2) {
			return sides[direction]; // If there is no wall going forward, then this is not a corner.
		}

		return false;
	}

	protected bool isDeadEnd(bool[] sides) {
		return sideCount (sides) == 1;
	}

	protected bool movingVert() {
		return direction % 2 == 0;
	}

	protected bool movingHoriz() {
		return direction % 2 == 1;
	}

	// Actually, returns the amount of missing sides.
	protected int sideCount(bool[] sides) {
		int falseCount = 0;
		for (int i = 0; i < sides.Length; i++) {
			if(!sides[i]) {
				falseCount++;
			}
		}
		return falseCount;
	}

	protected void turn(int dir) {
		xdir = 0;
		zdir = 0;
		if (dir == 0) {
			xdir = SPEED;
		}

		if (dir == 1) {
			zdir = -SPEED;
		}

		if (dir == 2) {
			xdir = -SPEED;
		}

		if (dir == 3) {
			zdir = SPEED;
		}

		transform.Rotate (Vector3.up * 90 * ((dir - direction) % 4));
		
		canTurn = false;
		direction = dir;
	}

	protected bool[] getSides(Square s) {
		return new[] {s.hasSouth, s.hasWest, s.hasNorth, s.hasEast};
	}

	protected Square getCurrSquare(float x, float z) {
		int initRow = (int) Mathf.Round (x / mazeGen.wallSize);
		int initCol = (int) Mathf.Round (z / mazeGen.wallSize);
		return walls [initRow, initCol];
	}
}
