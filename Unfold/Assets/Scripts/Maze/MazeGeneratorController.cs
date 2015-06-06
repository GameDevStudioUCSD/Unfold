using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* @author: Michael Gonzalez
 * This class is used directly in the Unity Editor to generate the maze. To use 
 * it, just attach the script component to a blank Unity Game Object.
 * @param Rows - The number of rows to generate
 * @param Cols - The number of columns to generate
 * @param wallSize - The size of each individual wall. This ensures spacing is
 *                   accurate when the maze is generated.
 * @param algorithm - Defines the algorithm to be used by the controller.
 * @param Player - Player controller to be placed at the start of the maze.
 *                 (CURRENTLY ONLY WORKS WITH ONE GAMEOBJECT)
 * @param exit - GameObject to signify the end of the maze
 */
public class MazeGeneratorController : MonoBehaviour {

	// Define new algorithm types here
	public enum AlgorithmChoice {
		DepthFirst,
		Recursive
	};

    public int Rows = 20;
    public int Cols = 20;
    public float wallSize = 10;
    public float wallHeight = 10;
    public AlgorithmChoice algorithm;
    public TextureController.TextureChoice levelType;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Floor, Ceiling, Player, ExitMarker;
    public GameObject[] spawnList;
	public FogOfWar fogOfWar;
    public bool debug_ON = false;
    [Range (1, 100)]
    public int spawningRate = 1;

    private Square[,] walls;
    private Square exit;
    private Square start;
    private Square curr;
    private MazeGenerator generator;
    private SortedDictionary<string, GameObject> westWalls;
    private GameObject innerWall;
    private GameObject gameTypeObj;

	public MazeInfo mi;
    public Spawner spawner;

	// Use this for initialization
	public void Start () {
        // If the player is a host, try and set the game type to their choice
        gameTypeObj = GameObject.Find("GameType");
        if(gameTypeObj != null)
        {
            levelType = gameTypeObj.GetComponent<MazeType>().GetGameType();
        }
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        
        // Add new algorithm cases here
        switch (levelType)
        {
            case TextureController.TextureChoice.Cave:
                algorithm = AlgorithmChoice.Recursive;
                spawningRate *= 50;
                break;
        }
        switch(algorithm)
        {
            case AlgorithmChoice.DepthFirst:
                generator = new WorkingDepthFirstMazeGenerator(Rows, Cols);
                break;
            case AlgorithmChoice.Recursive:
                generator = new RecursiveMaze(Rows, Cols);
                break;
            default:
                Debug.LogError("Algorithm not defined");
                return;
        }
        //generator = new WorkingDepthFirstMazeGenerator(Rows,Cols);
        generator.run(walls, exit);

		mi.walls = walls;
		mi.wallSize = wallSize;
		mi.exists = true;
	}
    // Removes duplicate walls from adjacent cells
    public void DetermineWallsToSpawn()
    {
        for( int c = 0; c < Cols; c++ )
        {
            for( int r = 0; r < Rows; r++)
            {
                curr = walls[r, c];
                if(curr.hasEast && c < Cols-1)
                {
                    walls[r, c + 1].hasWest = true;
                    curr.hasEast = false;
                }
                if(curr.hasSouth && r < Rows-1)
                {
                    walls[r + 1, c].hasNorth = true;
                    curr.hasSouth = false;
                }
            }
        }
    }
    public void DetermineLogicalWallCount()
    {
        for (int c = 1; c < Cols; c++)
        {
            for (int r = 1; r < Rows; r++)
            {
                curr = walls[r, c];
                if (curr.hasWest)
                {
                    walls[r, c - 1].hasEast = true;
                }
                if (curr.hasNorth)
                {
                    walls[r - 1, c].hasSouth = true;
                }
            }
        }
    }
    
