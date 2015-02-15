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

	// Character HP
	public int health;

	// Amount of damage the character deals
	public int damage;

	// Cooldown time between attacks
	public float attackDelay;

	// How fast the character can navigate the maze
	public float moveSpeed;
	
	// Hitbox of the opposing target character
	protected Collider attackCollider;

	// Type of attack performed by this character
	protected int attackType;

	// When the next attack time is available after attackDelay seconds
	protected float nextAttackTime = 0;

	/**
	 * Controls character attack patterns
	 */
	public void Attack() {
		if (this.attackCollider) {
			/*this.attackCollider.rigidbody.AddForce(Vector3.forward * 100f, ForceMode.Acceleration);
			this.attackCollider.rigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);*/
			Character target = (Character)this.attackCollider.gameObject.GetComponent("Character");
			if (target) {
				target.TakeDamage(this.damage, this.attackType);
				if(target.GetComponent<EnemyCharacter>() != null) {
					MonsterMovement move = (MonsterMovement)target.gameObject.GetComponent ("MonsterMovement");
					move.stun ();
				}
			}
		}

		this.nextAttackTime = Time.time + attackDelay;
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
}
