using UnityEngine;
using System.Collections;


public class ExitCollision : MonoBehaviour {


	public GameObject player;
	private Collider collider;
	void Start() 
	{
        Debug.Log("Public Player Var: " + player);
	}
	void OnTriggerEnter (Collider other)
	{
        Debug.Log("Trigger is working");
        Debug.Log("Object: " + other.gameObject);
        if (other.gameObject.name == player.name)
        {
            Debug.Log("Player Entered the Collision");
        }
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
