using UnityEngine;
using System.Collections;

public class MazeInfo : MonoBehaviour {
	public MazeGeneratorController maze;
	Square[,] walls;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Square[,] getWalls() {
		return this.walls;
	}
}
