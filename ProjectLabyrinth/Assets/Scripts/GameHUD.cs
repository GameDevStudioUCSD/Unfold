using System;
using UnityEngine;

/// <summary>
/// Class to control the behavior of all aspects of the player's interface
/// whilst in an instance of the game scene.
/// </summary>
public class GameHUD : MonoBehaviour {

	/// <summary>
	/// Gets or sets the joystick.
	/// </summary>
	/// <value>The joystick.</value>
	public CustomJoystick Joystick { get; set; }

	/// <summary>
	/// Gets or sets the user interface camera.
	/// </summary>
	/// <value>The user interface camera.</value>
	public Camera UICamera { get; set; }

	/// <summary>
	/// The player controller.
	/// </summary>
	public CharacterController playerController;

	/// <summary>
	/// The actual player character.
	/// </summary>
	public PlayerCharacter player;

	void Start() {
		this.UICamera = this.GetComponent<Canvas>().worldCamera;
		this.Joystick = this.GetComponentInChildren<CustomJoystick>();
		this.Joystick.ControllerMovedEvent += this.player.Move;
		this.Joystick.FingerLiftedEvent += this.player.Idle;
	}

	void Update() {
		this.Joystick.handleInput(this.UICamera);
	}
}
