using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This script has been repurposed to move the player to the multiplayer menu.
/// 
/// </summary>
public class NetworkCreateGame : MonoBehaviour
{
    public bool debug = false;
	public bool active;


    public string nextScene = "LobbyScene";

    void OnTriggerEnter(Collider collider)
    {
		if (active) {
			Application.LoadLevel (nextScene);
		}
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        Network.RemoveRPCs(networkPlayer);
        Network.DestroyPlayerObjects(networkPlayer);
    }
}
