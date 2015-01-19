using UnityEngine;
using System.Collections;

public class CorridorFinder : MonoBehaviour
{
	// Walks through a maze and builds a list of Squares and their
	// corridor's length. The length of each corridor will be used
	// as a weight for spawning monsters.
	public static ArrayList FindCorridors (Square[,] maze)
	{
		return null;
	}

	// Walks backwards from the end of a corridor (Square with 3 walls
	// adjacent to it), and returns the length of that corridor.
	private static int CorridorWalker (Square endOfCorridor, Square[,] maze)
	{
		// PSEUDOCODE:
		// 1. Create temp Squares, one for current and one for the prev square
		// 2. While adjacent wall count >= 2 (Either at the end of a corridor
		//    or in the middle of a hall, which is 2-3 adjacent walls)
		//    NOTE: 0-1 walls means multiple paths, signaling the end of a corridor
		// -  1. Find the 1-2 adjacent Squares next to current. Whichever one
		//      is NOT prev (or if prev is null, there should only be 1 Square),
		//      then set prev to current and current to the next Square
		// -  2. Increment corridorLength
		// 3. Return corridorLength

		Square current = endOfCorridor;
		Square prev = null;
		int corridorLength = 0;

		while (current.numOfWalls >= 2) // TODO: Update numOfWalls to function call
		{
			// NOTE:
			// hasEast, West, North, and South functions make this code a lot easier
			// I also need the function to count total walls, using numOfWalls for now

			// Potential issues with counting rows/columns. Double check that this is right!!!
			// TODO: Check for bounds?

			Square eastWall = maze[current.getRow(), current.getCol() + 1];
			Square westWall = maze[current.getRow(), current.getCol() - 1];
			Square northWall = maze[current.getRow() - 1, current.getCol()];
			Square southWall = maze[current.getRow() + 1, current.getCol()];

			// Check to see if there is no east wall and if prev != the east Square
			// AKA, check to see if this adjacent Square is legal to move to
			if (!current.hasEast && prev != eastWall)
			{
				prev = current;
				current = eastWall;
				corridorLength++;
			}
			else if (!current.hasWest && prev != westWall)
			{
				prev = current;
				current = westWall;
				corridorLength++;
			}
			else if (!current.hasNorth && prev != northWall)
			{
				prev = current;
				current = northWall;
				corridorLength++;
			}
			else if (!current.hasSouth && prev != southWall)
			{
				prev = current;
				current = southWall;
				corridorLength++;
			}
			else
			{
				// No legal moves, something went wrong
				// Throw or return 0
			}
		}

		return corridorLength;
	}
}
