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
	private bool serverJoined;
	
	private const int NONE_UI = 0;
	private const int START_UI = 1;
	private const int LOBBYHOST_UI = 6;
	private const int LOBBY_UI = 10;
	private const int GAMELIST_UI = 16;
	private int UIStatus = 1;

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	private NetworkPlayer[] playerList;
	private int playerCount;
	

	
	private void StartServer()
	{
		//MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		if(Network.isServer)
			nView.RPC("updatePlayerList",RPCMode.All);

        UIStatus = LOBBYHOST_UI;
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList (typeName);
		UIStatus = GAMELIST_UI;
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
			
		
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		UIStatus = LOBBY_UI;
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
	}
	
	void OnGUI()
	{
		/*START MENU*/
		if (UIStatus == START_UI)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Create Game"))
				StartServer();
			if (GUI.Button(new Rect(100, 300, 250, 100), "Join Game"))
			{
				RefreshHostList();
			}
		}
		
		/*GAME LIST MENU*/
		if (hostList != null && UIStatus == GAMELIST_UI)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				if (GUI.Button(new Rect(100, 100+(110*i), 250, 100), hostList[i].gameName))
					JoinServer(hostList[i]);
			}
			if (GUI.Button (new Rect(100, 500, 250, 100), "Back"))
				UIStatus = START_UI;
		}
		
		/*LOBBY MENU*/
		if (UIStatus == LOBBY_UI)
		{
			UIStatus = NONE_UI;
		}
		
		/*LOBBY MENU AS HOST*/
		if (UIStatus == LOBBYHOST_UI)
		{
			if (GUI.Button(new Rect(100, 300, 250, 100), "Start Game"))
			{
				UIStatus = NONE_UI;
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
        mapCreator.SetSpawnLocations();
        
		if(Network.isServer)
			nView.RPC("updateMatchStatus",RPCMode.All, true);
		SpawnPlayer ();
	}
	
	private void SpawnPlayer()
	{
		Network.Instantiate (playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	
	
}
