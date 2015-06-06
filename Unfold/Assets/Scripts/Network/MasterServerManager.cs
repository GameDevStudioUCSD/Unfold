using UnityEngine;
using System.Collections;

public class MasterServerManager : MonoBehaviour{
    /// <summary>
    /// @author: Michael Gonzalez
    /// This class implements the Unity Master Server to allow users to easily
    /// connect to one another. It provides the framework for both hosts and 
    /// clients.
    /// </summary>
    public const string gameTitle = "UnfoldX";
    private HostData[] gameList;
    private string clientConnectErr = "Error while connecting to host: ";
    private int portNumber = 26500;
    private string[] lastConnectionAttempt;
    public const uint CANCONNECT = 0x8000000;
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
        uint levelType = (uint)gameType;
        levelType |= CANCONNECT;
        MasterServer.RegisterHost(gameTitle, gameName, levelType.ToString());
    }
    public void ConnectToGame(int hostIndex, GameObject connectionInfo)
    {
        lastConnectionAttempt = gameList[hostIndex].ip;
        GameObject cInfo = (GameObject)Instantiate(connectionInfo);
        ConnectionInfo cInfoScript = cInfo.GetComponent<ConnectionInfo>();
        cInfoScript.setInfo(lastConnectionAttempt, portNumber);
    }
    public void RetryConnection()
    {
        Network.Connect(lastConnectionAttempt, portNumber);
    }
    public TextureController.TextureChoice DetermineGameType(int hostIndex)
    {
        TextureController.TextureChoice retVal;
        if(!IndexInRange(hostIndex, gameList.Length, "ConnectToGame"))
        {
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
        return retVal;
    }

	
}
