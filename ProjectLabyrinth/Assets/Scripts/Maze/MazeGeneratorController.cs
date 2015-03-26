﻿using UnityEngine;
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
    [Range (1, 100)]
    public int spawningRate = 1;

    private Square[,] walls;
    private Square exit;
    private Square start;
    private Square curr;
    private MazeGenerator generator;
    private SortedDictionary<string, GameObject> westWalls;
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
        GameObject innerWall;
        Transform wallTransform;
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
                        wallTransform = innerWall.transform.Find("InnerWall");
                        wallTransform.localScale -= Vector3.forward;
                        wallTransform.localPosition += (Vector3.right * (.5f));
                       // continue;
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
                        wallTransform = innerWall.transform.Find("InnerWall");
                        wallTransform.localScale += Vector3.forward;
                        wallTransform.localPosition += (Vector3.right * (.5f));
                    }
                }
                
            }
        }
    }

    // Creates the walls flagged for creation
    public void createWalls()
    {
        DetermineWallsToSpawn();
        Stack children = new Stack();
        TextureController textureController = new TextureController(levelType);
        GameObject child;
        Transform trans;
        Renderer wallRenderer;
        GameObject westWall;
        westWalls = new SortedDictionary<string, GameObject>();
        child = (GameObject)Network.Instantiate(Floor, new Vector3((Rows*wallSize/2), 0 , (Cols*wallSize/2)), Quaternion.identity, 0);
        child.transform.localScale  += new Vector3((wallSize*Rows/10), 0, (wallSize*Cols/10));
        child.GetComponent<Renderer>().material.mainTexture = textureController.GetFloorTexture();
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                if(debug_ON)
                {
                    GameObject indexSphere = (GameObject)Network.Instantiate(DebugSphere, new Vector3(r * wallSize, 15, c * wallSize), Quaternion.identity, 0);
                    float red = ((float)r / Rows);
                    float blue = ((float)c / Cols);
                //    Debug.Log("Red: " + red + " Blue: " + blue);
                    Color indexColor = new Color(red, 0, blue);
                    indexSphere.GetComponent<Renderer>().material.SetColor("_Color", indexColor);

                }
                curr = walls[r, c];
                if(curr.hasNorth)
                    children.Push(Network.Instantiate(NorthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasSouth)
                    children.Push(Network.Instantiate(SouthWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasEast)
                    children.Push(Network.Instantiate(EastWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity,0));
                if (curr.hasWest)
                {
                    westWall = (GameObject)Network.Instantiate(WestWall, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity, 0);
                    westWalls.Add(curr.ToString(), westWall);
                    children.Push(westWall);
                }
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
                        //if (debug_ON)
                            //Debug.Log("Trying to set texture at: " + r + ", " + c);
                        wallRenderer = trans.gameObject.GetComponent<Renderer>();
                        wallRenderer.material.mainTexture = textureController.GetRandomWall();
                    }
                    
                    //child.transform.parent = transform;
                    child.name = child.name.Replace("(Clone)", "");
                }
            }
        }
        FixWallIssues();
        DetermineLogicalWallCount();
        
    }
    public void SetSpawnLocations()
    {
        ArrayList corridors = CorridorFinder.FindCorridors(walls, Rows, Cols);
        Square curr;
        for (int i = 0; i < corridors.Count; i++)
        {
            curr = (Square)corridors[i];
            GameObject monsterToSpawn = Spawner.NextSpawn(curr, spawnList, 100 - spawningRate);
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

    public void createPlayer()
    {
    	//GameObject child;
		/*child = (GameObject) Network.Instantiate(Player, new Vector3(curr.getRow() * wallSize, 1, wallSize * curr.getCol()), Quaternion.identity, 0);
		//child.name = child.name.Replace("(Clone)", "");*/
    }
}
