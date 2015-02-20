using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Handles the server initialization and player disconnects.
/// 
/// </summary>
public class NetworkCreateGame : MonoBehaviour
{
    public bool debug = false;

    public int maxPlayerCount = 4;
    public int portNumber = 26500;

    public string nextScene = "LobbyScene";

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            StartServer();
        }

        //Goes to OnServerInitialized afterwards
    }

    void StartServer()
    {
        Network.InitializeServer(maxPlayerCount, portNumber, !Network.HavePublicAddress());
    }

    void OnServerInitialized()
    {
        if(debug)
        {
            Debug.Log("Server Initialized");
        }

        Application.LoadLevel(nextScene);
    }

    // Server will clean up if a player disconnects.
    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        Network.RemoveRPCs(networkPlayer);

        Network.DestroyPlayerObjects(networkPlayer);
    }
}
