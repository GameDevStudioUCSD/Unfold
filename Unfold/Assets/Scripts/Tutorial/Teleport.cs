using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public float x;
	public float z;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		PickupDetector det = (PickupDetector) other.GetComponent<PickupDetector> ();
		if (det == null)
			return;

		PlayerCharacter player = det.GetComponentInParent<PlayerCharacter> ();

		player.transform.position = new Vector3 (x, 0, z);
	}
}
