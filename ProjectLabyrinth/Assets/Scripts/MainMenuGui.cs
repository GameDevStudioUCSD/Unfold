using UnityEngine;
using System.Collections;

public class MainMenuGui : MonoBehaviour {
    public static float wRatio = .666f;
    public static float hRatio = .5f;

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
    Rect playAgain = new Rect(frameX + 50, frameHeight/2 + frameY, 100, 100);
    Rect mainMenu = new Rect(frameWidth + frameX - 150, frameHeight / 2 + frameY, 100, 100);
    
	// Use this for initialization
    void OnGUI()
    {
        Debug.Log("Height " + hRatio + " Width " + wRatio);
        GUI.Box(gameFrame, "Main Menu");

        //Results screen buttons
        GUI.Button(playAgain, "Play Again");
        
        GUI.Button(mainMenu, "Main Menu");

    }
	

}
