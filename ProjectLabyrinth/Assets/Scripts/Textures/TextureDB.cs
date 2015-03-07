using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TextureDB : MonoBehaviour {

    private static System.Type typeOfDB = typeof(Texture);
    public static SortedDictionary<string, Texture> GetDB()
    {
        SortedDictionary<string, Texture> db = new SortedDictionary<string, Texture>();
        db.Add("cave_floor", AssetDatabase.LoadAssetAtPath("Assets/Textures/cave_floor.png", typeOfDB) as Texture);
        db.Add("cave_wall_1", AssetDatabase.LoadAssetAtPath("Assets/Textures/cave_wall_1.png", typeOfDB) as Texture);
        db.Add("forest_floor", AssetDatabase.LoadAssetAtPath("Assets/Textures/forest_floor.png", typeOfDB) as Texture);
        db.Add("forest_wall_1", AssetDatabase.LoadAssetAtPath("Assets/Textures/forest_wall_1.png", typeOfDB) as Texture);
        db.Add("corn_wall_1", AssetDatabase.LoadAssetAtPath("Assets/Textures/corn_wall_1.png", typeOfDB) as Texture);
        db.Add("corn_floor", AssetDatabase.LoadAssetAtPath("Assets/Textures/corn_floor.png", typeOfDB) as Texture);
        return db;
    }
    public static int NumberOfWallTextures(TextureController.TextureChoice texture)
    {
        int retVal = 0;
        switch(texture)
        {
            case TextureController.TextureChoice.Corn:
                retVal = 1;
                break;
            case TextureController.TextureChoice.Cave:
                retVal = 1;
                break;
            case TextureController.TextureChoice.Forest:
                retVal = 1;
                break;
            case TextureController.TextureChoice.Graveyard:
                retVal = 0;
                break;
            case TextureController.TextureChoice.Mansion:
                retVal = 0;
                break;
        }
        return retVal;
    }
}
