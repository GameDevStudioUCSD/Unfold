using System;
using System.Collections;
using UnityEngine;

/**
 * Abstract Character class
 *
 * Collects common information about all types of character objects
 * spawned in the game.
 */
public abstract class Character : MonoBehaviour {

	public bool debug_On;
	
	// Cooldown time between attacks
	public float attackDelay;
	
	// base stats are remain with the character even after death
	public int baseDamage;
	public int baseMaxHealth;
	protected int currentHealth;
	public float baseMoveSpeed;
	
	/*total stats for character, initialized to base stats*/
	public int maxDamage;
	public int maxHealth;
	public float maxMoveSpeed;
	
	// Hitbox of the opposing target character
	protected Collider attackCollider;

	// Type of attack performed by this character
	protected int attackType;

	// When the next attack time is available after attackDelay seconds
	protected float nextAttackTime = 0;

	void Start()
	{
		maxDamage = baseDamage;
		maxHealth = baseMaxHealth;
		maxMoveSpeed = baseMoveSpeed;
	}
	
	/**
	 * Controls character attack patterns
     * Returns true if target takes damage
     * Else, returns false
	 */
	public virtual bool Attack() {
        bool hasAttacked = false;
		if (this.attackCollider) {
			/*
			this.attackCollider.rigidbody.AddForce(Vector3.forward * 100f, ForceMode.Acceleration);
			this.attackCollider.rigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);
			*/
            
			Character target = (Character)this.attackCollider.gameObject.GetComponent("Character");
            if (!target)
            {
                target = (Character)this.attackCollider.GetComponentInParent<Character>(); 
            }

			if (target) {
				target.TakeDamage(this.maxDamage, this.attackType);
                hasAttacked = true;
			}
			this.nextAttackTime = Time.time + attackDelay;
            
		}
        return hasAttacked;
	}

	/**
	 * Subtracts health from this character based on damage dealt.
	 *
	 * <param name="enDamage">Damage dealt by an enemy.</param>
	 * <param name="enAttackType">Type of attack dealt by an enemy.</param>
	 */
	public abstract void TakeDamage(int enDamage, int enAttackType);

	/**
	 * Controls character death behavior
	 */
	public abstract void Die();

	public void setAttackCollider(Collider col) {
		this.attackCollider = col;
	}

	public int getCurrentHealth() {
		return this.currentHealth;
	}
}
