using UnityEngine;
using System.Collections;

public class MazeInfo : MonoBehaviour {
	public MazeGeneratorController maze;
	public Square[,] walls { get; set; }
	public float wallSize { get; set; }

	public bool exists {get; set;}

	public void setWalls(Square[,] walls) {
		this.walls = walls;
	}

	public Square[,] getWalls() {
		return this.walls;
	}

	public void setWallSize(float size) {
		this.wallSize = size;
	}

	public Square getCurrSquare(float x, float z) {
		int initRow = (int) Mathf.Round (x / wallSize);
		int initCol = (int) Mathf.Round (z / wallSize);
		return walls [initRow, initCol];
	}

	public float getWallSize() {
		return wallSize;
	}

	public bool removeWall(EditWalls wall) {
		int row = (int) Mathf.Round (wall.transform.position.x / 11);
		int col = (int) Mathf.Round (wall.transform.position.z / 11);

		InnerWall iwall = (InnerWall)wall.GetComponentInChildren<InnerWall> ();
		float x = iwall.transform.position.x;
		float z = iwall.transform.position.z;

		return true;
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
