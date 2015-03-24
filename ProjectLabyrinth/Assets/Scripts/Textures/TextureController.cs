using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureController : MonoBehaviour {
    /* @author: Michael Gonzalez
     * This class allows developers to control a level's theme. More 
     * specifically, it allows the developer to pick a texture group for any
     * level. 
     */
    public enum TextureChoice
    {
        Corn,
        Cave,
        Graveyard,
        Mansion,
        Forest
    };
    private TextureChoice textureType;
    private const string str_error = "Error inside TextureController.cs: ";
    private string textureText;
    private int[] wallMaximums;
    private bool debugOn = false;
    private SortedDictionary<string, Texture> db = TextureDB.GetDB();
    public TextureController() : this(TextureChoice.Corn)
    {
    }
    public TextureController(TextureChoice texture)
    {
        this.SetTextureType(texture);
    }
    public void SetTextureType(TextureChoice texture)
    {
        textureType = texture;
        int numberOfWalls = TextureDB.NumberOfWallTextures(textureType);
        wallMaximums = new int[numberOfWalls];
        for(int i = 0; i < numberOfWalls; i++)
        {
            SetNWallMax(i, -1);
        }
        switch(texture)
        {
            case TextureChoice.Corn:
                textureText = "corn";
                break;
            case TextureChoice.Mansion:
                textureText = "mansion";
                break;
            case TextureChoice.Forest:
                textureText = "forest";
                break;
            case TextureChoice.Cave:
                textureText = "cave";
                break;
            case TextureChoice.Graveyard:
                textureText = "graveyard";
                break;
        }
    }
    public TextureChoice GetTextureType()
    {
        return textureType;
    }
    /* This function sets the maximum number of times wall N of the chosen 
     * texture type can be instatiated. 
     * Setting max walls to a negative number allows an infinite number of said
     * wall to be infinite
     */
    public void SetNWallMax(int wallNumber, int maxNum)
    {
        if(debugOn)
        {
            Debug.Log("MaxNum: " + maxNum);
        }
        if(wallNumber > TextureDB.NumberOfWallTextures(textureType))
        {
            Debug.LogError(str_error + "Tried to modify a wall that doesn't exist");
        }
        if (maxNum < 0) // Set to (2^31)-1 Basically infinity for our purposes
            wallMaximums[wallNumber] = 2147483647;
        else
            wallMaximums[wallNumber] = maxNum;
    }
	public Texture GetRandomWall()
    {
        Texture retVal = null;
        bool hasFoundValidValue = false;
        int index = 0;
        while(!hasFoundValidValue)
        {
            index = (int)Mathf.Floor(Random.Range(0, wallMaximums.Length));
            if(debugOn)
            {
                Debug.Log("Random wall chosen: " + index);
            }
            if (wallMaximums[index] > 0)
            {
                hasFoundValidValue = true;
                wallMaximums[index]--;
                break;
            }
        }
        index++;
        if(!db.TryGetValue(textureText + "_wall_" + index.ToString(), out retVal))
        {
            Debug.LogError( str_error + "Error indexing wall in GetRandomTexture()");
        }
        
        return retVal;
    }
    public Texture GetFloorTexture()
    {
        Texture retVal = null;
        if (!db.TryGetValue(textureText + "_floor" , out retVal))
        {
            Debug.LogError(str_error + "Error indexing floor in GetFloorTexture()");
        }

        return retVal;
    }
}
