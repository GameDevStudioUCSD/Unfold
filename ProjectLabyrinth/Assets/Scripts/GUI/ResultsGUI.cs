using UnityEngine;
using System.Collections;

public class ResultsGUI: AbstractGUI {
    public GameObject loadPlay;
    public GameObject loadMainMenu;

    /* For the results screen
     * Still need to set padding, height, width of buttons 
     * since they are currently hardcoded
     */
    private static float yLocPlayButton = yLocButton1;
    private static float yLocMenuButton = yLocButton3;

    Rect playAgain = new Rect(frameX + marginX, yLocPlayButton + marginY, xSizeMenuButton, ySizeMenuButton);
    Rect mainMenu = new Rect(frameX + marginX, yLocMenuButton + marginY, xSizeMenuButton, ySizeMenuButton);
    
	// Use this for initialization
    void OnGUI()
    {
        GUI.Box(gameFrame, "Results");
        //Results screen buttons
        if (GUI.Button(playAgain, "Play Again"))
        {
            Instantiate(loadPlay, new Vector3(0, 0, 0), Quaternion.identity);
        }
        if(GUI.Button(mainMenu, "Main Menu"))
        {
            Instantiate(loadMainMenu, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
