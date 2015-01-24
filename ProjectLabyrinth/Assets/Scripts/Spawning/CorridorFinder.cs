using UnityEngine;
using System.Collections;

public class CorridorFinder : MonoBehaviour
{
	// Walks through a maze and builds a list of Squares and their
	// corridor's length. The length of each corridor will be used
	// as a weight for spawning monsters.
	public static ArrayList FindCorridors (Square[,] maze, int rows, int cols)
	{
        ArrayList listOfCorridors = new ArrayList();
        for (int r = 0; r < rows; r++ )
        {
            for( int c = 0; c < cols; c++)
            {
                if( GetAdjacentWalls(maze[r,c]) == 3)
                {
                    listOfCorridors.Add(maze[r, c]);
                    CorridorWalker(maze[r, c]);
                }
            }
        }
            return listOfCorridors;
	}

	// Walks backwards from the end of a corridor (Square with 3 walls
	// adjacent to it), and returns the length of that corridor.
	private static int CorridorWalker (Square endOfCorridor)
	{
		return 0;
	}

    private static int GetAdjacentWalls(Square cell)
    {
		int adjacentWalls = 0;
		if (cell.hasNorth)
			adjacentWalls++;
		if (cell.hasSouth)
			adjacentWalls++;
		if (cell.hasWest)
			adjacentWalls++;
		if (cell.hasEast)
			adjacentWalls++;

		return adjacentWalls;
    }

	private static void SetAdjacentWalls(Square[,] maze, int rows, int cols){
		for (int r=0; r < rows-1; r++) {
			for(int c=0; c < cols-1; c++){
				Square temp = maze[r,c];
				if(temp.hasNorth && c>0){
					temp = maze[r,c-1];
					temp.hasSouth = true;
				}
				if(temp.hasSouth && c<cols){
					temp = maze[r,c+1];
					temp.hasNorth = true;
				}
				if(temp.hasWest && r>0){
					temp = maze[r-1,c];
					temp.hasWest = true;
				}
				if(temp.hasEast && r<rows){
					temp = maze[r+1,c];
					temp.hasSouth = true;
				}
			}
		}
	}
}
