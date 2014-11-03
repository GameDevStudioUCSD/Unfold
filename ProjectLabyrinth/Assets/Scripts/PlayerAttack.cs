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
				Debug.Log ("Attacking " + attackVictim.name);
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
