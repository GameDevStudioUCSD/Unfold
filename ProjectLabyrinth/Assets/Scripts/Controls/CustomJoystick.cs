using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Class to control game behavior based on user input to the joystick.
///
/// Design based on KumoKairo's CNControls:
/// https://www.assetstore.unity3d.com/en/#!/content/15233
/// https://github.com/KumoKairo/CNJoystick
/// </summary>
public class CustomJoystick : CNAbstractController {

	/// <summary>
	/// The farthest away from the base one can move the stick.
	/// </summary>
	private const float DRAG_RADIUS = 1.5f;
	
	// Runtime used fields
	/// <summary>
	/// Transform component of a stick
	/// </summary>
	private Transform stickTransform;
	/// <summary>
	/// Transform component of a base
	/// </summary>
	private Transform baseTransform;

	/// <summary>
	/// Neat initialization method
	/// </summary>
	public override void OnEnable() {		
		// Getting needed components
		// Hardcoded names. We have no need of renaming these objects anyway
		this.stickTransform = this.transform.FindChild("Stick").GetComponent<Transform>();
		this.baseTransform = this.transform.FindChild("Base").GetComponent<Transform>();
		
		this.stickTransform.gameObject.gameObject.SetActive(true);
		this.baseTransform.gameObject.gameObject.SetActive(true);
	}
	
	/// <summary>
	/// In this method we also need to set the stick and base local transforms back to zero
	/// </summary>
	protected override void ResetControlState() {
		base.ResetControlState();
		// Setting the stick and base local positions back to local zero
		this.stickTransform.localPosition = Vector3.zero;
	}
	
	/// <summary>
	/// Handles user input for the joystick.
	/// </summary>
	/// <param name="camera">Camera to manage.</param>
	public void handleInput(Camera camera) {
		this.ParentCamera = camera;
		Vector3[] coordinates = new Vector3[4];
		this.GetComponent<RectTransform>().GetWorldCorners(coordinates);
		this.CalculatedTouchZone = new Rect(
			coordinates[0].x, coordinates[0].y,
			coordinates[2].x - coordinates[0].x,
			coordinates[1].y - coordinates[0].y
		);

		// Check for touches
		if (TweakIfNeeded())
			return;

		Touch currentTouch;
		this.IsTouchCaptured(out currentTouch);
	}
	
	/// <summary>
	/// Function for joystick tweaking (moving with the finger)
	/// The values of the Axis are also calculated here
	/// </summary>
	/// <param name="touchPosition">Current touch position in screen coordinates (pixels)
	/// It's recalculated in units so it's resolution-independent</param>
	protected override void TweakControl(Vector2 touchPosition) {
		// First, let's find our current touch position in world space
		Vector3 worldTouchPosition = ParentCamera.ScreenToWorldPoint(touchPosition);
		
		// Now we need to find a directional vector from the center of the joystick
		// to the touch position
		Vector3 differenceVector = (worldTouchPosition - this.baseTransform.position);
		
		// If we're out of the drag range
		if (differenceVector.sqrMagnitude > Mathf.Pow(CustomJoystick.DRAG_RADIUS, 2) ) {
			// Normalize this directional vector
			differenceVector.Normalize();
			
			//  And place the stick to it's extremum position
			this.stickTransform.position = this.baseTransform.position +
				differenceVector * CustomJoystick.DRAG_RADIUS;
		} else {
			// If we're inside the drag range, just place it under the finger
			this.stickTransform.position = worldTouchPosition;
		}
		
		// Store calculated axis values to our private variable
		CurrentAxisValues = differenceVector;
		
		// We also fire our event if there are subscribers
		OnControllerMoved(differenceVector);
	}

	/// <summary>
	/// Determines whether the stick is active.
	/// </summary>
	/// <returns><c>true</c> if the stick is active; otherwise, <c>false</c>.</returns>
	public bool IsStickActive() {
		return this.IsCurrentlyTweaking;
	}

	// Some editor-only stuff. It won't compile to any of the builds
#if UNITY_EDITOR
	/// <summary>
	/// Your old DrawGizmosSelected method
	/// It allows you to change properties of the control in the inspector 
	/// - it will recalculate all needed properties
	/// </summary>
	protected override void OnDrawGizmosSelected(){}
#endif
}
