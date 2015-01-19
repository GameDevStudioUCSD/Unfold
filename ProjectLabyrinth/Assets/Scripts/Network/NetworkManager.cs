using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool debug_On;
	public GameObject playerPrefab;
	public GameObject mazeGenerator;
	public NetworkView nView;

	private MazeGeneratorController mapCreator;
	private bool serverStarted;
	private bool matchStarted;
	private bool gameList;

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	private NetworkPlayer[] playerList;
	private int playerCount;
	
	//MasterServer.ipAddress = "127.0.0.1";
	
	private void StartServer()
	{
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		/*mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
		mapCreator.Start();
		mapCreator.createWalls();*/
		if(Network.isServer)
			nView.RPC("updatePlayerList",RPCMode.All);
		
		serverStarted = true;
        
		//SpawnPlayer();
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList (typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
			
		
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		if (debug_On)
			Debug.Log ("Server Joined");
		if(Network.isClient)
			nView.RPC("updatePlayerList",RPCMode.All);
	}
	
	void OnServerInitialized()
	{
		if (debug_On)
			Debug.Log ("Server Initialized");
		//SpawnPlayer();
	}
	
	void OnGUI()
	{
		if (!matchStarted/*!Network.isClient && !Network.isServer*/)
		{
			if (!serverStarted && !gameList)
			{
				if (GUI.Button(new Rect(100, 100, 250, 100), "Create Game"))
					StartServer();
				if (GUI.Button(new Rect(100, 300, 250, 100), "Join Game"))
				{
					RefreshHostList();
					gameList = true;
				}
			}
			if (hostList != null && gameList == true)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 100+(110*i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
				if (GUI.Button (new Rect(100, 500, 250, 100), "Back"))
					gameList = false;
			}
			
			if (serverStarted)
			{
				if (GUI.Button(new Rect(100, 500, 250, 100), "Start Game"))
					StartMatch ();
				
			}
		}
	}
	
	[RPC]
	private void updatePlayerList()
	{
		//playerList[playerCount] = Network.player;
		playerCount++;
		Debug.Log("Player Count: " + playerCount);
	}
	
	[RPC]
	private void updateMatchStatus(bool matchStatus)
	{
		bool prevStatus = matchStarted;
		matchStarted = matchStatus;
		if (prevStatus == false && matchStarted == true && Network.isClient)
			SpawnPlayer();
	}
	
	private void StartMatch()
	{
		mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
		mapCreator.Start();
		mapCreator.createWalls();
		//updateMatchStatus(true);
		if(Network.isServer)
			nView.RPC("updateMatchStatus",RPCMode.All, true);
		SpawnPlayer ();
		
	}
	
	private void SpawnPlayer()
	{
		Network.Instantiate (playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	
	
}
