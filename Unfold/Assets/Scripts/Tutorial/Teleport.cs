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
		PlayerCharacter player = GetComponentInParent<PlayerCharacter> ();
		if (player == null)
			return;

		player.transform.position = new Vector3 (x, 0, z);
	}
}
