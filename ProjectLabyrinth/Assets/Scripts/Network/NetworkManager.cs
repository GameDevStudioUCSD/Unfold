using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public bool debug_On;
	public GameObject playerPrefab;
	public GameObject mazeGenerator;

	private MazeGeneratorController mapCreator;

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	

	
	private void StartServer()
	{
		//MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer (4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
        //mapCreator = new MazeGeneratorController(MazeGeneratorController.DepthFirst);
		mapCreator.Start();
		mapCreator.createWalls();
        //Debug.Log(mapCreator.getStartSquare());
		SpawnPlayer();
	}
	
	void OnServerInitialized()
	{
		if (debug_On)
			Debug.Log ("Server Initialized");
		//SpawnPlayer();
	}
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			if (GUI.Button(new Rect(100, 300, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100+(110*i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}	
		}
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
		SpawnPlayer();
	}
	
	private void SpawnPlayer()
	{
		//mapCreator.createPlayer();
		Network.Instantiate (playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
	}
	
	
}
