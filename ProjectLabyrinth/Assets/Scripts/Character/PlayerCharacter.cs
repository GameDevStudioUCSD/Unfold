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

	private const float ROTATE_SPEED = 2.5f;
	public Animator animator;

	public ParticleMovement trail;

	// Helps correlate user input to attack calculation
	private Touch initialTouch;

	/*bonus stats are temporary upgrades that are gained from items, are removed once item changes or player dies*/
	private int bonusDamage;       //from weapon class
	private int bonusMaxHealth;        //from armor class
	private float bonusMoveSpeed;      //from boot

	private Item weapon = null;
	private Item armor = null;
	private Item boots = null;
	
	/*0 for no ability, other numbers for different types of abilities*/
	private int abilityType = 0;
	/*0 for no ability, 1 for basic ability, 2 for adept ability*/
	private int abilityLevel = 0;

	void Start() {
		this.animator.SetBool("Walking", false);
		this.currentHealth = baseMaxHealth;
		this.baseDamage = 10;
		this.attackDelay = 1;
		this.baseMoveSpeed = 10;
		this.bonusDamage = 0;
		this.bonusMaxHealth = 0;
		this.bonusMoveSpeed = 0;
		updateStats();
	}

	void FixedUpdate() {
		// dirty solution to players being pushed around by enemies
		transform.position = new Vector3 (transform.position.x, 1.5f, transform.position.z);

		if (Time.time > nextAttackTime) {
			//ParticleMovement p = (ParticleMovement) GetComponentInChildren<ParticleMovement>();


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
				this.createPath ();
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

	public void Idle(CNAbstractController joystick) {
		this.animator.SetBool("Walking", false);
		this.StopCoroutine("RotateCoroutine");
	}

	/// <summary>
	/// Causes this player character to move.
	/// </summary>
	/// <param name="input">Input movement vector.</param>
	/// <param name="joystick">Joystick controlling player movement.</param>
	public void Move(Vector3 input, CNAbstractController joystick) {
		this.animator.SetBool("Walking", true);
		Vector3 movement = new Vector3(input.x, 0f, input.y);
		movement = Camera.main.transform.TransformDirection(movement);
		movement.y = 0f;
//		movement.Normalize();	// Allow movement sensitivity

		StopCoroutine("RotateCoroutine");
		StartCoroutine("RotateCoroutine", movement);
		this.GetComponent<CharacterController>().Move(Time.deltaTime * moveSpeed * movement);
	}

	private IEnumerator RotateCoroutine(Vector3 direction) {
		if (direction == Vector3.zero) {
			yield break;
		}

		do {
			this.transform.rotation = Quaternion.Lerp(
				this.transform.rotation,
				Quaternion.LookRotation(direction),
				Time.deltaTime * PlayerCharacter.ROTATE_SPEED
			);
			yield return null;
		} while ((direction - this.transform.forward).sqrMagnitude > 0.2f);
	}

	public override bool Attack() {
		bool hasAttacked = base.Attack();
		if (hasAttacked)
			SoundController.PlaySound(GetComponent<AudioSource>(), attackSound);
		return hasAttacked;
	}

	public void createPath() {
		ParticleGenerator p = (ParticleGenerator)GetComponentInChildren<ParticleGenerator> ();
		if (attackType == 1) {
			ParticleMovement slash = p.createPath (new Vector3(Screen.width / 8.0f, Screen.height / 2.0f));
			//Debug.Log (slash.transform.position);
			slash.move (new Vector3(Screen.width * 7 / 8.0f, Screen.height / 2.0f));
			//Debug.Log (slash.transform.position);

			Destroy (slash);
		}

	}

	public override void TakeDamage(int enDamage, int enAttackType) {
		if (enAttackType == 15) {
			enDamage = enDamage * 2;
		}

		this.currentHealth = this.currentHealth - enDamage;
		if (debug_On)
			Debug.Log("Taking Damage: -" + enDamage);
		if (this.currentHealth <= 0) {
			this.Die();
		}
	}

	public override void Die() {
		if (debug_On)
			Debug.Log("I am dead.");
		transform.position = spawn;
		this.currentHealth = this.maxHealth;
		
		weapon = null;
		armor = null;
		boots = null;
		checkItemsForSet();
	}

	public void setSpawn(Vector3 start) {
		spawn = start;
	}

	public void addHealth(int h) {
		if (this.currentHealth + h <= baseMaxHealth)
			this.currentHealth += h;
		else
			this.currentHealth = baseMaxHealth;
	}

	public void addSpeed(float s) {
		this.baseMoveSpeed += s;
	}

	public void addMaxHealth(int mh) {
		baseMaxHealth += mh;
		currentHealth += mh;
	}

	public void addDamage(int d) {
		baseDamage += d;
	}

	public void equipItem(Item newItem) {
		switch (newItem.itemType)
		{
			case 0:
				this.weapon = newItem;
				break;
			case 1:
				this.armor = newItem;
				break;
			case 2:
				this.boots = newItem;
				break;
		}
		updateBonusStats();
		checkItemsForSet();
	}
	
	public void updateStats()
	{
		this.damage = baseDamage + bonusDamage;
		maxHealth = baseMaxHealth + bonusMaxHealth;
		this.moveSpeed = baseMoveSpeed + bonusMoveSpeed;
	}
	
	public void updateBonusStats()
	{
		bonusDamage = 0;
		bonusMaxHealth = 0;
		bonusMoveSpeed = 0;
		
		if (weapon)
		{
			bonusDamage += weapon.bonusDamage;
			bonusMaxHealth += weapon.bonusMaxHealth;
			bonusMoveSpeed += weapon.bonusMoveSpeed;
		}
		if (armor)
		{
			bonusDamage += armor.bonusDamage;
			bonusMaxHealth += armor.bonusMaxHealth;
			bonusMoveSpeed += armor.bonusMoveSpeed;
		}
		if (boots)
		{
			bonusDamage += boots.bonusDamage;
			bonusMaxHealth += boots.bonusMaxHealth;
			bonusMoveSpeed += boots.bonusMoveSpeed;
		}
		
		updateStats();
	}
	
	public void checkItemsForSet()
	{	
		if (weapon && armor)
		{
			if (weapon.setVal == armor.setVal)
			{
				abilityType = weapon.setVal;
				abilityLevel = 1;
			}
		}
		if (weapon && boots)
		{
			if (weapon.setVal == boots.setVal)
			{
				abilityType = weapon.setVal;
				abilityLevel = 1;
			}
		}
		if (armor && boots)
		{
			if (armor.setVal == boots.setVal)
			{
				abilityType = armor.setVal;
				abilityLevel = 1;
			}
		}
	
		if (weapon && armor && boots)
		{
			if (weapon.setVal == armor.setVal && weapon.setVal == boots.setVal)
			{
				abilityType = weapon.setVal;
				abilityLevel = 2;
			}
		}
	}
}


