using UnityEngine;
using System.Collections;

public class MazeType : MonoBehaviour {

	// Use this for initialization
    private TextureController.TextureChoice gameType;
    private string gameName;
    public void SetGameType(TextureController.TextureChoice levelType)
    {
        gameType = levelType;
        DontDestroyOnLoad(transform.gameObject);
    }
    public TextureController.TextureChoice GetGameType()
    {
        return gameType;
    }
    public void SetGameName(string name)
    {
        gameName = name;
    }
    public void DisconnectMasterServer()
    {
        MasterServer.RegisterHost(MasterServerManager.gameTitle, gameName, ((int)gameType).ToString());
    }
	
}
