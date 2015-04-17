using UnityEngine;
using System.Collections;

public class MazeInfo : MonoBehaviour {
	public MazeGeneratorController maze;
	Square[,] walls;
	float wallSize;


	// Use this for initialization
	void Start () {
		wallSize = maze.wallSize;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
