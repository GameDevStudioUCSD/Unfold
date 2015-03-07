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

    private int UIStatus = 0;

    private const int CLIENT_UI = 1;

    /* Could be used to implement a server status UI */
    private const int SERVER_UI = 2;

	// Use this for initialization
	void Start () {


        if(Network.isServer)
        {
            UIStatus = SERVER_UI;

            SpawnPlayer();

            SpawnPlatform();
        }
        else
        {
            UIStatus = CLIENT_UI;
        }

	}

    void OnGUI()
    {
        if(UIStatus == CLIENT_UI)
        {
            GUILayout.Label("Player Name:");
            playerName = GUILayout.TextField(playerName);

            GUILayout.Space(5);

            //User can type in IP address for the server
            GUILayout.Label("IP Address:");
            ipAddress = GUILayout.TextField(ipAddress);

            GUILayout.Space(5);

            GUILayout.Label("Port Number:");
            portNumber = int.Parse(GUILayout.TextField(portNumber.ToString()));

            GUILayout.Space(5);

            if (GUILayout.Button("Connect", GUILayout.Height(50)))
            {
                Network.Connect(ipAddress, portNumber);

                UIStatus = 0;
            }
        }
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();

        //SpawnPlatform();
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
