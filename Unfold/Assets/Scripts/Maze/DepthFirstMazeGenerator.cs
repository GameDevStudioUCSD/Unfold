using System;
using System.Collections;
using UnityEngine;
/* @author: Michael Gonzalez
 * This class generates a random maze based off a depth-first algorithm. 
 * Credit: Wikipedia http://bit.ly/145Q9PI
 */
public class DepthFirstMazeGenerator : MazeGenerator
{
    private int depth = 0;
    private float diagonalLength;
	public DepthFirstMazeGenerator(int r, int c)
	{
        this.Rows = r;
        this.Cols = c;
        this.diagonalLength = Mathf.Sqrt(r * r + c * c);
	}

    override public void run(Square[,] cells, Square end)
    {
        this.exit = end;
        this.walls = cells;
        createSquares(true);
        selectEntrance();
    }

    //Function determines the entrance to start building the maze
    public void selectEntrance()
	{
		base.selectEntrance();
		Direction edge = this.randomEdge();
        switch (edge)
        {
            case Direction.North:
                generateMaze(0, UnityEngine.Random.Range(1, Cols - 1), Direction.South, true);
                break;
            case Direction.South:
                generateMaze(Rows - 1, UnityEngine.Random.Range(1, Cols - 1), Direction.North, true);
                break;
            case Direction.East:
                generateMaze(UnityEngine.Random.Range(1, Rows - 1), Cols - 1, Direction.West, true);
                break;
            case Direction.West:
                generateMaze(UnityEngine.Random.Range(1, Rows - 1), 0, Direction.East, true);
                break;

        }
    }

    // Recursive function to generate maze
    void generateMaze(int r, int c, Direction wallToDestroy, bool started)
    {
        Square curr = walls[r, c];
        curr.visited = true;
        destroyWall(curr, wallToDestroy);
        if (curr.start) //Base Case 
        {
            switch (wallToDestroy)
            {
                case Direction.North:
                    generateMaze(r - 1, c, Direction.South, false);
                    break;
                case Direction.South:
                    generateMaze(r + 1, c, Direction.North, false);
                    break;
                case Direction.East:
                    generateMaze(r, c + 1, Direction.West, false);
                    break;
                case Direction.West:
                    generateMaze(r, c - 1, Direction.East, false);
                    break;

            }
        }
        Stack neighbors = checkNeighbors(r, c);
        while (neighbors.Count > 0)
        {
            Square next = (Square)neighbors.Pop();
            float endDist = Square.DistanceBetween(start, curr);
            if ( endDist > .7f * (diagonalLength))
            {
                exit.exit = false;
                curr.exit = true;
                exit = curr;
            }
            generateMaze(next.getRow(), next.getCol(), next.wallToDestroy, false);
            depth++;
            //Switch statement destroys the wall inside the current cell which
            //leads to the next cell
            switch (next.wallToDestroy)
            {
                case Direction.North:
                    destroyWall(curr, Direction.South);
                    break;
                case Direction.South:
                    destroyWall(curr, Direction.North);
                    break;
                case Direction.East:
                    destroyWall(curr, Direction.West);
                    break;
                case Direction.West:
                    destroyWall(curr, Direction.East);
                    break;
            }
        }
    }

    Stack checkNeighbors(int r, int c)
    {
        Stack neighbors = new Stack();
        //The first part of this function shuffles the order in which the words
		//will be checked
		int[] neighborArray = (int[])Enum.GetValues(typeof(Direction));
		for (int i = neighborArray.Length; i > 0;) {
			int j = UnityEngine.Random.Range(0, i--);

			int temp = neighborArray[i];
			neighborArray[i] = neighborArray[j];
			neighborArray[j] = temp;
		}

		for (int i = 0; i < neighborArray.Length; i++)
        {
			switch ((Direction)neighborArray[i])
			{
                case Direction.North:
                    if (r - 1 >= 0 && !walls[r - 1, c].visited)
                    {
                        walls[r - 1, c].visited = true;
                        walls[r - 1, c].wallToDestroy = Direction.South;
                        neighbors.Push(walls[r - 1, c]);
                    }
                    break;
                case Direction.South:
                    if (r + 1 < Rows && !walls[r + 1, c].visited)
                    {
                        walls[r + 1, c].visited = true;
                        walls[r + 1, c].wallToDestroy = Direction.North;
                        neighbors.Push(walls[r + 1, c]);
                    }
                    break;
                case Direction.East:
                    if (c + 1 < Cols && !walls[r, c + 1].visited)
                    {
                        walls[r, c + 1].visited = true;
                        walls[r, c + 1].wallToDestroy = Direction.West;
                        neighbors.Push(walls[r, c + 1]);
                    }
                    break;
                case Direction.West:
                    if (c - 1 >= 0 && !walls[r, c - 1].visited)
                    {
                        walls[r, c - 1].visited = true;
                        walls[r, c - 1].wallToDestroy = Direction.East;
                        neighbors.Push(walls[r, c - 1]);
                    }
                    break;
            }
        }
        return neighbors;

    }
}
