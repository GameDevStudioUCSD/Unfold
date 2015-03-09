using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public AudioClip pickUpSound;
	public int itemType;
	public int setVal; 
	public int bonusDamage;
	public int bonusMaxHealth;
	public float bonusMoveSpeed;

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
		player.equipItem(this);
	}

}
