using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public class Item : MonoBehaviour {
    public AudioClip pickUpSound;
    public bool debugON;
    private bool hasPickedUp = false;
	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			PlayerCharacter player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			if(player && !hasPickedUp) {
                if (debugON)
                    Debug.Log("Tried to play pickup sound!");
                SoundController.PlaySound(audio, pickUpSound);
                GetComponent<MeshRenderer>().enabled = false;
                hasPickedUp = !hasPickedUp;
				player.setHealth(9000);
			}
            else if(player && hasPickedUp)
                Destroy(this.gameObject);
		}
	}
}
