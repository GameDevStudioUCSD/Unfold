using UnityEngine;
using System.Collections;

public class NetworkingMenu : MonoBehaviour {

	public string connectionIP = "127.0.0.1";
	public int portNumber = 8888;
    public static bool Connected { get; private set; }

	private void OnConnectedToServer()
	{
		Connected = true;
	}

	private void OnServerInitialized()
	{
		Connected = true;
	}


	private void OnDisconnectedFromServer()
	{
		Connected = false;
	}

	private void OnGUI()
	{
		if (!Connected)
		{
			connectionIP = GUILayout.TextField (connectionIP);
			//portNumber = int.Parse (GUILayout.TextField (portNumber.ToString ));
			if (GUILayout.Button ("Connect"))
				Network.Connect (connectionIP, portNumber);
			if (GUILayout.Button ("Host"))
					Network.InitializeServer (4, portNumber, false);
		}
		else
			GUILayout.Label ("Connections: " + Network.connections.Length.ToString ());

	}
}
