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
    private const int CONNECT_UI = 3;
	private const int LOBBYHOST_UI = 6;
	private const int LOBBY_UI = 10;
	private const int GAMELIST_UI = 16;
	private int UIStatus = 1;

	private const string typeName = "Project_Labyrinth_512658";
	private const string gameName = "RoomName";
    private int maxPlayerCount = 4;
	private int playerCount = 0;

    /*Only used for Unity masterserver
    private HostData[] hostList;
    private NetworkPlayer[] playerList;
     */

    /*Used for connecting to server*/
    private string playerName = "Default Player";
    private string ipAddress = "127.0.0.1";
    private int portNumber = 26500;

    /*Will hold the names of the players who have connected*/
    private string[] connectedPlayerNames;

    /*Used for the scroll view that will display connected players in lobby*/
    private Vector2 scrollPosition = Vector2.zero;

	
	private void StartServer()
	{
		//MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer (maxPlayerCount, portNumber, !Network.HavePublicAddress());

        connectedPlayerNames = new string[maxPlayerCount];

        /*Only use if we're using Unity master server
		MasterServer.RegisterHost(typeName, gameName);
         */

        /*The first player who connected is the server with default name*/
        connectedPlayerNames[playerCount] = "The_Server";

        /*Player count will be used as array index for connectedPlayerNames array*/
        playerCount++;

        /*Not using this RPC call because server doesn't need it right now
		if(Network.isServer)
			nView.RPC("updatePlayerList",RPCMode.AllBuffered, "The_Server");
         */

        UIStatus = LOBBYHOST_UI;
	}
	
    /* Only used for Unity masterserver
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
     */
	
	void OnConnectedToServer()
	{
		if (debug_On)
			Debug.Log ("Server Joined");

        /*
        * When the player connects, updatePlayerList is called for everyone.
        * The player's name is also sent to everyone.
        * If you want only the server to receive the RPC, then change RPCMode.
        */
        networkView.RPC("updatePlayerList", RPCMode.Server, playerName);
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
                UIStatus = CONNECT_UI;
			}
		}
		
		/*GAME LIST MENU
         * Only useful if using Unity masterserver
		if (hostList != null && UIStatus == GAMELIST_UI)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				if (GUI.Button(new Rect(100, 100+(110*i), 250, 100), hostList[i].gameName))
					JoinServer(hostList[i]);
			}
			if (GUI.Button (new Rect(100, 500, 250, 100), "Back"))
				UIStatus = START_UI;
		}*/
		
		/*LOBBY MENU*/
		if (UIStatus == LOBBY_UI)
		{
			UIStatus = NONE_UI;
		}

        /*Connect to server menu*/
        if (UIStatus == CONNECT_UI)
        {
            GUILayout.Label("Player Name:");
            playerName = GUILayout.TextField(playerName);

            GUILayout.Space(5);

            //User can type in IP address for the server
            GUILayout.Label("IP Address:");
            ipAddress = GUILayout.TextField(ipAddress);

            GUILayout.Space(5);

            GUILayout.Label("Port Number:");
            portNumber = int.Parse(GUILayout.TextField(portNumber.ToString()));

            GUILayout.Space(5);

            if (GUILayout.Button("Connect", GUILayout.Height(50)))
            {
                //Check if user has empty playerName
                if (playerName == "")
                {
                    //Player has empty name do something about it...
                    Debug.Log("ERROR: EMPTY PLAYER NAME");
                }
                else
                {

                    //Connect to a server
                    //User IP address in ipAddress
                    //Use port in portNumber
                    Network.Connect(ipAddress, portNumber);

                    //Goes to OnConnectedToServer()
                    
                }

            }

            GUILayout.Space(5);

            /*Goes back to menu with start server/connect*/
            if (GUILayout.Button("Back", GUILayout.Height(50)))
            {
                UIStatus = START_UI;
            }
        }
		
		/*LOBBY MENU AS HOST*/
		if (UIStatus == LOBBYHOST_UI)
		{

            /*Label shows how many Players are currently connected*/
            GUI.Label(new Rect(0, 0, 200, 30), "Number of Players: " + (playerCount));

            /*Scroll view contains names of all the players connected*/
            scrollPosition = GUI.BeginScrollView(new Rect(50, 50, 150, 200), scrollPosition, new Rect(0, 0, 125, 300));
            for(int i = 0; i < maxPlayerCount; i++)
            {
                GUI.Label(new Rect(20, 30 + 20 * i, 100, 50), connectedPlayerNames[i]);
            }
            GUI.EndScrollView();

			if (GUI.Button(new Rect(100, 300, 250, 100), "Start Game"))
			{
                if (debug_On)
                    Debug.Log("Start Game button clicked");
				UIStatus = NONE_UI;
				StartMatch ();
			}
		}
	}
	
	[RPC]
	private void updatePlayerList(string playerName)
	{
		//playerList[playerCount] = Network.player;
		Debug.Log("Player Count: " + playerCount);

        connectedPlayerNames[playerCount] = playerName;
        playerCount++;
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
        if (debug_On)
            Debug.Log("Tried to start match\nGetting MazeGeneratorController script");
		mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
        if (debug_On)
            Debug.Log("Running Start() on MazeGeneratorController");
		mapCreator.Start();
        if (debug_On)
            Debug.Log("Running createWalls() on MazeGeneratorController");
		mapCreator.createWalls();
        if (debug_On)
            Debug.Log("Running SetSpawnLocations() on MazeGeneratorController");
        mapCreator.SetSpawnLocations();
        
		if(Network.isServer)
			nView.RPC("updateMatchStatus",RPCMode.All, true);
		SpawnPlayer ();
	}
	
	private void SpawnPlayer()
	{
		Network.Instantiate (playerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
	}
	
	
}
