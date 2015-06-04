using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureDB : MonoBehaviour {

    private static System.Type typeOfDB = typeof(Texture);
	private static int totalMansionWalls = 15;
	public static SortedDictionary<string, Texture> GetDB()
    {
        SortedDictionary<string, Texture> db = new SortedDictionary<string, Texture>();
        db.Add("cave_floor", Resources.Load("Textures/cave_floor", typeOfDB) as Texture);
        db.Add("cave_ceiling", Resources.Load("Textures/Dungeon_roof", typeOfDB) as Texture);
        db.Add("cave_wall_1", Resources.Load("Textures/Dungeon_wall_1", typeOfDB) as Texture);
		db.Add("cave_wall_2", Resources.Load("Textures/Dungeon_wall_2", typeOfDB) as Texture);
		db.Add("cave_wall_3", Resources.Load("Textures/Dungeon_wall_3", typeOfDB) as Texture);
		db.Add("cave_wall_4", Resources.Load("Textures/Dungeon_wall_4", typeOfDB) as Texture);
		db.Add("cave_wall_5", Resources.Load("Textures/Dungeon_wall_5", typeOfDB) as Texture);
		db.Add("forest_floor", Resources.Load("Textures/forest_floor", typeOfDB) as Texture);
        db.Add("forest_ceiling", Resources.Load("Textures/Forest_canopy_v2_merged", typeOfDB) as Texture);
        db.Add("forest_wall_1", Resources.Load("Textures/forest_wall_1", typeOfDB) as Texture);
        db.Add("mansion_wall_1", Resources.Load("Textures/LOBBY_WALLv1", typeOfDB) as Texture);
		db.Add("mansion_wall_2", Resources.Load("Textures/LOBBY_WALLv2", typeOfDB) as Texture);
		db.Add("mansion_wall_3", Resources.Load("Textures/LOBBY_WALLv3", typeOfDB) as Texture);
		db.Add("mansion_wall_4", Resources.Load("Textures/LOBBY_WALLv4", typeOfDB) as Texture);
		db.Add("mansion_wall_5", Resources.Load("Textures/LOBBY_WALLv5", typeOfDB) as Texture);
		db.Add("mansion_floor", Resources.Load("Textures/LobbyFloor", typeOfDB) as Texture);
        db.Add("corn_wall_1", Resources.Load("Textures/corn_wall_1", typeOfDB) as Texture);
        db.Add("corn_wall_2", Resources.Load("Textures/corn_wall_2", typeOfDB) as Texture);
        db.Add("corn_wall_3", Resources.Load("Textures/corn_wall_3", typeOfDB) as Texture);
        db.Add("corn_floor", Resources.Load("Textures/GroundPlaceholder", typeOfDB) as Texture);
        db.Add("corn_ceiling", Resources.Load ("Textures/CORN_SKYv3", typeOfDB) as Texture);
        db.Add("WarningWall", Resources.Load("Textures/Warning", typeOfDB) as Texture);
        for( int i = 6; i <= totalMansionWalls; i++ ) {
			db["mansion_wall_" + i.ToString()] = db["mansion_wall_1"];
		}
		return db;
    }
    public static int NumberOfWallTextures(TextureController.TextureChoice texture)
    {
        int retVal = 0;
        switch(texture)
        {
            case TextureController.TextureChoice.Corn:
                retVal = 3;
                break;
            case TextureController.TextureChoice.Cave:
                retVal = 5;
                break;
            case TextureController.TextureChoice.Forest:
                retVal = 1;
                break;
            case TextureController.TextureChoice.Graveyard:
                retVal = 0;
                break;
            case TextureController.TextureChoice.Mansion:
                retVal = totalMansionWalls;
                break;
        }
        return retVal;
    }
}
