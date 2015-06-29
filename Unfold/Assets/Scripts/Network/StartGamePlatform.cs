using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// If the Player Avatar's collider with tag "Player" enters the platform,
/// then the server is notified.  If the player leaves, then the server is notified.
/// 
/// Once the required number of players enters the platform, the server will load
/// the game level.
/// 
/// </summary>
public class StartGamePlatform : MonoBehaviour {

    public GameObject loadedScene;

    private int numberOfPlayers = 0;

    public bool debug = false;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            numberOfPlayers++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            numberOfPlayers--;
        }
    }

    void Update()
    {
        if(numberOfPlayers >= Network.connections.Length + 1)
        {
            GetComponent<NetworkView>().RPC("StartGame", RPCMode.All);
        }
    }

    [RPC]
    void StartGame()
    {
        //Instantiate(loadedScene, Vector3.zero, Quaternion.identity);
        MiscFunctions peter = new MiscFunctions();
        peter.Load("GameScene");
        if(Network.isServer)
        {
            Network.maxConnections = 0;
            MasterServer.UnregisterHost();
        }
    }
}
