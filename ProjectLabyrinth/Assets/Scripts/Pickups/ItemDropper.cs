using UnityEngine;
using System.Collections;

public class ItemDropper : MonoBehaviour {
	
	public GameObject[] pickupList;
	
	// Use this for initialization
	public void dropItem(float x, float z) {
		System.Random rnd = new System.Random();
		int rand = rnd.Next(0,pickupList.Length);
		Instantiate(pickupList[rand], new Vector3(x, 1, z), Quaternion.identity);
	}
}
