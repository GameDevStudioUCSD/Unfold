using UnityEngine;
using System.Collections;

public class GameSelectionGUI : MonoBehaviour {
    

    public GameObject loadRecMaze;
    public GameObject loadDepthFirstMaze;
    public GameObject loadMainMenu;
    static float wRatio = .666f;
    static float hRatio = .5f;
    static float w = Screen.width;
    static float h = Screen.height;
    static float frameWidth = w * wRatio;
    static float frameHeight = h * hRatio;

    static float frameX = w * (1 - wRatio) / 2;
    static float frameY = h * (1 - hRatio) / 2;



   //Instantiated gameFrame for Main Menu
    Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight);

    /* For the results screen
     * Still need to set padding, height, width of buttons 
     * since they are currently hardcoded
     */
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
