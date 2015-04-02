using UnityEngine;
using System.Collections;
/* @author: Michael Gonzalez
 * This class generates a random maze based off a depth-first algorithm. 
 * Credit: Wikipedia http://bit.ly/145Q9PI
 */
public class WorkingDepthFirstMazeGenerator : MazeGenerator
{
	public bool debug_On = true;

    private int[] neighborOrder = { NORTH, SOUTH, EAST, WEST };
    private int depth = 0;
    private float diagonalLength;
	public WorkingDepthFirstMazeGenerator(int r, int c)
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
        //if (exit == start)
            EnsureExitExists();
    }

    //Function determines the entrance to start building the maze
    void selectEntrance()
    {
        //Debug.Log("Trying to make entrance");
        int edge = randomEdge();
        //Debug.Log("The random edge is: " + edge);
        switch (edge)
        {
            case NORTH:
                //Debug.Log("Runng NORTH Code");
                generateMaze(0, Random.Range(1, Cols - 1), SOUTH, true);
                break;
            case SOUTH:
                //Debug.Log("Runng SOUTH Code");
                generateMaze(Rows - 1, Random.Range(1, Cols - 1), NORTH, true);
                break;
            case EAST:
                //Debug.Log("Runng EAST Code");
                generateMaze(Random.Range(1, Rows - 1), Cols - 1, WEST, true);
                break;
            case WEST:
                //Debug.Log("Runng WEST Code");
                generateMaze(Random.Range(1, Rows - 1), 0, EAST, true);
                break;

        }
    }

    // Recursive function to generate maze
    void generateMaze(int r, int c, int wallToDestroy, bool started)
    {
        //Debug.Log("Generating Maze!\n Current cell is R: " + r + " C: " + c);
        Square curr = walls[r, c];
        curr.visited = true;
        if (started)
        {
            curr.start = started;
            exit = curr;
            start = curr;
        }
        destroyWall(curr, wallToDestroy);
        //Debug.Log("Curr: Wall To Destroy: " + wallToDestroy)
        if (curr.start) //Base Case 
        {
            switch (wallToDestroy)
            {
                case NORTH:
                    generateMaze(r - 1, c, SOUTH, false);
                    break;
                case SOUTH:
                    generateMaze(r + 1, c, NORTH, false);
                    break;
                case EAST:
                    generateMaze(r, c + 1, WEST, false);
                    break;
                case WEST:
                    generateMaze(r, c - 1, EAST, false);
                    break;

            }
        }
        Stack neighbors = checkNeighbors(r, c);
        bool hasFoundExit = start == exit;
        while (neighbors.Count > 0)
        {
            Square next = (Square)neighbors.Pop();
			if(next.visited)
			{
				continue;
			}
            //Debug.Log("Depth: " + depth);
            float endDist = Square.DistanceBetween(start, curr);
            
            /**if (hasFoundExit && endDist > .7f * (diagonalLength))
            {
                exit.exit = false;
                if (debug_On)
                	Debug.Log(exit == start);
                curr.exit = true;
                exit = curr;
            }**/
            generateMaze(next.getRow(), next.getCol(), next.getWallToDestroy(), false);
            depth++;
            //Switch statement destroys the wall inside the current cell which
            //leads to the next cell
            switch (next.getWallToDestroy())
            {
                case NORTH:
                    destroyWall(curr, SOUTH);
                    break;
                case SOUTH:
                    destroyWall(curr, NORTH);
                    break;
                case EAST:
                    destroyWall(curr, WEST);
                    break;
                case WEST:
                    destroyWall(curr, EAST);
                    break;
            }
        }
    }

    private void EnsureExitExists()
    {
        /**int startQuadrant = Square.DetermineQuadrant(start, walls);
        switch(startQuadrant)
        {
            case 1:
                exit = walls[Rows - 1, 0];
                break;
            case 2:
                exit = walls[Rows - 1, Cols - 1];
                break;
            case 3:
                exit = walls[0, 0];
                break;
            case 4:
                exit = walls[0, Cols - 1];
                break;
        }
        exit.exit = true;**/
        ArrayList corridors = CorridorFinder.FindCorridors(walls, Rows, Cols);
        float dist = 0;
        float newDist = 1;
        exit = start;
        
        foreach (Square s in corridors)
        {
            if(debug_On)
            {
                Debug.Log(Square.DistanceBetween(s, start));
            }
            newDist = Square.DistanceBetween(s, start);
            if(  newDist > dist)
            {
                exit = s;
                dist = newDist;
            }
        }
        exit.exit = true;
    }

    Stack checkNeighbors(int r, int c)
    {
        Stack neighbors = new Stack();
        //The first part of this function shuffles the order in which the words
        //will be checked
        int numOfNeighbors = 4;
        int temp, i;
        //While there remain elements to shuffle...
        while (numOfNeighbors > 0)
        {
            //Pick a remaining element
            i = Random.Range(0, numOfNeighbors--);
            //And swap it with the current element
            temp = neighborOrder[numOfNeighbors];
            neighborOrder[numOfNeighbors] = neighborOrder[i];
            neighborOrder[i] = temp;
        }
        // Checks the valid neighbors
        numOfNeighbors = 4;
        for (i = 0; i < numOfNeighbors; i++)
        {
            //Debug.Log("Random Neighbor list: " + neighborOrder[i]);
            switch (neighborOrder[i])
            {
                case NORTH:
                    if (r - 1 >= 0 && !walls[r - 1, c].visited)
                    {
                        //walls[r - 1, c].visited = true;
                        walls[r - 1, c].setWallToDestroy(SOUTH);
                        neighbors.Push(walls[r - 1, c]);
                    }
                    break;
                case SOUTH:
                    if (r + 1 < Rows && !walls[r + 1, c].visited)
                    {
                        //walls[r + 1, c].visited = true;
                        walls[r + 1, c].setWallToDestroy(NORTH);
                        neighbors.Push(walls[r + 1, c]);
                    }
                    break;
                case EAST:
                    if (c + 1 < Cols && !walls[r, c + 1].visited)
                    {
                        //walls[r, c + 1].visited = true;
                        walls[r, c + 1].setWallToDestroy(WEST);
                        neighbors.Push(walls[r, c + 1]);
                    }
                    break;
                case WEST:
                    if (c - 1 >= 0 && !walls[r, c - 1].visited)
                    {
                        //walls[r, c - 1].visited = true;
                        walls[r, c - 1].setWallToDestroy(EAST);
                        neighbors.Push(walls[r, c - 1]);
                    }
                    break;
            }
        }
        return neighbors;

    }
}
