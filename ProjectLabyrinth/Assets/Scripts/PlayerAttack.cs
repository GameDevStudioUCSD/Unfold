using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	Collider attackVictim;
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log ("MOUSE PRESSED");
			if (attackVictim)
			{
				Debug.Log ("Attacking " + attackVictim.name);
				attackVictim.rigidbody.AddForce(Vector3.forward * 3000f, ForceMode.Acceleration);
				attackVictim.rigidbody.AddForce(Vector3.up * 3000f, ForceMode.Acceleration);
				
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		attackVictim = other;
	}
	
	void OnTriggerExit(Collider other)
	{
		attackVictim = null;
	}
	
}
