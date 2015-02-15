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

	// The particular item this enemy drops when killed
	public GameObject Item;
	
	void Start() {
		this.health = startHealth;
		this.damage = 5;
		this.attackDelay = 2;
		this.moveSpeed = 8;
	}
	
	void FixedUpdate() {
		
		if (Time.time > nextAttackTime && attackCollider != null) {
			// 10% chance for critical strikes
			if (UnityEngine.Random.Range(0, 100) < 10) {
				this.attackType = 15;
			}
			this.Attack();
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<HitDetector> () != null) {
			PlayerCharacter chr = other.GetComponentInParent<PlayerCharacter>();
			this.attackCollider = other;
		}
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
		Instantiate(Item, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
	}
}
