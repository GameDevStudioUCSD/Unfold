using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public AudioClip pickUpSound;
	
	/* itemType determines what kind of item it is
	 * 0 - Weapon
	 * 1 - Armor
	 * 2 - Boots
	 */
	public int itemType;
	
	/* setVal stores the set's index
     */
	public int setVal; 
	public int bonusDamage;
	public int bonusMaxHealth;
	public float bonusMoveSpeed;

	public bool debug_On;
	protected bool hasPickedUp = false;
	protected PlayerCharacter player;
	
	float t = 0f;
	float del = .005f;

	void Update ()
	{
		transform.Rotate(Vector3.down * 100 * Time.deltaTime);
		float change = del * Mathf.Sin (t);
		transform.Translate(Vector3.up * change);
		t += .03f;
	}
	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			if (debug_On)
				Debug.Log("Item Trigger Reached");
			SoundController.PlaySound(GetComponent<AudioSource>(), pickUpSound);
			GetComponent<MeshRenderer>().enabled = false;
			hasPickedUp = !hasPickedUp;
			pickedUp();

			Destroy(this.gameObject);
		}
	}

	void pickedUp(){
		player.equipItem(this);
	}

}
