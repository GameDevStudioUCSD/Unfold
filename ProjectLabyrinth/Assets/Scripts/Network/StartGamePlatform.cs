using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// If the Player Avatar's collider with tag "Player" enters the platform,
/// then the server is notified.  If the player leaves, then the server is notified.
/// 
/// Once the required number of players enters the platform, the server will load
/// the game level.  Only the server receives the RPC calls so only the server
/// should be able to start the game.
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
            if(Network.isServer)
            {
                numberOfPlayers++;
            }

            networkView.RPC("AddPlayer", RPCMode.Server);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Network.isServer)
            {
                numberOfPlayers--;
            }

            networkView.RPC("SubtractPlayer", RPCMode.Server);
        }
    }

    void Update()
    {
        /* Server player does not count as a connection so use length + 1 */
        if(numberOfPlayers == Network.connections.Length + 1)
        {
            networkView.RPC("StartGame", RPCMode.All);
        }
    }

    [RPC]
    void AddPlayer()
    {
        if(debug)
        {
            Debug.Log("Add Player");
        }
        
        numberOfPlayers++;
    }

    [RPC]
    void SubtractPlayer()
    {
        if(debug)
        {
            Debug.Log("Subtract Player");
        }

        numberOfPlayers--;
    }

    [RPC]
    void StartGame()
    {
        Instantiate(loadedScene, Vector3.zero, Quaternion.identity);
    }
}
