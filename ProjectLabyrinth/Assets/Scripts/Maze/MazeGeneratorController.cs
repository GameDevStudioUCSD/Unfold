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
    public int algorithm = DepthFirst;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Player, ExitMarker, Bird, Spider;

    private Square[,] walls;
    private Square exit;
    private Square start;
    private Square curr;
    private MazeGenerator generator;
    public MazeGeneratorController(int algorithm)
    {
        algorithm = this.algorithm;
    }
	// Use this for initialization
	public void Start () {
        //Debug.Log("Started script");
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        
        // Add new algorithm cases here
        switch(algorithm)
        {
            case DepthFirst:
                generator = new WorkingDepthFirstMazeGenerator(Rows, Cols);
                break;
            case Recursive:
                generator = new RecursiveMaze(Rows, Cols);
                break;
            default:
                Debug.LogError("Algorithm not defined");
                return;
        }
                    generator.run(walls, exit);
        //createWalls();
	
		Square enemy = walls [Random.Range (0, Rows), Random.Range (0, Cols)];
		Network.Instantiate (Bird, new Vector3 (enemy.getRow () * wallSize, 5, enemy.getCol () * wallSize), Quaternion.identity, 0);

		enemy = walls [Random.Range (0, Rows), Random.Range (0, Cols)];
		Network.Instantiate (Spider, new Vector3 (enemy.getRow () * wallSize, 0, enemy.getCol () * wallSize), Quaternion.identity, 0);
	}
    
    // Creates the walls flagged for creation
    public void createWalls()
    {
        Stack children = new Stack();
        GameObject child;
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
                    //child.transform.parent = transform;
                    child.name = child.name.Replace("(Clone)", "");
                }
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
