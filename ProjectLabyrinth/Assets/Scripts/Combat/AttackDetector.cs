using UnityEngine;
using System.Collections;

public class AttackDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<MonsterMovement> () != null) {
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter> ();
			chr.setAttackCollider (other);
		}
	}

	void OnTriggerExit(Collider other) {
		PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
		chr.setAttackCollider(null);
	}
}
