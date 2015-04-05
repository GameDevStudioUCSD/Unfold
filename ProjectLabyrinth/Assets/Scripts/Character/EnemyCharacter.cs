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
	public GameObject dropper;
	
	void Start() {
        // This places the monsters underneath a parent object labeled 
        // "Monsters"
        GameObject monsterRoot = GameObject.Find("Monsters");
        if( monsterRoot != null )
        {
            Transform monsterTransform;
            monsterTransform = GetComponent<Transform>();
            monsterTransform.parent = monsterRoot.GetComponent<Transform>();
        }
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
	
	public override bool TakeDamage(int enDamage, int enAttackType) {
		if (enAttackType == this.weakness) {
			enDamage = enDamage * 2;
		}
		
		this.currentHealth = this.currentHealth - enDamage;
		if (this.currentHealth <= 0) {
			this.Die();
			Debug.Log("Damage: " + enDamage);
			return true;
		}

		MonsterMovement mov = (MonsterMovement)GetComponent<MonsterMovement> ();
		mov.stun ();
		Debug.Log("Damage: " + enDamage);
		return false;
	}
	
	public override void Die() {
		Destroy(this.gameObject);
        PickupDropper dropperScript = dropper.GetComponent<PickupDropper>();
		dropperScript.dropItem(transform.position.x, transform.position.z);
	}
}
