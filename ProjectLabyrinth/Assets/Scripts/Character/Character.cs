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

	public bool debug_On = true;

	//Number of enemies killed. Used only for player but implemented here for access to Attack function
	protected int kills = 0;

	// Cooldown time between attacks
	public float attackDelay;

	// Statistics shared amongst all characters
	public int damage;
	public int currentHealth;
	public float moveSpeed;
	
	// Hitboxes of the opposing target characters
	protected ArrayList attackCollider = new ArrayList();

	// Those who are currently attacking this character
	protected ArrayList attackers = new ArrayList();

	// Type of attack performed by this character
	protected int attackType;

	// When the next attack time is available after attackDelay seconds
	protected float nextAttackTime = 0;
	

	/**
	 * Controls character attack patterns
	 * Returns true if target takes damage
	 * Else, returns false
	 */
	public virtual bool Attack() {
		bool hasAttacked = false;

		// long name is long
		ArrayList ac = this.attackCollider;

		for(int i = ac.Count - 1; i >= 0; i--) {
			/*
			this.attackCollider.rigidbody.AddForce(Vector3.forward * 100f, ForceMode.Acceleration);
			this.attackCollider.rigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);
			*/


			Character target = (Character)((Collider) ac[i]).gameObject.GetComponent("Character");
			if (!target)
			{
				target = (Character) ((Collider) ac[i]).GetComponentInParent<Character>(); 
			}

			if (target) {
				if (target.TakeDamage(this.damage, this.attackType))
					kills++;
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
	public abstract bool TakeDamage(int enDamage, int enAttackType);

	/**
	 * Controls character death behavior
	 */
	public abstract void Die();

	public void setAttackCollider(Collider col) {
		this.attackCollider.Add (col);
	}

	public void removeAttackCollider(Collider col) {
		this.attackCollider.Remove (col);
	}

	public void setAttacker(Character chr) {
		this.attackers.Add (chr);
	}

	public void removeAttacker(Character chr) {
		this.attackers.Remove (chr);
	}

	public int getCurrentHealth() {
		return this.currentHealth;
	}
}
