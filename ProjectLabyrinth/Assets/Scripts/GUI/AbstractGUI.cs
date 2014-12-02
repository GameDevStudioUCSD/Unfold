using System;
using UnityEngine;

/**
 * Abstract GUI class
 *
 * Responsible for defining a few dimensions of various GUI objects
 * and screens loaded in Unity.
 */
public abstract class AbstractGUI : MonoBehaviour
{
	protected static float wRatio = .666f;
	protected static float hRatio = .5f;
	protected static float w = Screen.width;
	protected static float h = Screen.height;
	protected static float frameWidth = w * wRatio;
	protected static float frameHeight = h * hRatio;

    protected static float marginX = frameWidth / 10;
    protected static float marginY = frameHeight / 10;

	protected static float frameX = w * (1 - wRatio) / 2;
	protected static float frameY = h * (1 - hRatio) / 2 + marginY;
    
    protected static float buttonOffset = frameHeight / 4;

    protected static float xSizeMenuButton = frameWidth - (2 * marginX);
    protected static float ySizeMenuButton = buttonOffset - marginY;

    protected static float yLocButton1 = frameY + buttonOffset;
    protected static float yLocButton2 = frameY + (buttonOffset * 2);
    protected static float yLocButton3 = frameY + (buttonOffset * 3);

	// Only used in HUD class for now
    protected static Rect gameFrame = new Rect(frameX, frameY, frameWidth, frameHeight + marginY);
}
