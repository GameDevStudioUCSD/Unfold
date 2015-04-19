using UnityEngine;
using System.Collections;

/**
 * Game selection screen that appears when player hits
 * "Play" on the main menu screen. A GUI subclass.
 */
public class GameSelectionGUI : AbstractGUI {

    public bool debug_On = false;
    public GameObject loadRecMaze;
    public GameObject loadDepthFirstMaze;
    public GameObject loadMainMenu;

    private static float yLocRecursiveButton = yLocButton1;
    private static float yLocDFButton = yLocButton3;

    Rect recursiveMaze = new Rect(frameX + marginX, yLocRecursiveButton + marginY, xSizeMenuButton, ySizeMenuButton);
    Rect depthFirstMaze = new Rect(frameX + marginX, yLocDFButton + marginY, xSizeMenuButton, ySizeMenuButton);
    
	// Use this for initialization
    void OnGUI()
    {
        if (debug_On)
        	Debug.Log("Height " + hRatio + " Width " + wRatio);
        	
        GUI.Box(gameFrame, "Mode Selection");

        //Results screen buttons
        if (GUI.Button(recursiveMaze, "Recursive\nMaze"))
        {
			Instantiate(loadRecMaze, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
		if (GUI.Button(depthFirstMaze, "Depth First\nMaze"))
		{
			Instantiate(loadDepthFirstMaze, new Vector3(0,0,0), Quaternion.identity);
		}

    }
	

}
