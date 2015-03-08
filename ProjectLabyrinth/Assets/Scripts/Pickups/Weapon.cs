using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public AudioClip pickUpSound;
	public int itemVal; 
	public int bonusDamage;
	public int bonusArmor;
	public float bonusSpeed;

	public bool debug_On;
	protected bool hasPickedUp = false;
	protected PlayerCharacter player;

	
	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			if(player && !hasPickedUp) {
				if (debug_On)
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

	void pickedUp(){


	}

}
