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
	
	protected static float frameX = w * (1 - wRatio) / 2;
	protected static float frameY = h * (1 - hRatio) / 2;

	// Only used in HUD class for now
	protected static float margin = frameWidth / 10;
}
