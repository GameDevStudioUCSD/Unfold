using UnityEngine;
using System.Collections;
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
    const int NORTH = 0;
    const int SOUTH = 1;
    const int EAST = 2;
    const int WEST = 3;
    //Define new algorithm types here
    public const int DepthFirst = 0;
    public const int Recursive = 1;


    public int Rows = 20;
    public int Cols = 20;
    public float wallSize = 10;
    public enum AlgorithmChoice
    {
        DepthFirst,
        Recursive
    };
    
    public AlgorithmChoice algorithm;
    public TextureController.TextureChoice levelType;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Floor, Player, ExitMarker, DebugSphere;
    public GameObject[] spawnList;
    public bool debug_ON = false;

    private Square[,] walls;
    private Square exit;
    private Square start;
    private Square curr;
    private MazeGenerator generator;
    public MazeGeneratorController(AlgorithmChoice algorithm)
    {
        algorithm = this.algorithm;
    }
	// Use this for initialization
	public void Start () {
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        
        // Add new algorithm cases here
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
        Debug.Log ("ASDF");
	
	}
    
    // Creates the walls flagged for creation
    public void createWalls()
    {
    	Debug.Log ("TEST");
        Stack children = new Stack();
        TextureController textureController = new TextureController(levelType);
        GameObject child;
        Transform trans;
        Renderer wallRenderer;
        child = (GameObject)Network.Instantiate(Floor, new Vector3((Rows*wallSize/2), 0 , (Cols*wallSize/2)), Quaternion.identity, 0);
        child.transform.localScale  += new Vector3((wallSize*Rows/10), 0, (wallSize*Cols/10));
        child.GetComponent<Renderer>().material.mainTexture = textureController.GetFloorTexture();
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                curr = walls[r, c];
                if(curr.hasNorth)
                    children.Push(Network.Instantiate(NorthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasSouth)
                    children.Push(Network.Instantiate(SouthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasEast)
                    children.Push(Network.Instantiate(EastWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasWest)
                    children.Push(Network.Instantiate(WestWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.start)
                {
                    start = curr;
                }
                if(curr.exit)
                    children.Push(Instantiate(ExitMarker, new Vector3(curr.getRow() * wallSize, 0, wallSize * curr.getCol()), Quaternion.identity));
                while (children.Count > 0)
                {
                    child = (GameObject)children.Pop();
                    trans = child.transform.Find("InnerWall");
                    if(trans != null)
                    {
                        if (debug_ON)
                            Debug.Log("Trying to set texture at: " + r + ", " + c);
                        wallRenderer = trans.gameObject.GetComponent<Renderer>();
                        wallRenderer.material.mainTexture = textureController.GetRandomWall();
                    }
                    
                    //child.transform.parent = transform;
                    child.name = child.name.Replace("(Clone)", "");
                }
            }
        }
        
    }
    public void SetSpawnLocations()
    {
        ArrayList corridors = CorridorFinder.FindCorridors(walls, Rows, Cols);
        Square curr;
        for (int i = 0; i < corridors.Count; i++)
        {
            curr = (Square)corridors[i];
            GameObject monsterToSpawn = Spawner.NextSpawn(curr, spawnList);
            int r = curr.getRow();
            int c = curr.getCol();
            if(debug_ON)
                Network.Instantiate(DebugSphere, new Vector3(r * wallSize, 10, c * wallSize), Quaternion.identity, 0);
            if (monsterToSpawn != null)
            {
				float monsterHeight = 0;
				if(monsterToSpawn.GetComponent<BirdMovement> () != null)
				{
					monsterHeight = 4;
				}

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

    public void createPlayer()
    {
    	//GameObject child;
		/*child = (GameObject) Network.Instantiate(Player, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity, 0);
		//child.name = child.name.Replace("(Clone)", "");*/
    }
}
