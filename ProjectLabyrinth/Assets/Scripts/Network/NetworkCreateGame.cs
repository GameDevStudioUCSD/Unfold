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


    public string nextScene = "LobbyScene";

    void OnTriggerEnter(Collider collider)
    {
        Application.LoadLevel(nextScene);
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        Network.RemoveRPCs(networkPlayer);
        Network.DestroyPlayerObjects(networkPlayer);
    }
}
