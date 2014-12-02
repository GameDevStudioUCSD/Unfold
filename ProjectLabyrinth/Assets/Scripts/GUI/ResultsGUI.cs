using UnityEngine;
using System.Collections;

public class ResultsGUI: AbstractGUI {
    public GameObject loadPlay;
    public GameObject loadMainMenu;
    public Texture squidNormal;
    public Texture squidWink;

    /* For the results screen
     * Still need to set padding, height, width of buttons 
     * since they are currently hardcoded
     */
    private bool winking = false;
    private static float yLocPlayButton = yLocButton1;
    private static float yLocMenuButton = yLocButton3;
    private static float yLocSquid = yLocButton2;

    
    Rect playAgain = new Rect(frameX + marginX, yLocPlayButton + marginY, xSizeMenuButton, ySizeMenuButton);
    Rect mainMenu = new Rect(frameX + marginX, yLocMenuButton + marginY, xSizeMenuButton, ySizeMenuButton);
    Rect squid;
	// Use this for initialization
    void Start()
    {
        float sWidth = squidNormal.width;
        float sHeight = squidNormal.height;
        squid = new Rect(frameX + frameWidth / 2 - 22, yLocSquid + marginY, sWidth, sHeight);
    }
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
        
        if( winking )
            GUI.Label(squid, squidWink);
        if( !winking )
            GUI.Label(squid, squidNormal);
    }

    void Update()
    {
        if (Mathf.Floor(Time.time) % 10 == 0)
        {
            winking = !winking;
        }
    }
}
