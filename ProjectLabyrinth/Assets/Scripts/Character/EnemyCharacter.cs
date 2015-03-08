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
	public ItemDropper dropper;
	
	void Start() {
		this.currentHealth = baseMaxHealth;
		this.baseDamage = 5;
		this.attackDelay = 2;
		this.baseMoveSpeed = 8;
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
		
		this.currentHealth = this.currentHealth - enDamage;
		if (this.currentHealth <= 0) {
			this.Die();
		}

		MonsterMovement mov = (MonsterMovement)GetComponent<MonsterMovement> ();
		mov.stun ();
		Debug.Log("Damage: " + enDamage);
	}
	
	public override void Die() {
		Destroy(this.gameObject);
		dropper.dropItem(transform.position.x, transform.position.z);
		/*System.Random rnd = new System.Random();
		int rand = rnd.Next(0,4);
		//int rand = 1;
		switch (rand)
		{
			case 0:
				Instantiate(healthGlobe, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
				break;
			case 1:
				Instantiate(upgradeSpeed, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
				break;
			case 2:
				Instantiate(upgradeMaxHealth, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
				break;
			case 3:
				Instantiate(upgradeDamage, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
				break;
		}*/
	}
}
