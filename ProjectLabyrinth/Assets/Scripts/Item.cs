using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public class Item : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		PlayerCharacter player = (PlayerCharacter)other.gameObject.GetComponent("PlayerCharacter");
		if (player) {
			Destroy(this.gameObject);
			player.health = 9000;
		}
	}
}
