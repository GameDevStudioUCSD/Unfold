using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// The Network object for clients.
/// Handles join game collider.
/// Singleton.
/// Cleans up after disconnecting from server.
/// 
/// </summary>
public class NetworkJoinGame : MonoBehaviour 
{
    public bool debug = false;

    public string ipAddress = "127.0.0.1";
    public int portNumber = 26500;

    public string nextScene = "LobbyScene";

    private static NetworkJoinGame networkInstance;

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
        JoinGame();

        //Goes to OnConnectedToServer afterwards
    }

	private void JoinGame()
    {
        Network.Connect(ipAddress, portNumber);
    }

    void OnConnectedToServer()
    {
        if(debug)
        {
            Debug.Log("Connected to Server");
        }

        Application.LoadLevel(nextScene);
    }

    void OnDisconnectedFromServer()
    {
        if(debug)
        {
            Debug.Log("Disconnected from Server");
        }

        DestroyObject(this.gameObject);
    }
}
