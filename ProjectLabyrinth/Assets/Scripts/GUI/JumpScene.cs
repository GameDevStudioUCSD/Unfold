using UnityEngine;
using System.Collections;

public class JumpScene : MonoBehaviour {

	public GameObject loadedScene;
	public bool debug_On;
	
	void OnTriggerEnter(Collider other)
	{
		Collider attackCollider = other;
		GameObject attackObject = attackCollider.gameObject;
		if (debug_On)
			Debug.Log ("Collider set to " + attackCollider.name);
			
		Instantiate(loadedScene, new Vector3(0, 0, 0), Quaternion.identity);
	}
}
