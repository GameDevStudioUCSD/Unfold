using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		Debug.Log(other.name + " Entered");
	}
	
	void OnTriggerStay (Collider other)
	{
		Debug.Log(other.name + " Staying");
	}
	
	void OnTriggerExit (Collider other)
	{
		Debug.Log(other.name + " Exited");
	}
}
