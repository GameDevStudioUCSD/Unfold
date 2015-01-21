using UnityEngine;
using System.Collections;

public class LockOnToEnemy : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		/*if (other.GetType() == IFightable)
			other.GetComponent<IFightable>().DecrementHealth(5);*/
	}
	
	void OnTriggerStay (Collider other)
	{

	}
	
	void OnTriggerExit (Collider other)
	{

	}
}
