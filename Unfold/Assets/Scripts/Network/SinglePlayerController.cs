using UnityEngine;
using System.Collections;

/**
 * Filename: SinglePlayerController.cs
 * Author: Michael Gonzalez
 * Description: This script will attach to the "Yes Button" in the SinglePlayer
 *              GUI menu. It will setup the lambda functions responsible for 
 *              object instantiation to work without a network connection 
 *              present. Furthermore, it will close all other main menu GUI 
 *              elements and place the player inside the Start Game lobby
 */
public class SinglePlayerController  : MonoBehaviour {
    // Holds a reference to the Book UI's start button
    GameObject createGamePanelController;
    // Holds a reference to the StartButton's CreateGamePanel script 
    CreateGame createGameScript;
    // Holds a reference to the GameType gameObject
    GameObject gameType;
    // Holds a reference to the MazeType script
    MazeType mazeTypeScript;

    public bool isDebugging = true;

    /** Finds the CreateGamePanelController in the game scene and prepares a 
     * reference to its CreateGame Script
     */
    private CreateGame GetCreateGameScript()
    {
        createGamePanelController = GameObject.Find("CreateGamePanelController");
        if (createGamePanelController == null)
        {
            Debug.LogError("Null Reference Exception: Could not find CreateGamePanelController!");
            return null;
        }
        createGameScript = createGamePanelController.GetComponent<CreateGame>();
        if (isDebugging)
        {
            Debug.Log("Start Button Reference: " + createGamePanelController);
            Debug.Log("CreateGame Script reference: " + createGameScript);
        }
        return createGameScript;
    }
    private MazeType GetMazeTypeScript()
    {
        gameType = GameObject.Find("GameType");
        if (createGamePanelController == null)
        {
            Debug.LogError("Null Reference Exception: Could not find GameType!");
            return null;
        }
        mazeTypeScript = gameType.GetComponent<MazeType>(); 
        if (isDebugging)
        {
            Debug.Log("GameType Reference: " + gameType);
            Debug.Log("MazeType Script reference: " + mazeTypeScript);
        }
        return mazeTypeScript;
    }

    public void StartSinglePlayerGame()
    {
        // Get pre-existing control script references
        GetCreateGameScript();
        GetMazeTypeScript();
        // Set the game type to SinglePlayer
        mazeTypeScript.SetSinglePlayer(true);
        createGameScript.EnterGame();
    }
	
}
