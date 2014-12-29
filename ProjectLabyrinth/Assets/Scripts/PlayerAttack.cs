using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public int AttackDamage;
	public int AttackDelay;

	private Collider attackCollider;
	private GameObject attackObject;
	private Touch initialTouch = new Touch();
	private bool hasSwiped = false;
	private float distance = 0;
	private float nextAttackTime = 0;
	
	void FixedUpdate()
	{
		if (Time.time > nextAttackTime)
		{
			foreach(Touch t in Input.touches)
			{
				if (t.phase== TouchPhase.Began)
				{
					initialTouch = t;
				}
				else if (t.phase == TouchPhase.Moved && !hasSwiped)
				{
					float deltaX = initialTouch.position.x - t.position.x;
					float deltaY = initialTouch.position.y - t.position.y;
					distance = Mathf.Sqrt((deltaX*deltaX) + (deltaY*deltaY));
					bool swipedHorizontally = Mathf.Abs(deltaY/deltaX) < .2f;
					bool swipedVertically = Mathf.Abs(deltaY/deltaX) > 5f;
					
					
					if (distance > 100f)
					{
						if (swipedHorizontally && deltaX > 0) //swiped right
						{
							Attack (1);
						}
						else if (swipedHorizontally && deltaX <= 0) //swiped left
						{
							Attack (1);
						}
						else if (swipedVertically && deltaY > 0) //swiped up
						{
							Attack (2);
						}
						else if (swipedVertically && deltaY <= 0) //swiped down
						{
							Attack (2);
						}
						else if (!swipedVertically && !swipedHorizontally && deltaX <= 0) //swiped diagonal1
						{
							Attack (4);
						}
						else if (!swipedVertically && !swipedHorizontally && deltaX < 0) //swiped diagonal2
						{
							Attack (8);
						}
						hasSwiped = true;
					}
				}
				else if (t.phase == TouchPhase.Ended)
				{
					initialTouch = new Touch();
					hasSwiped = false;
				}
			}
		
			if (Input.GetKeyUp(KeyCode.Alpha1))
			{
				Debug.Log ("1 Pressed");
				Attack (1);
			}
			if (Input.GetKeyUp(KeyCode.Alpha2))
			{
				Debug.Log ("2 Pressed");
				Attack (2);
			}
			if (Input.GetKeyUp(KeyCode.Alpha3))
			{
				Debug.Log ("3 Pressed");
				Attack (4);
			}
			if (Input.GetKeyUp(KeyCode.Alpha4))
			{
				Debug.Log ("4 Pressed");
				Attack (8);
			}
		}
	}
	
	void Attack(int attackType)
	{
		if (attackCollider && attackObject)
		{
			Debug.Log ("Attacking " + attackCollider.name);
			attackCollider.rigidbody.AddForce(Vector3.forward * 100f, ForceMode.Acceleration);
			attackCollider.rigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);
			IFightable target = (IFightable)attackObject.GetComponent("IFightable");
			if (target)
				target.Attacked(AttackDamage, attackType);
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
