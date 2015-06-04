using UnityEngine;
using System.Collections;

public class AttackDetector : MonoBehaviour {

	public WeaponButton Button;

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<HitDetector>() != null) {
			PlayerCharacter player = (PlayerCharacter)other.GetComponentInParent<PlayerCharacter>();
			player.currentHealth = player.currentHealth - 10;
		}
		if (other.GetComponent<EnemyCharacter>() != null) {
			EnemyCharacter enemy = (EnemyCharacter) other.GetComponent<EnemyCharacter>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
			chr.setAttackCollider(other);
			enemy.setAttacker(chr);
			enemy.setActive(true);
		}
		if (other.GetComponent<InnerWall>() != null) {
			InnerWall innerWall = other.GetComponent<InnerWall>();
			EditWalls wall = innerWall.transform.parent.GetComponent<EditWalls>();
			Button.setWall(wall);
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<EnemyCharacter>() != null) {
			EnemyCharacter enemy = (EnemyCharacter) other.GetComponent<EnemyCharacter>();
			PlayerCharacter chr = GetComponentInParent<PlayerCharacter>();
			chr.removeAttackCollider(other);
			enemy.removeAttacker(chr);
			enemy.setActive(false);
		}
		if (other.GetComponent<InnerWall>() != null) {
			Button.setWall(null);
		}
	}
}
