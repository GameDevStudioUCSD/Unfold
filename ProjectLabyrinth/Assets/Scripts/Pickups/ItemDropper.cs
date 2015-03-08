using UnityEngine;
using System.Collections;

public class ItemDropper : MonoBehaviour {
	
	public GameObject[] pickupList;
	
	// Use this for initialization
	public void dropItem() {
		System.Random rnd = new System.Random();
		int rand = rnd.Next(0,pickupList.Length);
		Instantiate(pickupList[rand], new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
		
		
		
		/*switch (rand)
		{
		case 0:
			Instantiate(pickupList[0], new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
			break;
		case 1:
			Instantiate(upgradeSpeed, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
			break;
		case 2:
			Instantiate(upgradeMaxHealth, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
			break;
		case 3:
			Instantiate(upgradeDamage, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
			break;
		}*/
	}
}
