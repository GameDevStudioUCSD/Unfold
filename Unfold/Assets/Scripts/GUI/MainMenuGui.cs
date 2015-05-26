using UnityEngine;
using System.Collections;

public class MainMenuGui : AbstractGUI {
    

    public GameObject loadPlay;
    public GameObject loadPractice;
    public GameObject loadOptions;

    /* For the results screen
     * Still need to set padding, height, width of buttons 
     * since they are currently hardcoded
     */
    private static float yLocPlayButton = yLocButton1;
    private static float yLocPracticeButton = yLocButton2;
    private static float yLocOptionsButton = yLocButton3;

    static Rect playButton = new Rect(frameX + marginX, yLocPlayButton + marginY, xSizeMenuButton, ySizeMenuButton);
    static Rect practiceButton = new Rect(frameX + marginX, yLocPracticeButton + marginY, xSizeMenuButton, ySizeMenuButton);
    static Rect optionsButton = new Rect(frameX + marginX, yLocOptionsButton + marginY, xSizeMenuButton, ySizeMenuButton);
    
	// Use this for initialization
    void OnGUI()
    {
        Debug.Log("Height " + hRatio + " Width " + wRatio);
        GUI.Box(gameFrame, "Main Menu");

        //Results screen buttons
        if (GUI.Button(playButton, "Play!"))
        {
            Instantiate(loadPlay, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        GUI.Button(practiceButton, "Practice");
        GUI.Button(optionsButton, "Options");

    }
	

}
