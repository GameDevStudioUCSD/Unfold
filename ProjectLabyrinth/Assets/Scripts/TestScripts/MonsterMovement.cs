using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour {

	private MazeGeneratorController mazeGen;
	private Square[,] walls;
	float dir = .1f;
	bool begin = true;

	// Use this for initialization
	void Start () {
		mazeGen = GameObject.Find ("Maze Generator").GetComponent<MazeGeneratorController>();
		walls = mazeGen.getWalls ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (dir, 0, 0);
		if (!begin && transform.position.x % 10 == 0) {
						dir *= -1;
		} else if (transform.position.x % 10 == 0) {
			begin = false;
			dir *= -1;
		}
	}

	bool isInCenter() {
		return false;
	}
}
