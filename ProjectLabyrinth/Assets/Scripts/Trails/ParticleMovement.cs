using UnityEngine;
using System.Collections;

public class ParticleMovement : MonoBehaviour {

	public float Distance = 1;

	// Update is called once per frame
	void Update () {
	/*Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 pos = r.GetPoint (Distance);
		transform.position = pos;
	*/
	}

	public void move (Vector3 mouse) {
		//Vector3 v = new Vector3 (t.position.x, t.position.y);
		Ray r = Camera.main.ScreenPointToRay (mouse);
		Vector3 pos = r.GetPoint (Distance);
		transform.position = pos;
	}
}
