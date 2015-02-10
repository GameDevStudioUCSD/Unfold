using System;
using System.Collections;
using UnityEngine;

/**
 * PlayerCharacter class
 *
 * Represents player stats and things the player can do
 */
public class PlayerCharacter : Character {

	// Helps correlate user input to attack calculation
	private Touch initialTouch;

	void Start() {
	}

	void FixedUpdate() {

		if (Time.time > nextAttackTime) {

			foreach (Touch t in Input.touches) {

				if (t.phase == TouchPhase.Began) {
					this.initialTouch = t;
				} else if (t.phase == TouchPhase.Moved) {
					float deltaX = this.initialTouch.position.x - t.position.x;
					float deltaY = this.initialTouch.position.y - t.position.y;
					float distance = Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2));
					bool horizontalAttack = Mathf.Abs(deltaY / deltaX) < .2f;
					bool verticalAttack = Mathf.Abs(deltaY / deltaX) > 5f;

					if (distance > 100f) {
						if (horizontalAttack) {
							this.attackType = 1;
						} else if (verticalAttack) {
							this.attackType = 2;
						} else if (deltaX <= 0) {
							this.attackType = 4;
						} else if (deltaX > 0) {
							this.attackType = 8;
						}
						this.Attack();
					}
				} else if (t.phase == TouchPhase.Ended) {
					this.initialTouch = new Touch();
				}

			}

			if (Input.GetKeyUp(KeyCode.Alpha1)) {
				this.attackType = 1;
				this.Attack();
			} else if (Input.GetKeyUp(KeyCode.Alpha2)) {
				this.attackType = 2;
				this.Attack();
			} else if (Input.GetKeyUp(KeyCode.Alpha3)) {
				this.attackType = 4;
				this.Attack();
			} else if (Input.GetKeyUp(KeyCode.Alpha4)) {
				this.attackType = 8;
				this.Attack();
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		this.attackCollider = other;
	}
	
	void OnTriggerExit(Collider other) {
		this.attackCollider = null;
	}

	public override void TakeDamage(int enDamage, int enAttackType) {
		if (enAttackType == 15) {
			enDamage = enDamage * 2;
		}

		this.health = this.health - enDamage;
		if (this.health <= 0) {
			this.Die();
		}
	}

	public override void Die() {
	}
}
