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
	/// Slider displaying the player's potential damage.
	/// </summary>
	public Slider damage;

	/// <summary>
	/// Slider displaying the player's speed.
	/// </summary>
	public Slider speed;

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

		if (this.damage != null) {
			this.damage.maxValue = player.damage;
			this.damage.value = player.damage;
			Transform damageText = this.damage.transform.Find("Damage text");
			if (damageText) {
				damageText.GetComponent<Text>().text = player.damage.ToString();
			}
			Transform damageSymbol = this.damage.transform.Find("Damage symbol");
			if (damageSymbol) {
				damageSymbol.Rotate(Vector3.down);
			}
		}

		if (this.speed != null) {
			this.speed.maxValue = player.moveSpeed;
			this.speed.value = player.moveSpeed;
			Transform speedText = this.speed.transform.Find("Speed text");
			if (speedText) {
				speedText.GetComponent<Text>().text = player.moveSpeed.ToString();
			}
			Transform speedSymbol = this.speed.transform.Find("Speed symbol");
			if (speedSymbol) {
				speedSymbol.Rotate(Vector3.down);
			}
		}
	}
}
