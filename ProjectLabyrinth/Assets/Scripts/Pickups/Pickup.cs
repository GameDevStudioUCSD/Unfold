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
    
	void OnTriggerEnter(Collider other) {
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
            if (debug_On)
                Debug.Log("Tried to play pickup sound!");
            if(particles != null)
            {
                Color objColor = GetComponent<Renderer>().material.color;
                GameObject particleObj;
                particleObj = (GameObject)Network.Instantiate(particles, transform.position + Vector3.up, Quaternion.identity, 0);
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
		switch (type)
		{
			/*health*/
			case 0:
				if (debug_On)
					Debug.Log("Adding health");
				player.addHealth(5);
				break;
			/*speed upgrade*/
			case 1:
				if (debug_On)
					Debug.Log("Adding speed");
				player.addSpeed(5);
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
		}
	}
}
