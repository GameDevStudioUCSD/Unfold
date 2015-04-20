using UnityEngine;
using System.Collections;

public class MazeInfo : MonoBehaviour {
	public MazeGeneratorController maze;
	Square[,] walls;
	float wallSize;
	public bool debug;


	// Use this for initialization
	void Start () {
		if (debug)
			Debug.Log ("I was called!");

		this.wallSize = maze.wallSize;
		this.walls = maze.getWalls ();
	}
	
	// Update is called once per frame
	void Update () {
		if (debug)
			Debug.Log ("I was called too!");
	}

	public Square[,] getWalls() {
		return this.walls;
	}

	public Square getCurrSquare(float x, float z) {
		int initRow = (int) Mathf.Round (x / wallSize);
		int initCol = (int) Mathf.Round (z / wallSize);
		return walls [initRow, initCol];
	}

	public float getWallSize() {
		return wallSize;
	}

	// In order: south, west, north, east.
	public bool[] getSquareWalls(Square s) {
		bool south, west, north, east;

		int x = s.getRow ();
		int z = s.getCol ();
		// Because some mazes don't generate a wall on both sides of the wall, we need to
		// check the next square over as well.
		if ((x + 1) < maze.Rows)
			south = walls[x + 1, z].hasNorth;
		else
			south = true;
		
		if ((x - 1) >= 0)
			north = walls[x - 1, z].hasSouth;
		else
			north = true;
		
		if ((z - 1) >= 0)
			west = walls[x, z - 1].hasEast;
		else
			west = true;
		
		if ((z + 1) < maze.Cols)
			east = walls[x, z + 1].hasWest;
		else
			east = true;
		
		return new[] {s.hasSouth || south, s.hasWest || west, s.hasNorth || north, s.hasEast || east};
	}
}
