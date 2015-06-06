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
    public GameObject connectingUI;
    private float startTime;


    public string nextScene = "LobbyScene";

    void Start()
    {
        startTime = 100000000000000;
    }
    void Update()
    {
        if (Time.time > startTime)
        {
			Application.LoadLevel (nextScene);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (active)
        {
            GameObject connUI = (GameObject)GameObject.Instantiate(connectingUI);
            GameObject.DontDestroyOnLoad(connUI);
            startTime = Time.time + 0.3f;
        }
    }

    void OnPlayerDisconnected(NetworkPlayer networkPlayer)
    {
        Network.RemoveRPCs(networkPlayer);
        Network.DestroyPlayerObjects(networkPlayer);
    }
}
