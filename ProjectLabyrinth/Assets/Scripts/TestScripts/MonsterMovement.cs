using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	public float SPEED = 0.1f;

	public MazeGeneratorController mazeGen;
	private Square[,] walls;
	float xdir;
	float zdir;
	bool canTurn = false;
	int direction;

	// Use this for initialization
	void Start () {
		walls = mazeGen.getWalls ();
		Square initSqr = getCurrSquare (transform.position.x, transform.position.z);

		bool[] sides = getSides (initSqr, transform.position.x, transform.position.z);

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
		transform.Translate (Vector3.forward * SPEED);

		if (canTurn && isInCenter ()) {
			Square curr = getCurrSquare(transform.position.x, transform.position.z);
			bool[] sides = getSides (curr, transform.position.x, transform.position.z);
			if (isFork (sides)) {
				bool found = false;
				sides[(direction + 2) % 4] = true; // Don't want to turn around

				while (!found) {
					int side = Random.Range (0, 4);
					found = !sides[side];

					if (found) {
						turn (side);
					}
				}
			} else if (isCorner(sides)) {
				sides[(direction + 2) % 4] = true;

				if(sides[(direction + 1) % 4]) {
					turn ((direction + 3) % 4);
				} else {
					turn ((direction + 1) % 4);
				}
			} else if (isDeadEnd(sides)) {
				turn ((direction + 2) % 4);
			}
			canTurn = false;
		} else if (!canTurn && movingVert () && Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 && 
			Mathf.Round (transform.position.x) % 10 == 5) {

			canTurn = true;
		} else if (!canTurn && movingHoriz () && Mathf.Abs (transform.position.z - Mathf.Round (transform.position.z)) < .2 && 
			Mathf.Round (transform.position.z) % 10 == 5) {

			canTurn = true;
		}
	}

	bool isInCenter() {
		float diffx = Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x));
		float diffz = Mathf.Abs (transform.position.z - Mathf.Round (transform.position.z));
		float xpos = Mathf.Round (transform.position.x);
		float zpos = Mathf.Round (transform.position.z);

		if (diffx <= .25 &&
		    xpos % mazeGen.wallSize == 0 && 
		    diffz <= .25 &&
		    zpos % mazeGen.wallSize == 0) {
			
			return true;
		}

		return false;
	}

	bool isFork(bool[] sides) {
		int falseCount = sideCount (sides);

		return falseCount > 2;
	}

	bool isCorner(bool[] sides) {
		int falseCount = sideCount (sides);

		if (falseCount == 2) {
			return sides[direction]; // If there is no wall going forward, then this is not a corner.
		}

		return false;
	}

	bool isDeadEnd(bool[] sides) {
		return sideCount (sides) == 1;
	}

	bool movingVert() {
		return direction == 0 || direction == 2;
	}

	bool movingHoriz() {
		return direction == 1 || direction == 3;
	}

	// Actually, returns the amount of missing sides.
	int sideCount(bool[] sides) {
		int falseCount = 0;
		for (int i = 0; i < sides.Length; i++) {
			if(!sides[i]) {
				falseCount++;
			}
		}
		return falseCount;
	}

	void turn(int dir) {
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

	private bool[] getSides(Square s, float x, float z) {
		bool south, west, north, east;
		if (Mathf.Round (x / mazeGen.wallSize + 1) < mazeGen.Rows)
			south = getCurrSquare (x + mazeGen.wallSize, z).hasNorth;
		else
			south = true;

		if (Mathf.Round (x / mazeGen.wallSize - 1) >= 0)
			north = getCurrSquare (x - mazeGen.wallSize, z).hasSouth;
		else
			north = true;

		if (Mathf.Round (z / mazeGen.wallSize - 1) >= 0)
			west = getCurrSquare (x, z - mazeGen.wallSize).hasEast;
		else
			west = true;

		if (Mathf.Round (z / mazeGen.wallSize + 1) < mazeGen.Cols)
			east = getCurrSquare (x, z + mazeGen.wallSize).hasWest;
		else
			east = true;

		return new[] {s.hasSouth || south, s.hasWest || west, s.hasNorth || north, s.hasEast || east};
	}

	private Square getCurrSquare(float x, float z) {
		int initRow = (int) Mathf.Round (x / mazeGen.wallSize);
		int initCol = (int) Mathf.Round (z / mazeGen.wallSize);
		return walls [initRow, initCol];
	}
}
