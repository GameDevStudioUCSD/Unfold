using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Handles join game collider.
/// 
/// </summary>
public class NetworkJoinGame : MonoBehaviour 
{
    public string nextScene = "LobbyScene";

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Application.LoadLevel(nextScene);
        }
    }

}
