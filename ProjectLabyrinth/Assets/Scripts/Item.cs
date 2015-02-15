using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public class Item : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			PlayerCharacter player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			if(player) {
				Destroy(this.gameObject);
				player.setHealth(9000);
			}
		}
	}
}
