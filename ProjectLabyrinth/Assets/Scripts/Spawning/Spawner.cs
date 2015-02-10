using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    const int SPAWNCHANCE = 30;
	// Calculates the chance whether a monster should spawn in a given square 
    // Returns true if a monster should spawn, else returns false
	private static bool CalculateSpawnChance(Square cell)
    {
        bool willSpawn = false;
        float seed = 1;
        seed *= cell.weight;
        seed *= CorridorFinder.GetAdjacentWalls(cell);
        if (seed > Random.Range(1, SPAWNCHANCE))
            willSpawn = true;
        return willSpawn;
    }
    // Creates the next GameObject to spawn.
    // Returns the Object if a random seed is met, else returns null
    public static GameObject NextSpawn(Square cell, GameObject[] spawnList)
    {
        GameObject returnVal = null;
        if(CalculateSpawnChance(cell))
        {
            int i = Random.Range(0, spawnList.Length);
            returnVal = (GameObject)spawnList[i];
        }
        return returnVal;
    }
}
