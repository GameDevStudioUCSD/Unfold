using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	float xAxis, zAxis = 0.0f;
	
	float xAngle, zAngle = 0.0f;
	// float zVelocity = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		xAxis = Input.GetAxis("Horizontal");
		zAxis = Input.GetAxis("Vertical");
		
		transform.Translate (xAxis, 0, zAxis);
		
		//transform.Rotate(xAngle, 0, zAngle);
		//transform.Translate(0, 0, zVelocity);
	}
}
