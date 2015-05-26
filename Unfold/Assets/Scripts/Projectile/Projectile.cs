using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {



	public int damage;            //assign damage here
	public AudioClip hitSound;
	public int attackType;
	public GameObject particles;
	protected PlayerCharacter player;






	//automatically "self-destruct after 2 seconds
	void Start(){
		Destroy (this.gameObject, 2.0f);
	}


	//player takes damage if hit by projectile
	void OnTriggerEnter(Collider col) {
		SoundController.PlaySound(GetComponent<AudioSource>(), hitSound);

		HitDetector hitDetector = (HitDetector)col.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter)hitDetector.GetComponentInParent<PlayerCharacter> ();
			player.TakeDamage (damage, attackType);
			Destroy (this.gameObject);
		} 

	}


}
