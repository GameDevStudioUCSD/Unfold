using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public abstract class Pickup : MonoBehaviour {
    public AudioClip pickUpSound;
    public bool debugON;
    protected bool hasPickedUp = false;
    protected PlayerCharacter player;
    
	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			if(player && !hasPickedUp) {
                if (debugON)
                    Debug.Log("Tried to play pickup sound!");
                SoundController.PlaySound(GetComponent<AudioSource>(), pickUpSound);
                GetComponent<MeshRenderer>().enabled = false;
                hasPickedUp = !hasPickedUp;
				pickedUp();
			}
            else if(player && hasPickedUp)
                Destroy(this.gameObject);
		}
	}
	
	public abstract void pickedUp();
}
