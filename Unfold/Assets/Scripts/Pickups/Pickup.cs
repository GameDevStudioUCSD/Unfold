using System;
using UnityEngine;

/**
 * Item class
 *
 * Represents items that may randomly drop from dead enemies.
 */
public class Pickup : MonoBehaviour {
    public AudioClip pickUpSound;
    public GameObject particles;
    public int type;
    
    /*used for determining what model to display in model, if not a weapon, use -1*/
    public int modelID;
    protected bool hasPickedUp = false;
    protected PlayerCharacter player;
    private double deleteTime;
    
	float t = 0f;
	float del = .005f;
    
    void Update() {
		transform.Rotate(Vector3.down * 100 * Time.deltaTime);
		float change = del * Mathf.Sin (t);
		transform.Translate(Vector3.up * change);
		t += .03f;
		if (hasPickedUp)
		{
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().enabled = false;
            foreach( Transform child in transform)
            {
                if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    child.gameObject.GetComponent<MeshRenderer>().enabled = false;
                foreach (Transform subchild in child)
                {
                    if (subchild.gameObject.GetComponent<MeshRenderer>() != null)
                        subchild.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
			if (Time.time > deleteTime)
			{
				Destroy(this.gameObject);
			}	
		}
    }
    
	void OnTriggerEnter(Collider other) {
		PickupDetector PickupDetector = (PickupDetector)other.gameObject.GetComponent("PickupDetector");
		if (PickupDetector && !hasPickedUp) {
			player = (PlayerCharacter) PickupDetector.GetComponentInParent<PlayerCharacter>();
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
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().enabled = false;
            else
                GetComponent<SkinnedMeshRenderer>().enabled = false;
            hasPickedUp = !hasPickedUp;
            deleteTime = Time.time+1;
			pickedUp();
		}
	}
	
	void pickedUp()
	{
		WeaponButton button = player.weaponButton;
		player.updateWeaponModel(modelID);
		switch (type)
		{
			//health
			case 0:
				player.addHealth(4);
				break;
			//speed upgrade
			case 1:
				player.addSpeed(2);
				break;
			//max health upgrade
			case 2:
				player.addMaxHealth(3);
				break;
			//damage upgrade
			case 3:
				player.addDamage(2);
				break;

			//Start Sword (???)
			case 4:
				button.setWeapon (3);
				break;

			//Hammer (Break walls)
			case 5:
				button.setWeapon (0);
				break;

			case 6:
				button.setWeapon(1);
				break;

			case 7:
				button.setWeapon (2);
				break;

			case 8:
				button.setWeapon (4);
				break;

			case 9:
				button.setWeapon (5);
				break;
		}
		
	}
}
