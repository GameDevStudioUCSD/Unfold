using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {

	public int AttackDamage;
	public int AttackDelay;
	public int AttackType;	

	private Collider attackCollider;
	private GameObject attackObject;
	private float nextAttackTime = 0;

	void FixedUpdate()
	{
		if (Time.time > nextAttackTime) 
		{
			bool power = false;
			//Idea: Randomly increase AttackDamage at intervals for "power attacks"
			if ((int)nextAttackTime % 17 == 0)
			{
				AttackDamage *= 2;
				power = true;
			}
			Attack(AttackType);
			if (power)
			{
				AttackDamage /= 2;
			}
		}
	}

	void Attack(int attackType)
	{
		if (attackCollider && attackObject)
		{
			Debug.Log ("Attacking " + attackCollider.name);
			attackCollider.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100f, ForceMode.Acceleration);
			attackCollider.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f, ForceMode.Acceleration);
			IFightable target = (IFightable)attackObject.GetComponent("IFightable");
			if (target)
			{
				Debug.Log ("Ouch!");
				target.Attacked(AttackDamage, attackType);
			}
		}
		nextAttackTime = Time.time + AttackDelay;
		Debug.Log ("nextAttackTime: " + nextAttackTime + " CurrentTime: " + Time.time);
	}
	
	void OnTriggerEnter(Collider other)
	{
		attackCollider = other;
		attackObject = attackCollider.gameObject;
		Debug.Log ("Collider set to " + attackCollider.name);
	}
	
	void OnTriggerExit(Collider other)
	{
		attackCollider = null;
		attackObject = null;
		Debug.Log ("Collider set to null");
	}	
}
