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

    private string playerName = "Default Player";
    private string ipAddress = "127.0.0.1";
    private int portNumber = 26500;
    private bool isReconnecting = false;
    private float timeToReconnect;

    private int UIStatus = 0;

    private const int CLIENT_UI = 1;

    /* Could be used to implement a server status UI */
    private const int SERVER_UI = 2;

	// Use this for initialization
	void Start () {
        if(Network.isServer)
        {
            SpawnPlatform();
        SpawnPlayer();
        }
        else
        {
            /*// Save the Host connection information
            ipAddress = Network.connections[0].ipAddress;
            portNumber = Network.connections[0].port;
            // Disconnect and reconnect to ensure prefabs instantiate in the 
            // correct location
            Network.Disconnect();
            isReconnecting = true;
            timeToReconnect = (Time.time + 1);*/
            GameObject cInfo = GameObject.Find("ConnectionInfo");
            ConnectionInfo cInfoScript = cInfo.GetComponent<ConnectionInfo>();
            Debug.Log(cInfoScript.ipAddress[0]);
            Network.Connect(cInfoScript.ipAddress, cInfoScript.portNumber);
        }

	}

    void Update()
    {
        if(isReconnecting && Time.time > timeToReconnect)
        {
            Network.Connect(ipAddress, portNumber);
            isReconnecting = false;
        }
    }


    void OnConnectedToServer()
    {
        //SpawnPlayer();
        SpawnPlayer();
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity, 0);
    }

    private void SpawnPlatform()
    {
        Network.Instantiate(startPlatformPrefab, startPlatformPoint.transform.position, Quaternion.identity, 0);
    }
}
