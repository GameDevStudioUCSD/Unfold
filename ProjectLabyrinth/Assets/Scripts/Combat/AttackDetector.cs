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
		if (other.GetComponent<InnerWall>() != null) {
			InnerWall innerWall = other.GetComponent<InnerWall>();
			EditWalls wall = innerWall.transform.parent.GetComponent<EditWalls>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
			chr.setWall(wall);
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
		if (other.GetComponent<InnerWall>() != null) {
			InnerWall innerWall = other.GetComponent<InnerWall>();
			EditWalls wall = innerWall.transform.parent.GetComponent<EditWalls>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
			chr.setWall(null);
		}
	}
}
