using System;
using UnityEngine;

/**
 * Abstract GUI class
 *
 * Responsible for defining a few dimensions of various GUI objects
 * and screens loaded in Unity.
 */
public abstract class AbstractGUI : IPlayerWindow {
	protected static float buttonOffset = frameHeight / 4;

	protected static float xSizeMenuButton = frameWidth - (2 * marginX);
	protected static float ySizeMenuButton = buttonOffset - marginY;

	protected static float yLocButton1 = frameY + buttonOffset;
	protected static float yLocButton2 = frameY + (buttonOffset * 2);
	protected static float yLocButton3 = frameY + (buttonOffset * 3);

	// Only used in HUD class for now
	protected static Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight + marginY);
}
