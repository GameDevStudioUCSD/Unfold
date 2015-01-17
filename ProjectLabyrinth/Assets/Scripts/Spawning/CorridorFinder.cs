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
	private static int CorridorWalker (Square endOfCorridor)
	{
		return 0;
	}
}
