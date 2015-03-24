using UnityEngine;
using System.Collections;

public class PickupDropper : MonoBehaviour {
	
	public GameObject[] pickupList;
	public bool debug_On;
	
	// Use this for initialization
	public void dropItem(float x, float z) {
		System.Random rnd = new System.Random();
		int rand = rnd.Next(0,pickupList.Length-1);
		if (debug_On)
			Debug.Log("Dropped an Item of type: " + rand);
		Instantiate(pickupList[rand], new Vector3(x, 1, z), Quaternion.identity);
	}
}
