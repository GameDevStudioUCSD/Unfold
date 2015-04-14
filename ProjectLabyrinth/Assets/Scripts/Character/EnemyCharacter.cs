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
	public GameObject arrow;
	
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
        arrow.SetActive(false);
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
			this.attackCollider.Add (other);
			chr.setAttacker (this);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.GetComponent<HitDetector> () != null) {
			PlayerCharacter chr = other.GetComponentInParent<PlayerCharacter>();
			this.attackCollider.Remove (other);
			chr.removeAttacker (this);
		}
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
		foreach(Character chr in attackers) {
			chr.removeAttackCollider (this.GetComponent<Collider>());
		}
		Destroy(this.gameObject);
        PickupDropper dropperScript = dropper.GetComponent<PickupDropper>();
		dropperScript.dropItem(transform.position.x, transform.position.z);
	}

	// Turns the arrow on/off
	public void setActive(bool state) {
		this.arrow.SetActive (state);
	}
}
