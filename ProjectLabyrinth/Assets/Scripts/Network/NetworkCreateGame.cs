using UnityEngine;
using System.Collections;

public class NetworkCreateGame : MonoBehaviour
{
    public bool debug = false;

    public int maxPlayerCount = 4;
    public int portNumber = 26500;

    public string nextScene = "LobbyScene";

    private static NetworkCreateGame networkInstance;

    void Awake()
    {
        if (networkInstance == null)
        {
            networkInstance = this;
        }

        if (networkInstance != this)
        {
            DestroyObject(this.gameObject);
        }

        // This object will persist through the scenes
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        StartServer();

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

    void OnDisconnectedFromServer()
    {
        if (debug)
        {
            Debug.Log("Disconnected from Server");
        }

        DestroyObject(this.gameObject);
    }
}
