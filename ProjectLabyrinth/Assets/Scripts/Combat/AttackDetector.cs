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
		if (other.GetComponent<EnemyCharacter> () != null) {
			EnemyCharacter enemy = (EnemyCharacter) other.GetComponent<EnemyCharacter>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter> ();
			chr.setAttackCollider (other);
			enemy.setAttacker (chr);
			enemy.setActive (true);

		}
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<EnemyCharacter>() != null) {
			EnemyCharacter enemy = (EnemyCharacter) other.GetComponent<EnemyCharacter>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
			chr.removeAttackCollider(other);
			enemy.removeAttacker (chr);
			enemy.setActive (false);
		}
	}
}
