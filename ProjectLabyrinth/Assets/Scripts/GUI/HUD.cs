using UnityEngine;
using System.Collections;

/**
 * Pause menu screen that persists when player is in
 * the maze game. A GUI subclass.
 */
public class HUD : AbstractGUI {

	//private static float buttonOffset = frameHeight / 4;

	private static float sizePauseButton = Screen.width / 10;
	private static float xLocPauseButton = Screen.width - sizePauseButton;

    private static float yLocResumeButton = yLocButton1;
    private static float yLocOptionsButton = yLocButton2;
    private static float yLocExitButton = yLocButton3;

	private bool isPaused;
	static Rect pauseButton = new Rect(xLocPauseButton, 0, sizePauseButton, sizePauseButton);

	static Rect resumeButton = new Rect(frameX + marginX, yLocResumeButton + marginY, xSizeMenuButton, ySizeMenuButton);
	static Rect optionsButton = new Rect(frameX + marginX, yLocOptionsButton + marginY, xSizeMenuButton, ySizeMenuButton);
	static Rect exitButton = new Rect(frameX + marginX, yLocExitButton + marginY, xSizeMenuButton, ySizeMenuButton);
	//static Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight);
	public GameObject loadMainMenu;

	void OnGUI()
	{
		if (GUI.Button (pauseButton, "PAUSE"))
		{
			isPaused = !isPaused;
		}
		if (isPaused)
		{
			//Debug.Log ("buttonOffset: " + buttonOffset);
			//Debug.Log ("sizePauseButton: " + sizePauseButton);
			//Debug.Log ("sizeMenuButton: " + ySizeMenuButton);

			GUI.Box(gameFrame, "PAUSED");
			if (GUI.Button( resumeButton, "Resume"))
			{
				isPaused = false;
			}
			GUI.Button( optionsButton, "Options");
			if (GUI.Button( exitButton, "Exit"))
			{
				Instantiate(loadMainMenu);
			}
		}
	}
}
