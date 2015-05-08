using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public class Pickup : MonoBehaviour {
    public AudioClip pickUpSound;
    public bool debug_On;
    public GameObject particles;
    public int type;
    protected bool hasPickedUp = false;
    protected PlayerCharacter player;
    
	float t = 0f;
	float del = .005f;
    
    void Update() {
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
                Debug.Log("Pickup Trigger Reached");
            if(particles != null)
            {
                Color objColor = GetComponent<Renderer>().material.color;
                GameObject particleObj;
                Quaternion rot = new Quaternion(270, 0, 0, 0);
                particleObj = (GameObject)Network.Instantiate(particles, transform.position + Vector3.up, rot, 0);
                particleObj.GetComponent<Transform>().parent = player.GetComponent<Transform>();
                particleObj.GetComponent<ParticleSystem>().startColor = objColor;
            }
            SoundController.PlaySound(GetComponent<AudioSource>(), pickUpSound);
            GetComponent<MeshRenderer>().enabled = false;
            hasPickedUp = !hasPickedUp;
			pickedUp();
            Destroy(this.gameObject);
		}
	}
	
	void pickedUp()
	{
		WeaponButton button = player.weaponButton;
		switch (type)
		{
			/*health*/
			case 0:
				if (debug_On)
					Debug.Log("Healing health");
				player.addHealth(5);
				break;
			/*speed upgrade*/
			case 1:
				if (debug_On)
					Debug.Log("Adding speed");
				player.addSpeed(2);
				break;
			/*max health upgrade*/
			case 2:
				if (debug_On)
					Debug.Log("Adding maxHealth");
				player.addMaxHealth(5);
				break;
			/*damage upgrade*/
			case 3:
				if (debug_On)
					Debug.Log("Adding damage");
				player.addDamage(5);
				break;

			/* Start Sword (???) */
			case 4:
				if (debug_On)
					Debug.Log ("Adding start sword");
				
//				player.removeAbility();
//				player.addAbility ("startsword");
				button.setWeapon (3);
				break;

			/* Hammer (Break walls) */
			case 5:
				if(debug_On)
					Debug.Log ("Adding hammer");

//				player.removeAbility ();
//				player.addAbility ("hammer");
				button.setWeapon (0);
				break;

			case 6:
				if(debug_On)
					Debug.Log ("Adding sword");

//				player.removeAbility ();
//				player.addAbility ("sword");
				button.setWeapon(1);
				break;

			case 7:
				if(debug_On)
					Debug.Log ("Adding foil");
				
//				player.removeAbility ();
//				player.addAbility ("foil");
				button.setWeapon (2);
				break;
		}
	}
}
