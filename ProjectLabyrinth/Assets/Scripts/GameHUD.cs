using System;
using UnityEngine;

/// <summary>
/// Class to control the behavior of all aspects of the player's interface
/// whilst in an instance of the game scene.
/// </summary>
public class GameHUD : MonoBehaviour {

	/// <summary>
    /// NetworkView required to check if player is mine.
    /// </summary>
    public NetworkView networkView;

	/// <summary>
	/// Gets or sets the user interface camera.
	/// </summary>
	/// <value>The user interface camera.</value>
	public Camera UICamera { get; set; }

	/// <summary>
    /// The actual player character.
    /// </summary>
	public PlayerCharacter player;

	/// <summary>
	/// The health system for the player.
	/// </summary>
	public HealthSystem healthSystem;

	/// <summary>
    /// The joystick used for determining user input.
    /// </summary>
    public CustomJoystick Joystick;

	/// <summary>
	/// The particle generator.
	/// </summary>
	public ParticleGenerator particles;

	/// <summary>
    /// Object initialization method.
    /// </summary>
    void OnEnable()
    {
        this.UICamera = this.GetComponent<Canvas>().worldCamera;
        if (this.player == null)
        {
            this.player = this.transform.root.GetComponentInChildren<PlayerCharacter>();
        }
        if (this.Joystick != null)
        {
            this.Joystick.ControllerMovedEvent += this.player.Move;
            this.Joystick.FingerLiftedEvent += this.player.Idle;
        }

    }

    // Update is called once per frame
    void Update() {
		if (this.Joystick != null) {
			this.Joystick.handleInput(this.UICamera);
			if (this.particles != null) {
				this.particles.handleInput(this.Joystick);
			}
		}
		if (this.healthSystem != null) {
			this.healthSystem.display(this.player);
		}
		foreach (Touch currentTouch in Input.touches) {
			if (currentTouch.phase == TouchPhase.Began) {
				Vector3 worldPosition = this.UICamera.ScreenToWorldPoint(currentTouch.position);

				// TODO: Add pause button functionality
				if (!this.Joystick.IsStickActive()) {
				}
			}
		}
	}
}
