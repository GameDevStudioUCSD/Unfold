using UnityEngine;
using System.Collections;


public class ExitCollision : MonoBehaviour {


	public GameObject player;
	private Collider collider;
	void Start() 
	{
		
	}
	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.name == "First Person Controller")
			Debug.Log ("Player Entered the Collision");
		Debug.Log ("Trigger is working");
	}
	
	/*void OnCollisionStay (Collision other)
	{
		Debug.Log ("Object Inside the trigger");
	}
	
	void OnCollisionExit (Collision other)
	{
		Debug.Log ("Object has Exited the trigger");
	}*/
}
