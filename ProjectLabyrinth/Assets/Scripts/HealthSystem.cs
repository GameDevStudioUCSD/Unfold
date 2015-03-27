using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls presentation of the player's health on the heads-up display.
/// </summary>
public class HealthSystem : MonoBehaviour {

	/// <summary>
	/// Gets or sets the player character.
	/// </summary>
	/// <value>The player character.</value>
	public PlayerCharacter player { get; set; }

	/// <summary>
	/// Gets or sets the health bar.
	/// </summary>
	/// <value>The health bar.</value>
	public Slider healthBar { get; set; }

	/// <summary>
	/// Gets or sets the health text.
	/// </summary>
	/// <value>The health text.</value>
	public Text healthText { get; set; }

	/// <summary>
	/// Object initialization method.
	/// </summary>
	public void OnEnable() {
		this.healthBar = this.transform.Find("Health bar").GetComponent<Slider>();
		this.healthText = this.transform.Find("Health text").GetComponent<Text>();
	}

	/// <summary>
	/// Display this system according to the health of the specified player.
	/// </summary>
	/// <param name="player">The specified player character.</param>
	public virtual void display(PlayerCharacter player) {
		this.player = player;
		this.healthBar.maxValue = player.maxHealth;
		this.healthBar.value = player.getCurrentHealth();
		this.healthText.text = player.getCurrentHealth().ToString() + "/" + player.maxHealth.ToString();
	}
}
