using UnityEngine;
using System.Collections;

public class MazeType : MonoBehaviour {

	// Use this for initialization
    private TextureController.TextureChoice gameType;
    private string gameName;
    public delegate Object InstantiationMethod(Object original, Vector3 position, Quaternion rotation);
    public InstantiationMethod instantiationMethod = MyNetInstantiate;
    void Start()
    {
    }
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
    public static Object MyNetInstantiate(Object original, Vector3 position, Quaternion rotation)
    {
        return Network.Instantiate(original, position, rotation, 0);
    }
    public void SetSinglePlayer(bool flag)
    {
        if (flag)
        {
            instantiationMethod = Object.Instantiate;
        }
        else
        {
            instantiationMethod = MyNetInstantiate;
        }
    }
	
}
