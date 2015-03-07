using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * PlayerCharacter class
 *
 * Represents player stats and things the player can do
 */
public class PlayerCharacter : Character {
    public AudioClip[] attackSound;
	private Vector3 spawn;
	
	public Controller3D movementController;

	// Player health bar object
	public Slider healthBar;

	// Helps correlate user input to attack calculation
	private Touch initialTouch;

	void Start() {
		this.health = maxHealth;
		this.damage = 10;
		this.attackDelay = 1;
		this.moveSpeed = 10;
	}

	void FixedUpdate() {

		if (Time.time > nextAttackTime) {
			ParticleMovement p = (ParticleMovement) GetComponentInChildren<ParticleMovement>();

			if(Input.GetMouseButton (0)) {
				p.move (Input.mousePosition);
			}

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

		this.healthBar.value = this.health;
	}

    public override bool Attack()
    {
        bool hasAttacked = base.Attack();
        if(hasAttacked)
            SoundController.PlaySound(GetComponent<AudioSource>(), attackSound);
        return hasAttacked;
    }
	public override void TakeDamage(int enDamage, int enAttackType) {
		if (enAttackType == 15) {
			enDamage = enDamage * 2;
		}

		this.health = this.health - enDamage;
		if (debug_On)
			Debug.Log ("I've been got!");
		if (this.health <= 0) {
			this.Die();
		}
	}

	public override void Die() {
		if (debug_On)
			Debug.Log ("I am dead.");
		transform.position = spawn;
		this.health = maxHealth;
	}

	public void setSpawn(Vector3 start) {
		spawn = start;
	}
	
	public void addHealth(int h)
	{
		if (this.health + h <= maxHealth)
			this.health += h;
		else
			this.health = maxHealth;
	}
	
	public void addSpeed(float s)
	{
		this.moveSpeed += s;
		movementController.setMovementSpeed(this.moveSpeed);
	}
	
	public void addMaxHealth(int mh)
	{
		maxHealth += mh;
		health += mh;
	}
	
	public void addDamage(int d)
	{
		damage += d;
	}
}
