using System;
using UnityEngine;

/**
 * Abstract interface for the player window.
 *
 * Sets some basic constants to control certain aspects of the screen.
 */
public interface IPlayerWindow : MonoBehaviour {
	// Sets the ratios for the screen width and height
	protected static float wRatio = .666f;
	protected static float hRatio = .5f;

	// Sets the ratios for the frame width and height
	protected static float frameWidth = Screen.width * wRatio;
	protected static float frameHeight = Screen.height * hRatio;

	// Sets the margins for the frame width and height
	protected static float marginX = frameWidth / 10;
	protected static float marginY = frameHeight / 10;

	// Sets the (x,y) locations for the frame
	protected static float frameX = Screen.width * (1 - wRatio) / 2;
	protected static float frameY = Screen.height * (1 - hRatio) / 2 + marginY;
}