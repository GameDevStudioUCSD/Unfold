using UnityEngine;
using System.Collections;

public class LockOnArrowMovement : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down * 200 * Time.deltaTime);
	}
}
