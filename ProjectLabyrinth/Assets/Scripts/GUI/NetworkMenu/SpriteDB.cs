using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpriteDB : MonoBehaviour {
    private static System.Type typeOfDB = typeof(Sprite);
    public static SortedDictionary<string, Sprite> GetDoodleDB()
    {
        SortedDictionary<string, Sprite> db = new SortedDictionary<string, Sprite>();
        db.Add("doodle1", Resources.Load("Sprites/Doodles/doodle01", typeOfDB) as Sprite);
        db.Add("doodle2", Resources.Load("Sprites/Doodles/doodle02", typeOfDB) as Sprite);
        db.Add("doodle3", Resources.Load("Sprites/Doodles/doodle03", typeOfDB) as Sprite);
        db.Add("doodle4", Resources.Load("Sprites/Doodles/doodle04", typeOfDB) as Sprite);
        db.Add("doodle5", Resources.Load("Sprites/Doodles/doodle05", typeOfDB) as Sprite);
        db.Add("doodle6", Resources.Load("Sprites/Doodles/doodle06", typeOfDB) as Sprite);
        db.Add("doodle7", Resources.Load("Sprites/Doodles/doodle07", typeOfDB) as Sprite);
        db.Add("doodle8", Resources.Load("Sprites/Doodles/doodle08", typeOfDB) as Sprite);
        db.Add("doodle9", Resources.Load("Sprites/Doodles/doodle09", typeOfDB) as Sprite);
        db.Add("doodle10", Resources.Load("Sprites/Doodles/doodle10", typeOfDB) as Sprite);
        return db;
    }
    public static SortedDictionary<string, Sprite> GetSlipDB()
    {
        SortedDictionary<string, Sprite> db = new SortedDictionary<string, Sprite>();
        db.Add("slip1", Resources.Load("Sprites/Slips/slipv201", typeOfDB) as Sprite);
        db.Add("slip2", Resources.Load("Sprites/Slips/slipv202", typeOfDB) as Sprite);
        db.Add("slip3", Resources.Load("Sprites/Slips/slipv203", typeOfDB) as Sprite);
        return db;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
