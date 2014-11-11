using UnityEngine;
using System.Collections;

public class IFightable : MonoBehaviour {

	//GameObject lockOnArrow;
	public int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
	
	public void DecrementHealth (int damage)
	{
		Debug.Log("Taking " + damage + " damage! " + health + " health remaining.");
		health -= damage;
		if (health <= 0)
			Die();
	}
	
	void Die ()
	{
		Debug.Log("DEAD!");
	}
}
