using UnityEngine;
using System.Collections;

public class Maze_Generator : MonoBehaviour {
    const int NORTH = 0;
    const int SOUTH = 1;
    const int EAST = 2;
    const int WEST = 3;
    public int Rows = 10;
    public int Cols = 10;
    public int wallSize = 10;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Player, ExitMarker;
    //private Random rng = new Random();
    private Square[,] walls;
    private int[] neighborOrder = { NORTH, SOUTH, EAST, WEST };
    private Square exit;
	// Use this for initialization
	void Start () {
        //Debug.Log("Started script");
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                walls[r, c] = new Square(r, c);
            }
        }
        
        selectEntrance();
        createWalls();
	
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
                generateMaze(0, Random.Range(1, Cols - 1 ), SOUTH, true);
                break;
            case SOUTH:
                //Debug.Log("Runng SOUTH Code");
                generateMaze(Rows - 1, Random.Range(1, Cols-1), NORTH, true);
                break;
            case EAST:
                //Debug.Log("Runng EAST Code");
                generateMaze(Random.Range(1, Rows - 1), Cols - 1 , WEST, true);
                break;
            case WEST:
                //Debug.Log("Runng WEST Code");
                generateMaze(Random.Range(1, Rows - 1), 0, EAST, true);
                break;

        }
    }
    // Function returns an int that corrolates to a wall
    int randomEdge()
    {
        return Random.Range(0,3);
    }
    // Recursive function to generate maze
    void generateMaze(int r, int c, int wallToDestroy, bool started)
    {
        Debug.Log("Generating Maze!\n Current cell is R: " + r + " C: " + c);
        Square curr = walls[r, c];
        curr.visited = true;
        curr.start = started;
        destroyWall(curr, wallToDestroy);
        //Debug.Log("Curr: Wall To Destroy: " + wallToDestroy)
        if(curr.start) //Base Case 
        {
            switch (wallToDestroy)
            {
                case NORTH:
                    generateMaze(r-1, c, SOUTH, false);
                    break;
                case SOUTH:
                    generateMaze(r+1, c, NORTH, false);
                    break;
                case EAST:
                    generateMaze(r, c+1, WEST, false);
                    break;
                case WEST:
                    generateMaze(r, c-1, EAST, false);
                    break;

            }
        }
        Stack neighbors = checkNeighbors(r, c);
        while( neighbors.Count > 0)
        {
            Square next = (Square) neighbors.Pop();
            exit = next;
            generateMaze(next.getRow(), next.getCol(), next.getWallToDestroy(), false);
            //Switch statement destroys the wall inside the current cell which
            //leads to the next cell
            switch(next.getWallToDestroy())
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

    Stack checkNeighbors(int r, int c)
    {
        Stack neighbors = new Stack();
        //The first part of this function shuffles the order in which the words
        //will be checked
        int numOfNeighbors = 4;
        int temp, i;
        //While there remain elements to shuffle...
        while(numOfNeighbors > 0)
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
        for( i = 0; i < numOfNeighbors; i++)
        {
            Debug.Log("Random Neighbor list: " + neighborOrder[i]);
            switch(neighborOrder[i])
            {
                case NORTH:
                    if (r - 1 >= 0 && !walls[r - 1, c].visited)
                    {
                        walls[r - 1, c].visited = true;
                        walls[r - 1, c].setWallToDestroy(SOUTH);
                        neighbors.Push(walls[r - 1, c]);
                    }
                    break;
                case SOUTH:
                    if (r + 1 < Rows && !walls[r + 1, c].visited) 
                    {
                        walls[r + 1, c].visited = true;
                        walls[r + 1, c].setWallToDestroy(NORTH);
                        neighbors.Push(walls[r + 1, c]);
                    }
                    break;
                case EAST:
                    if (c + 1 < Cols && !walls[r, c + 1].visited) 
                    {
                        walls[r, c + 1].visited = true;
                        walls[r, c + 1].setWallToDestroy(WEST);
                        neighbors.Push(walls[r, c + 1]);
                    }
                    break;
                case WEST:
                    if (c - 1 >= 0 && !walls[r, c - 1].visited) 
                    {
                        walls[r, c - 1].visited = true;
                        walls[r, c  - 1].setWallToDestroy(EAST);
                        neighbors.Push(walls[r, c - 1]);
                    }
                    break;
            }
        }
        return neighbors;

    }
    // Function to prevent walls from generating at runtime
    void destroyWall(Square cell, int wallToDestroy)
    {
        switch(wallToDestroy)
        {
            case NORTH:
                //Debug.Log("Removing NORTH Wall");
                cell.hasNorth = false;
                break;
            case SOUTH:
                //Debug.Log("Removing SOUTH Wall");
                cell.hasSouth = false;
                break;
            case EAST:
                //Debug.Log("Removing EAST Wall");
                cell.hasEast = false;
                break;
            case WEST:
                //Debug.Log("Removing WEST Wall");
                cell.hasWest = false;
                break;
        }
    }
    // Creates the walls flagged for creation
    void createWalls()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                Square curr = walls[r, c];
                if(curr.hasNorth)
                    Instantiate(NorthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                if (curr.hasSouth)
                    Instantiate(SouthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                if (curr.hasEast)
                    Instantiate(EastWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                if (curr.hasWest)
                    Instantiate(WestWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                if (curr.start)
                    Instantiate(Player, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                
            }
        }
        //Instantiate(ExitMarker, new Vector3(exit.getRow() * wallSize, 1, wallSize * exit.getCol()), Quaternion.identity);
    }
}
