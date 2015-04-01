using UnityEngine;
using System.Collections;

public class ParticleGenerator : MonoBehaviour {
	
	public ParticleMovement trailObject = null;
	private int count;

	public void handleInput(CustomJoystick joystick) {
		if (Input.GetMouseButtonDown(0)) {

			if (!joystick.IsStickActive()) {
				createPath (Input.mousePosition);
				count = 1;
			}
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			if (!joystick.IsStickActive ()) {
				createPath (touchToMouse (Input.GetTouch (0).position));
				count = 1;
			}
		}


		if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved)) {
			ParticleMovement p = (ParticleMovement) this.GetComponentInChildren<ParticleMovement> ();
			if(p != null) {
				if(Input.GetMouseButton (0)) {
					p.move (Input.mousePosition);
				}

				else {
					p.move (touchToMouse (Input.GetTouch (0).position));
				}

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
	

		if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
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

	public Vector3 touchToMouse (Vector2 pos) {
		return new Vector3 (pos.x, pos.y);
	}

}
