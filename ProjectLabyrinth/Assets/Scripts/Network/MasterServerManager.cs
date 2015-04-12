using UnityEngine;
using System.Collections;

public class MasterServerManager : MonoBehaviour {
    /// <summary>
    /// @author: Michael Gonzalez
    /// This class implements the Unity Master Server to allow users to easily
    /// connect to one another. It provides the framework for both hosts and 
    /// clients.
    /// </summary>
    private const string gameTitle = "Unfold";
    private HostData[] gameList;
    private string clientConnectErr = "Error while connecting to host: ";
    private int portNumber = 26500;

    public bool debugOn = true;
    public HostData[] GetHostData()
    {
        if( gameList == null )
        {
            MasterServer.ClearHostList();
        }
        MasterServer.RequestHostList(gameTitle);
        gameList = MasterServer.PollHostList();
        return gameList;
    }
    public void RegisterServer(string gameName, TextureController.TextureChoice gameType)
    {
        string levelType = gameType.ToString();
        MasterServer.RegisterHost(gameTitle, gameName, levelType);
    }
    public void ConnectToGame(int hostIndex)
    {
        if(!IndexInRange(hostIndex, gameList.Length, "ConnectToGame"))
        {
            return;
        }
        Network.Connect(gameList[hostIndex].ip, portNumber);
        if(debugOn)
        {
            Debug.Log("Host IP: " + gameList[hostIndex].ip);
        }
    }

    void MasterServer.OnFailedToConnectToMasterServer(NetworkConnectionError err)
    {
        Debug.Log("Error connecting to Unity Master Server: " + err);
    }
    public TextureController.TextureChoice DetermineGameType(int hostIndex)
    {
        TextureController.TextureChoice retVal;
        if(!IndexInRange(hostIndex, gameList.Length, "ConnectToGame"))
        {
            Debug.Log("Setting level type to the default: Corn");
            retVal = TextureController.TextureChoice.Corn;
        }
        else
        {
            int gameType;
            int.TryParse(gameList[hostIndex].comment, out gameType);
            retVal = (TextureController.TextureChoice)gameType;
        }
        return retVal;
    }
    private bool IndexInRange(int num, int range, string method)
    {
        bool retVal = num < range;
        if (!retVal)
            Debug.Log("Index out of bounds in " + method);
        return retVal;
    }

	
}
