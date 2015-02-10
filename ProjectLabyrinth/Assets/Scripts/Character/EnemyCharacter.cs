using System;
using System.Collections;
using UnityEngine;

/**
 * EnemyCharacter class
 *
 * Represents enemy character stats and things enemies can do
 */
public class EnemyCharacter : Character {

	/**
	 * Determines what kind of weakness this enemy has, if any
	 *
	 * 0000 = no weaknesses
	 * 0001 = horizontal weakness
	 * 0010 = vertical weakness
	 * 0100 = diagonal1 weakness (topright -> bottomleft)
	 * 1000 = diagonal2 weakness (topleft -> bottomright)
	 */
	public int weakness;

	void Start() {
	}

	void FixedUpdate() {
	}

	void OnTriggerEnter(Collider other) {
		this.attackCollider = other;
	}

	void OnTriggerExit(Collider other) {
		this.attackCollider = null;
	}

	public override void TakeDamage(int enDamage, int enAttackType) {
		if (enAttackType == this.weakness) {
			enDamage = enDamage * 2;
		}

		this.health = this.health - enDamage;
		if (this.health <= 0) {
			this.Die();
		}
		Debug.Log("Damage: " + enDamage);
	}

	public override void Die() {
		Destroy(this.gameObject);
	}
}
