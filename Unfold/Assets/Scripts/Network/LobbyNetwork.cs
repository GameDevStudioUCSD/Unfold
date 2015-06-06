using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Once server joins, the game is hosted and server player is instantiated.
/// 
/// A client will have to connect to the server through GUI inputs before its
/// player is instantiated.
/// 
/// </summary>

public class LobbyNetwork : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject spawnPoint;

    public GameObject startPlatformPrefab;
    public GameObject startPlatformPoint;
    public GameObject connUIPrefab;

    public int reconnectLimit = 5;
    private string ipAddress = "127.0.0.1";
    private int portNumber = 26500;
    private bool isReconnecting = false;
    private int reconnectAttempts = 0;
    private float timeToReconnect;
    private GameObject connUIInstance, cInfo;


	// Use this for initialization
	void Start () {
        connUIInstance = GameObject.Find("ConnectionInfoCanvas(Clone)");
        if(Network.isServer)
        {
            SpawnPlatform();
            SpawnPlayer();
            Destroy(connUIInstance);
        }
        // Otherwise, treat the game as a client
        else
        {
            // Find the conecting info game object that will tell us how to log
            // onto the server
            cInfo = GameObject.Find("ConnectionInfo");
            // Grab the connection info script
            ConnectionInfo cInfoScript = cInfo.GetComponent<ConnectionInfo>();
            // Create a UI element to notify the player the game is trying to 
            // connect to the server
            // Attempt to connect to the server
            Network.Connect(cInfoScript.ipAddress, cInfoScript.portNumber);
            // Set the reconnection timer
            timeToReconnect = Time.time + 3;
        }

	}
    void OnFailedToConnect(NetworkConnectionError error)
    {
        // Setup the script to reconnect
        isReconnecting = true;
        reconnectAttempts++;
    }
    void Update()
    {
        // Try to reconnect if connection failed
        if(isReconnecting && Time.time > timeToReconnect)
        {
            Network.Connect(ipAddress, portNumber);
            isReconnecting = false;
        }
        // We've tried to reconnect to many times. Terminating connecting
        else if (reconnectAttempts > reconnectLimit)
        {
            // Prevent multiple connection info objects from existing
            Destroy(cInfo);  
            MiscFunctions func = new MiscFunctions();
            func.Load("MainMenu");
        }
    }


    void OnConnectedToServer()
    {
        // Destroy the UI element telling the player they're connecting and
        // throw them into the game
        Destroy(connUIInstance);
        SpawnPlayer();
    }
   

    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity, 0);
    }

    private void SpawnPlatform()
    {
        Network.Instantiate(startPlatformPrefab, startPlatformPoint.transform.position, Quaternion.Euler(0,180,0), 0);
    }
}
