using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour {

	private CNJoystick joystick;
	public ParticleMovement trail;

	// Use this for initialization
	void Start () {
		joystick = (CNJoystick) GameObject.Find("CNJoystick");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			if (!joystick.IsTouchCaptured()) {
				GameObject trail = GameObject.Instantiate(Trail) as Transform;
				trail.transform.parent = GameObject.Find("Player Avatar/Canvas").transform;
			}
		}
	}
}
