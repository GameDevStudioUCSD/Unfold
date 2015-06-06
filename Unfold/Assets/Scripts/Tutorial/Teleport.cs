using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

	public float x;
	public float z;

	public SpiderTutorialMovement monster;
	public Text txt;
	public string message;

	public bool active;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (!active) {
			return;
		}

		PickupDetector det = (PickupDetector) other.GetComponent<PickupDetector> ();
		if (det == null)
			return;

		PlayerCharacter player = det.GetComponentInParent<PlayerCharacter> ();

		player.transform.position = new Vector3 (x, 0, z);

		if (monster != null) {
			this.monster.canMove = true;
		}

		txt.text = message;
	}
}
