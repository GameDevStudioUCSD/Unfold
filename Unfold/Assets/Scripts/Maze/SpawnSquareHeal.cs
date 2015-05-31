using UnityEngine;
using System.Collections;

public class SpawnSquareHeal : MonoBehaviour {

	public AudioClip healSound;
	PlayerCharacter player;
	
	void OnTriggerEnter(Collider other) {
		PickupDetector PickupDetector = (PickupDetector)other.gameObject.GetComponent("PickupDetector");
		if (PickupDetector) {
			SoundController.PlaySound(GetComponent<AudioSource>(), healSound);
			player = (PlayerCharacter) PickupDetector.GetComponentInParent<PlayerCharacter>();
			player.addHealth ();
		}
	}
}
