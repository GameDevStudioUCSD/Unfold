using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Server will network initialize the maze.
/// RPC call to all players to spawn their players
/// 
/// </summary>

public class NetworkManager : MonoBehaviour {

	public bool debug_On;
	public GameObject playerPrefab;
    public GameObject spawnSquare;
	public GameObject mazeGenerator;

	private MazeGeneratorController mapCreator;
    private NetworkView nView;
    
    void Start()
    {
        /* Check if single or multiplayer */
        if(Network.isServer)
        {
            /* So we can use RPC calls */
            nView = GetComponent<NetworkView>();

            BuildMaze(true);
        }

    }
	
	
	private void BuildMaze(bool isMultiplayer)
	{
        /* Generates the maze */
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
        
        /* Set the player's spawn location */
		player.GetComponentInChildren<PlayerCharacter> ().setSpawn (spawnLocation);
	}
	
	
}
