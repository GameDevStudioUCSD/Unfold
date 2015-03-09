using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour {

	private CNJoystick joystick;
	public ParticleMovement trailObject = null;
	private int count;

	// Use this for initialization
	void Start () {
		joystick = (CNJoystick) GameObject.Find("Joystick").GetComponent<CNJoystick>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Touch t;
			if (!joystick.IsTouchCaptured(out t)) {
				createPath (Input.mousePosition);
				count = 1;
			}
		}


		if (Input.GetMouseButton(0)) {
			ParticleMovement p = (ParticleMovement) this.GetComponentInChildren<ParticleMovement> ();
			if(p != null) {
				p.move (Input.mousePosition);
				ParticleSystem s = (ParticleSystem) p.GetComponent<ParticleSystem>();
				if(count > 0) {
					count--;
				}
				else {
					s.startSize = 0.1f;
					s.startLifetime = 0.3f;
				}

			}
		}
	

		if (Input.GetMouseButtonUp(0)) {
			Destroy (GameObject.Find ("Trail(Clone)"));
		} 
	}

	public ParticleMovement createPath (Vector3 start)
	{
		Ray r = Camera.main.ScreenPointToRay (start);
		Vector3 pos = r.GetPoint (1);
		ParticleMovement trail = (ParticleMovement)GameObject.Instantiate (trailObject, pos, Quaternion.identity);
		trail.transform.SetParent (transform, false);
		return trail;
	}
}
