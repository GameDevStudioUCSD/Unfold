using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    const int SPAWNCHANCE = 7;
    public SpawnRate forest;
    public SpawnRate cave;
    public SpawnRate corn;
    public SpawnRate mansion;
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
    public GameObject NextSpawn(Square s, GameObject[] spawnList, int spawnChance, TextureController.TextureChoice levelType)
    {
        GameObject returnVal = null;
        SpawnRate rates = new SpawnRate();
        switch (levelType)
        {
            case TextureController.TextureChoice.Forest:
                rates = forest;
                break;
            case TextureController.TextureChoice.Cave:
                rates = cave;
                break;
            case TextureController.TextureChoice.Corn:
                rates = corn;
                break;
            case TextureController.TextureChoice.Mansion:
                rates = mansion;
                break;
        }
        if (CalculateSpawnChance(s, spawnChance))
        {
            Monster monster = rates.SelectMonster();
            switch (monster)
            {
                case Monster.Spanter:
                    returnVal = spawnList[0];
                    break;
                case Monster.Bird:
                    returnVal = spawnList[1];
                    break;
                case Monster.SpinnyTop:
                    returnVal = spawnList[2];
                    break;
                case Monster.Miniman:
                    returnVal = spawnList[3];
                    break;
            }
        }
        return returnVal;
    }
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
