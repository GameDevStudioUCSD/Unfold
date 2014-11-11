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
public class Maze_Generator_Controller : MonoBehaviour {
    const int NORTH = 0;
    const int SOUTH = 1;
    const int EAST = 2;
    const int WEST = 3;
    //Define new algorithm types here
    const int DepthFirst = 0;

    public int Rows = 10;
    public int Cols = 10;
    public int wallSize = 10;
    public int algorithm = DepthFirst;
    public GameObject NorthWall, SouthWall, EastWall, WestWall, Player, ExitMarker;


    private Square[,] walls;
    private Square exit;
    private MazeGenerator generator;
	// Use this for initialization
	void Start () {
        //Debug.Log("Started script");
        //Creates the walls matrix
        walls = new Square[Rows,Cols];
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                walls[r, c] = new Square(r, c);
            }
        }
        // Add new algorithm cases here
        switch(algorithm)
        {
            case DepthFirst:
                generator = new DepthFirstMazeGenerator(Rows, Cols);
                break;
            default:
                Debug.LogError("Algorithm not defined");
                return;
        }
                    generator.run(walls, exit);
        createWalls();
	
	}
    
    // Creates the walls flagged for creation
    void createWalls()
    {
        Stack children = new Stack();
        GameObject child;
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                Square curr = walls[r, c];
                if(curr.hasNorth)
                    children.Push(Instantiate(NorthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity));
                if (curr.hasSouth)
                    children.Push(Instantiate(SouthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity));
                if (curr.hasEast)
                    children.Push(Instantiate(EastWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity));
                if (curr.hasWest)
                    children.Push(Instantiate(WestWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity));
                if (curr.start)
                    Instantiate(Player, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity);
                if(curr.exit)
                    children.Push(Instantiate(ExitMarker, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity));
                while (children.Count > 0)
                {
                    child = (GameObject)children.Pop();
                    child.transform.parent = transform;
                }
            }
        }
        
    }
}
