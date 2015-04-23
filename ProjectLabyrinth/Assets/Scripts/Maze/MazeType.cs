using UnityEngine;
using System.Collections;

public class MazeType : MonoBehaviour {

	// Use this for initialization
    private TextureController.TextureChoice gameType;
    public void SetGameType(TextureController.TextureChoice levelType)
    {
        gameType = levelType;
        DontDestroyOnLoad(transform.gameObject);
    }
    public TextureController.TextureChoice GetGameType()
    {
        return gameType;
    }
	
}
