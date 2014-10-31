using UnityEngine;
using System.Collections;

public class TriggerTest : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
	
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
