using UnityEngine;
using System.Collections;

public class Maze_Generator : MonoBehaviour {
    public int Rows = 10;
    public int Cols = 10;
    public GameObject WallStub;
    private bool[,] walls;
	// Use this for initialization
	void Start () {
        walls = new bool[Rows,Cols];
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
            }
        }
	
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
