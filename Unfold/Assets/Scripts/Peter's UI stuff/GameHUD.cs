using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to control the behavior of all aspects of the player's interface
/// whilst in an instance of the game scene.
/// </summary>
public class GameHUD : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip UISound;

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
	/// System to control heads-up display of player statistics.
	/// </summary>
	public PlayerStatsHUD playerStats;

	/// <summary>
	/// Player menu system interface for heads-up display.
	/// </summary>
	public MenuSystem menuSystem;

	/// <summary>
	/// Button to bring up the menu system on the player's HUD.
	/// </summary>
	public Button optionsButton;

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
	void OnEnable() {
		this.UICamera = this.GetComponent<Canvas>().worldCamera;
		if (this.Joystick != null && this.player != null) {
			this.Joystick.ControllerMovedEvent += this.player.Move;
			this.Joystick.FingerLiftedEvent += this.player.Idle;
		}
		if (this.optionsButton != null && this.menuSystem != null) {
			this.optionsButton.onClick.AddListener(delegate() {
				SoundController.PlaySound(audioSource, UISound);
				this.menuSystem.gameObject.SetActive(true);
			} );
		}
	}

    // Update is called once per frame
	void Update() {
		if (this.playerStats != null) {
			this.playerStats.display(this.player);
		}
		if (this.menuSystem && this.menuSystem.gameObject.activeSelf) {
			this.menuSystem.control();
		}
		if (this.Joystick != null) {
			this.Joystick.handleInput(this.UICamera);
			if (this.particles != null) {
				this.particles.handleInput(this.Joystick);
			}
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
