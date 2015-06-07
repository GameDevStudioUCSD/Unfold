using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Server will network initialize the maze.
/// RPC call to all players to spawn their players
/// 
/// </summary>

public class NetworkManager : MonoBehaviour {

	public GameObject playerPrefab;
    public GameObject spawnSquare;
	public GameObject mazeGenerator;
	public GameObject skPrefab;
    public GameObject networkError;
    public bool isGameScene = false;

	private MazeGeneratorController mapCreator;
    private NetworkView nView;
    private bool loadMainMenu;
    private float timer = 9000000000000;
    
    void Start()
    {
        /* Check if single or multiplayer */
        if(Network.isServer && isGameScene)
        {
            /* So we can use RPC calls */
            nView = GetComponent<NetworkView>();

            BuildMaze(true);
        }
        if( skPrefab != null )
		    DontDestroyOnLoad(skPrefab);
    }
	
	
	private void BuildMaze(bool isMultiplayer)
	{
        /* Generates the maze */
		mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
            
		mapCreator.Start();
        GameObject connectionInfo = GameObject.Find("GameType");
        connectionInfo.GetComponent<MazeType>().DisconnectMasterServer();

		mapCreator.createWalls();
        mapCreator.SetSpawnLocations();

        /* Get the starting point */
        Square start = mapCreator.getStartSquare();
        Vector3 spawnPoint = new Vector3(start.getRow() * mapCreator.wallSize, -0.25f, start.getCol() * mapCreator.wallSize);

        if(isMultiplayer)
        {
            /* Create the spawning square */
            Network.Instantiate(spawnSquare, spawnPoint, Quaternion.identity, 0);

            /* Spawn the player multiplayer */
            nView.RPC("SpawnPlayer", RPCMode.All);
        }
        else if(!isMultiplayer)
        {
            /*Spawn the player non-multiplayer*/
			GameObject player = (GameObject) Instantiate(playerPrefab, spawnPoint, Quaternion.identity);

            player.GetComponentInChildren<PlayerCharacter>().setSpawn(spawnPoint);
        }
	}

	[RPC]
	private void SpawnPlayer()
	{
        /* Find the spawning square in the hierarchy */
        GameObject spawningSquare = GameObject.Find("Spawning Square(Clone)");

        /* Get player number */
        int playerNumber = int.Parse(Network.player.ToString());

        /* Get the transform of the spawn point pertaining to this player's number */
        Transform spawnTransform = spawningSquare.transform.GetChild(playerNumber);

        /* Get vector3 of the spawnPoint */
        Vector3 spawnLocation = new Vector3(spawnTransform.position.x, 1.5f, spawnTransform.position.z);

        /* Instantiate player */
		GameObject player = (GameObject) Network.Instantiate (playerPrefab, spawnLocation, Quaternion.identity, 0);

        /* Remove the "Clone" from the player's name */
        MazeGeneratorController.RemoveCloneFromName(player);

        /* Assign the player's number to their name */
        player.name = player.name + " " + (playerNumber+1).ToString();

		/* Causes a null pointer exception. Commenting out to prevent it right now */
		/* TODO: Fix this! */
		// skPrefab.GetComponent<ScoreKeeper> ().addPlayer(player.name);
        
		/* TODO: this doesn't work either */
		// player.GetComponentInChildren<PlayerCharacter>().data.name = player.name; 

        /* Set the player's spawn location */
		player.GetComponentInChildren<PlayerCharacter> ().setSpawn (spawnLocation);
	}
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if(info == NetworkDisconnection.LostConnection)
        {
            timer = Time.time + 3;
            loadMainMenu = true;
            GameObject errorMsg;
            errorMsg = (GameObject)Instantiate(networkError);
            ErrorText errorText = errorMsg.GetComponent<ErrorText>();
            errorText.SetErrorText("Lost connection to the server!");
        }
    }
    void Update()
    {
        if (Time.time > timer && loadMainMenu)
        {
            MiscFunctions func = new MiscFunctions();
            func.Load("MainMenu");
        }
    }
}
