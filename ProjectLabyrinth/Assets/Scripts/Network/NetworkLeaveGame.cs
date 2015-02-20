using UnityEngine;
using System.Collections;

public class NetworkLeaveGame : MonoBehaviour {

    public string nextScene = "MultiplayerMenu";

	void OnTriggerEnter(Collider collider)
    {
        if(Network.isClient)
        {
            Network.CloseConnection(Network.connections[0], true);
        }

        if(Network.isServer)
        {
            Network.Disconnect();
        }
    }

    void OnDisconnectedFromServer()
    {
        Application.LoadLevel(nextScene);
    }
}
