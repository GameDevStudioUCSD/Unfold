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
        return 0;
    }
}