    public void FixWallIssues()
    {
        // The cells directly to the south and south east to curr
        Square sCell, swCell;
        NetworkView wallNView;
        bool inBounds;
        for (int c = 0; c < Cols; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                curr = walls[r, c];
                if (r < Rows - 1)
                    sCell = walls[r + 1, c];
                else
                    sCell = curr;
                if (c > 0 && r < Rows - 1)
                    swCell = walls[r + 1, c - 1];
                else
                    swCell = curr;
                //First, fix any flickering issues
                if(curr.hasWest && curr.hasNorth)
                {
                    if(debug_ON)
                    {
                        Debug.Log("Tried to fix flickering");
                    }
                    if (westWalls.TryGetValue(curr.ToString(), out innerWall))
                    {
                        wallNView = innerWall.GetComponent<NetworkView>();
                        wallNView.RPC("ShrinkWall", RPCMode.AllBuffered);
                    }
                }
                inBounds = (r < Rows - 1 && c > 0);
                if (inBounds && curr.hasWest && !sCell.hasNorth && !sCell.hasWest && swCell.hasNorth)
                {
                    if(debug_ON)
                    {
                        Debug.Log("Found a missing corner!");
                    }
                    if(westWalls.TryGetValue(curr.ToString(), out innerWall))
                    {
                        wallNView = innerWall.GetComponent<NetworkView>();
                        wallNView.RPC("ExpandWall", RPCMode.AllBuffered);
                    }
                }
                
            }
        }
    }

    // Creates the walls flagged for creation
    public void CreateFloor()
    {
        GameObject floor;
        GameObject ceiling;
        NetworkView nViewFloor, nViewCeiling;
        Vector3 position = new Vector3((Rows*wallSize/2), 0 , (Cols*wallSize/2));
		Vector3 roofPosition = new Vector3((Rows*wallSize/2), wallHeight , (Cols*wallSize/2));
        floor = (GameObject)Network.Instantiate(Floor, position, Quaternion.identity, 0);
        ceiling = (GameObject)Network.Instantiate (Ceiling, roofPosition, Quaternion.identity, 0);
        ceiling.transform.Rotate(0, 0, 180);
        RemoveCloneFromName(floor);
        RemoveCloneFromName(ceiling);
        nViewFloor = floor.GetComponent<NetworkView>();
		nViewCeiling = ceiling.GetComponent<NetworkView>();	
        nViewFloor.RPC( "ModifyFloorSize", RPCMode.AllBuffered, wallSize, Rows, Cols );
        nViewFloor.RPC("UpdateTexture", RPCMode.AllBuffered, (int)levelType);
        nViewCeiling.RPC( "ModifyCeilingSize", RPCMode.AllBuffered, wallSize, Rows, Cols );
        nViewCeiling.RPC("UpdateTexture", RPCMode.AllBuffered, (int)levelType);
    }
    public void ApplyWallTexture( GameObject currentWall)
    {
        NetworkView nView = currentWall.GetComponent<NetworkView>();
        nView.RPC("UpdateTexture", RPCMode.AllBuffered, (int)levelType);
    }

    /**
     * This method will create an indestructable outer wall layer so players
     * cannot leave the maze.
     */
    public void CreateOuterWall()
    {
        // The position where this wall should be placed
        Vector3 pos;
        Quaternion rot = Quaternion.identity;
        // The current wall being created
        Stack outerWalls = new Stack();
        for (int i = 1; i <= 2; i++)
        {

            // Create the east and west walls
            for (int r = 0; r < Rows; r++)
            {
                pos = new Vector3(r * wallSize, 1, -1 * i);
                outerWalls.Push(Network.Instantiate(WestWall, pos, rot, 0));
                pos = new Vector3(r * wallSize, 1, (Cols - 1) * wallSize + i);
                outerWalls.Push(Network.Instantiate(EastWall, pos, rot, 0));
            }
            // Create the north and south walls
            for (int c = 0; c < Cols; c++)
            {
                pos = new Vector3(-1 * i, 1, c * wallSize);
                outerWalls.Push((GameObject)Network.Instantiate(NorthWall, pos, rot, 0));
                pos = new Vector3((Rows - 1) * wallSize + i, 1, c * wallSize);
                outerWalls.Push(Network.Instantiate(SouthWall, pos, rot, 0));
            }
        } 
        NetworkView nView;
        while (outerWalls.Count > 0)
        {
            nView = ((GameObject)outerWalls.Pop()).GetComponent<NetworkView>();
            nView.RPC("MakeIndestructable", RPCMode.AllBuffered);
        }
    }
    public void createWalls()
    {
        CreateOuterWall();
        DetermineWallsToSpawn();
        Stack instantiationList = new Stack();
        //TextureController textureController = new TextureController(levelType);
        GameObject objToInstantiate = null;
        Vector3 pos;
        Quaternion rot = Quaternion.identity;
        westWalls = new SortedDictionary<string, GameObject>();
        CreateFloor();
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                curr = walls[r, c];
                pos = new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol());
				if (this.fogOfWar != null) {
					objToInstantiate = (GameObject)Network.Instantiate(this.fogOfWar.gameObject, pos + 9 * Vector3.up, transform.rotation, 0);
					objToInstantiate.transform.Rotate (90,0,0);
					instantiationList.Push(objToInstantiate);
				}
                if(curr.hasNorth)
                {
                    objToInstantiate = (GameObject) Network.Instantiate(NorthWall, pos, rot , 0);
                    ApplyWallTexture(objToInstantiate);
                    instantiationList.Push(objToInstantiate);
                }
                if (curr.hasSouth)
                {
                    objToInstantiate = (GameObject)Network.Instantiate(SouthWall, pos, rot, 0);
                    ApplyWallTexture(objToInstantiate);
                    instantiationList.Push(objToInstantiate);
                }
                if (curr.hasEast)
                {
                    objToInstantiate = (GameObject)Network.Instantiate(EastWall, pos, rot, 0);
                    ApplyWallTexture(objToInstantiate);
                    instantiationList.Push(objToInstantiate);
                }
                if (curr.hasWest)
                {
                    objToInstantiate = (GameObject)Network.Instantiate(WestWall, pos, rot, 0);
                    ApplyWallTexture(objToInstantiate);
                    instantiationList.Push(objToInstantiate);
                    westWalls.Add(curr.ToString(), objToInstantiate);
                }
                if (curr.start)
                {
                    start = curr;
                    
                }
                if(curr.exit)
                {
                    objToInstantiate = (GameObject)Network.Instantiate(ExitMarker, pos, rot, 0);
                    instantiationList.Push(objToInstantiate);
                }
                
                while (instantiationList.Count > 0)
                {
                    objToInstantiate = (GameObject)instantiationList.Pop();
                    RemoveCloneFromName(objToInstantiate);
                }
            }
        }
        FixWallIssues();
        DetermineLogicalWallCount();
        
    }
    public static void RemoveCloneFromName(GameObject obj)
    {
        obj.name = obj.name.Replace("(Clone)", "");
    }
    public void SetSpawnLocations()
    {
        ArrayList corridors = CorridorFinder.FindCorridors(walls, Rows, Cols);
        Square curr;
        for (int i = 0; i < corridors.Count; i++)
        {
            curr = (Square)corridors[i];
            GameObject monsterToSpawn = spawner.NextSpawn(curr, spawnList, 100 - spawningRate, levelType);
            int r = curr.getRow();
            int c = curr.getCol();
            if (monsterToSpawn != null)
            {
				float monsterHeight = 0;
				if(monsterToSpawn.GetComponent<BirdMovement> () != null)
				{
					monsterHeight = 6;
				}
                if(!curr.start)
                    Network.Instantiate(monsterToSpawn, new Vector3(r * wallSize, monsterHeight, c * wallSize), Quaternion.identity, 0);
            }
        }
        
    }
    public Square getStartSquare()
    {
        return start;
    }

	public Square[,] getWalls()
	{
		return walls;
	}
}