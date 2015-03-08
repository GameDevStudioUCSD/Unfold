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


	
	private int bonusDamage;       //from weapon class
	private int bonusMaxHealth;        //from weapon class
	private float bonusSpeed;      //from boot

	private GameObject weapon;


	void Start() {
		this.currentHealth = baseMaxHealth;
		this.baseDamage = 10;
		this.attackDelay = 1;
		this.baseMoveSpeed = 10;
		this.bonusDamage = 0;
		this.bonusMaxHealth = 0;
		this.bonusSpeed = 0;
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

		this.healthBar.value = this.currentHealth;
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

		this.currentHealth = this.currentHealth - enDamage;
		if (debug_On)
			Debug.Log ("I've been got!");
		if (this.currentHealth <= 0) {
			this.Die();
		}
	}

	public override void Die() {
		if (debug_On)
			Debug.Log ("I am dead.");
		transform.position = spawn;
		this.currentHealth = this.maxHealth;
	}

	public void setSpawn(Vector3 start) {
		spawn = start;
	}
	
	public void addHealth(int h)
	{
		if (this.currentHealth + h <= baseMaxHealth)
			this.currentHealth += h;
		else
			this.currentHealth = baseMaxHealth;
	}
	
	public void addSpeed(float s)
	{
		this.baseMoveSpeed += s;
		movementController.setMovementSpeed(this.baseMoveSpeed);
	}
	
	public void addMaxHealth(int mh)
	{
		baseMaxHealth += mh;
		maxHealth += mh;
	}
	
	public void addDamage(int d)
	{
		baseDamage += d;
	}

	public void changeItem(int bonusDamage, int bonusMaxHealth, float bonusSpeed){
		this.bonusDamage = bonusDamage;
		this.bonusMaxHealth = bonusMaxHealth;
		this.bonusSpeed = bonusSpeed;
		this.bonusMaxHealth = bonusMaxHealth;
		this.damage = this.baseDamage + this.bonusDamage;
		this.maxHealth = this.baseMaxHealth  + bonusMaxHealth;
		this.moveSpeed = this.baseMoveSpeed - this.bonusSpeed + bonusSpeed;

	}

	public void setWeapon(GameObject newWeapon){
		this.weapon = newWeapon;
	}
}   
 

