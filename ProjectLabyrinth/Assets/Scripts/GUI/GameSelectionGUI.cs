using UnityEngine;
using System.Collections;

/**
 * Game selection screen that appears when player hits
 * "Play" on the main menu screen. A GUI subclass.
 */
public class GameSelectionGUI : AbstractGUI {

    public GameObject loadRecMaze;
    public GameObject loadDepthFirstMaze;
    public GameObject loadMainMenu;

   //Instantiated gameFrame for Main Menu
    Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight);


    Rect recursiveMaze = new Rect(frameX + 50, frameHeight/2 + frameY, 100, 100);
    Rect depthFirstMaze = new Rect(frameWidth + frameX - 150, frameHeight / 2 + frameY, 100, 100);
    
	// Use this for initialization
    void OnGUI()
    {
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
