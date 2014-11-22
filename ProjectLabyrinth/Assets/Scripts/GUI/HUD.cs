using UnityEngine;
using System.Collections;

/**
 * Pause menu screen that persists when player is in
 * the maze game. A GUI subclass.
 */
public class HUD : AbstractGUI {

	private static float buttonOffset = frameHeight / 4;
	private static float sizePauseButton = Screen.width / 10;
	private static float xSizeMenuButton = frameWidth - (2*margin);
	private static float ySizeMenuButton = buttonOffset - margin;
	private static float xLocPauseButton = Screen.width - sizePauseButton;
	private static float yLocResumeButton = frameY + buttonOffset;
	private static float yLocOptionsButton = frameY + (buttonOffset * 2);
	private static float yLocExitButton = frameY + (3 * buttonOffset);
	private bool isPaused;
	static Rect pauseButton = new Rect(xLocPauseButton, 0, sizePauseButton, sizePauseButton);
	static Rect resumeButton = new Rect(frameX + margin, yLocResumeButton + margin, xSizeMenuButton, ySizeMenuButton);
	static Rect optionsButton = new Rect(frameX + margin, yLocOptionsButton + margin, xSizeMenuButton, ySizeMenuButton);
	static Rect exitButton = new Rect(frameX + margin, yLocExitButton + margin, xSizeMenuButton, ySizeMenuButton);
	static Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight);
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
