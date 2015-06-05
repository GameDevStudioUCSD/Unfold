using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls behavior of the menu system on the player's heads-up display.
/// </summary>
public class MenuSystem : MonoBehaviour {

	/// <summary>
	/// Button to close this menu screen.
	/// </summary>
	public Button closeButton;

	/// <summary>
	/// Button to return to the main menu for the game.
	/// </summary>
	public Button mainMenuButton;

	/// <summary>
	/// Slider to allow the player to adjust the game's volume.
	/// </summary>
	public Slider volumeSlider;

	/// <summary>
	/// Button to exit this application.
	/// </summary>
	public Button exitButton;
	
	public AudioSource audioSource;
	public AudioClip UISound;

	void Start() {
		if (this.closeButton != null) {
			this.closeButton.onClick.AddListener(delegate() {
				this.gameObject.SetActive(false);
				SoundController.PlaySound(audioSource, UISound);
			});
		}
		if (this.mainMenuButton != null) {
			this.mainMenuButton.onClick.AddListener(delegate() {
				Application.LoadLevel("MainMenu");
				SoundController.PlaySound(audioSource, UISound);
			});
		}
		if (this.exitButton != null) {
			this.exitButton.onClick.AddListener(delegate() {
				SoundController.PlaySound(audioSource, UISound);
				Application.Quit();
			});
		}
	}

	public void control() {
		if (this.volumeSlider != null) {
			AudioListener.volume = this.volumeSlider.value;
		}
	}
}
