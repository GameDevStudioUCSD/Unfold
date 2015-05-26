using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteController {
    public bool debug = false;
    private SortedDictionary<string, Sprite> doodleDB;
    private SortedDictionary<string, Sprite> slipDB;
    public Sprite GetRandomDoodle()
    {
        if(doodleDB == null)
        {
            doodleDB = SpriteDB.GetDoodleDB();
        }
        Sprite retVal = null;
        int random = Random.Range(0, doodleDB.Count);
        random++;
        if (debug)
        {
            Debug.Log("DB size: " + doodleDB.Count);
        }
        if(!doodleDB.TryGetValue("doodle" + random.ToString(), out retVal))
        {
            Debug.LogError("Error indexing into Doodle Dude DB");
            Debug.LogError("Random Value: " + random + " DB size: " + doodleDB.Count);
        }
        return retVal;

    }
    public Sprite GetRandomSlip()
    {
        if (slipDB == null)
        {
            slipDB = SpriteDB.GetSlipDB();
        }
        Sprite retVal = null;
        int random = Random.Range(0, slipDB.Count);
        random++;
        if (debug)
        {
            Debug.Log("DB size: " + slipDB.Count);
        }
        if (!slipDB.TryGetValue("slip" + random.ToString(), out retVal))
        {
            Debug.LogError("Error indexing into Slip DB");
            Debug.LogError("Random Value: " + random + " DB size: " + slipDB.Count);
        }
        return retVal;
    }
}
