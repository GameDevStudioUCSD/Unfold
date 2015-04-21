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
	/// Button to allow the player to control the game's sound effects.
	/// </summary>
	public Button muteSoundButton;

	/// <summary>
	/// Button to exit this application.
	/// </summary>
	public Button exitButton;

	void Start() {
		if (this.closeButton != null) {
			this.closeButton.onClick.AddListener(delegate() {
				this.gameObject.SetActive(false);
			});
		}
		if (this.mainMenuButton != null) {
			this.mainMenuButton.onClick.AddListener(delegate() {
				Application.LoadLevel("MainMenu");
			});
		}
		if (this.muteSoundButton != null) {
			Text muteSoundButtonText = (Text)this.muteSoundButton.GetComponentInChildren<Text>();
			this.muteSoundButton.onClick.AddListener(delegate() {
				if (AudioListener.volume > 0) {
					AudioListener.volume = 0;
					if (muteSoundButtonText) {
						muteSoundButtonText.text = "Unmute Sounds";
					}
				} else {
					AudioListener.volume = 1;
					if (muteSoundButtonText) {
						muteSoundButtonText.text = "Mute Sounds";
					}
				}
			} );
		}
		if (this.exitButton != null) {
			this.exitButton.onClick.AddListener(delegate() {
				Application.Quit();
			});
		}
	}
}
