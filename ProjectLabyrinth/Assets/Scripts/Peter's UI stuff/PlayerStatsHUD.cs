using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls presentation of player statistics on the heads-up display.
/// </summary>
public class PlayerStatsHUD : MonoBehaviour {

	/// <summary>
	/// Slider displaying the player's health.
	/// </summary>
	public Slider health;

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
		if (this.health != null) {
			this.health.maxValue = player.maxHealth;
			this.health.value = player.currentHealth;
			Transform healthText = this.health.transform.Find("Health text");
			if (healthText) {
				healthText.GetComponent<Text>().text = player.currentHealth.ToString() +
					"/" + player.maxHealth.ToString();
			}
			Transform healthSymbol = this.health.transform.Find("Health symbol");
			if (healthSymbol) {
				healthSymbol.Rotate(Vector3.down);
			}
		}

		if (this.damageText != null) {
			this.damageText.text = "Damage: " + player.damage;
		}
		if (this.damageSymbol != null) {
			this.damageSymbol.Rotate(Vector3.down);
		}
		if (this.speedText != null) {
			this.speedText.text = "Speed: " + player.moveSpeed;
		}
		if (this.speedSymbol != null) {
			this.speedSymbol.Rotate(Vector3.down);
		}
	}
}
