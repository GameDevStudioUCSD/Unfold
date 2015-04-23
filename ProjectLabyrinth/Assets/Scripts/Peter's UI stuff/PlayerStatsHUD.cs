using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls presentation of player statistics on the heads-up display.
/// </summary>
public class PlayerStatsHUD : MonoBehaviour {

	/// <summary>
	/// Text to display the player's health amount on the game HUD.
	/// </summary>
	public Text healthText;

	/// <summary>
	/// Symbol representing the health token collected in game.
	/// </summary>
	public Transform healthSymbol;

	/// <summary>
	/// Text to display the player's damage amount on the game HUD.
	/// </summary>
	public Text damageText;

	/// <summary>
	/// Symbol representing the damage token collected in game.
	/// </summary>
	public Transform damageSymbol;
	
	/// <summary>
	/// Text to display the player's speed amount on the game HUD.
	/// </summary>
	public Text speedText;
	
	/// <summary>
	/// Symbol representing the speed token collected in game.
	/// </summary>
	public Transform speedSymbol;

	public virtual void display(PlayerCharacter player) {
		if (this.healthText != null) {
			this.healthText.text = "Health: " + player.currentHealth.ToString() +
				"/" + player.maxHealth.ToString();
		}
		if (this.healthSymbol != null) {
			this.healthSymbol.Rotate(Vector3.down);
		}
		if (this.damageText != null) {
			this.damageText.text = "Damage: " + player.damage.ToString();
		}
		if (this.damageSymbol != null) {
			this.damageSymbol.Rotate(Vector3.down);
		}
		if (this.speedText != null) {
			this.speedText.text = "Speed: " + player.moveSpeed.ToString();
		}
		if (this.speedSymbol != null) {
			this.speedSymbol.Rotate(Vector3.down);
		}
	}
}
