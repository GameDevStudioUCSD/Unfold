﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    const int SPAWNCHANCE = 7;
    public static bool debug_ON = false;
	// Calculates the chance whether a monster should spawn in a given square 
    // Returns true if a monster should spawn, else returns false
	private static bool CalculateSpawnChance(Square cell, int spawnChance)
    {
        bool willSpawn = false;
        float seed = 1;
        seed *= cell.weight;
        seed *= CorridorFinder.GetAdjacentWalls(cell);
        if (debug_ON)
            Debug.Log("Seed: " + seed);
        if (seed > Random.Range(1, spawnChance))
            willSpawn = true;
        return willSpawn;
    }
    // Creates the next GameObject to spawn.
    // Returns the Object if a random seed is met, else returns null
    public static GameObject NextSpawn(Square cell, GameObject[] spawnList, int spawnChance)
    {
        GameObject returnVal = null;
        if(CalculateSpawnChance(cell, spawnChance))
        {
            int i = Random.Range(0, spawnList.Length);
            returnVal = (GameObject)spawnList[i];
        }
        return returnVal;
    }
}
