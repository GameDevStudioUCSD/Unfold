using UnityEngine;
using System.Collections;

abstract public class IFightable : MonoBehaviour {

	public int health;
	
	/*
	0000 = no weaknesses
	0001 = horizontal weakness
	0010 = vertical weakness
	0100 = diagonal1 weakness (topright -> bottomleft)
	1000 = diagonal2 weakness (topleft -> bottomright)
	*/
	public int weakness;
	
	/*
		damage is the damage the player attempts to afflict onto the object
		attackType is the direction of the sword slash:
		1 = horizontal
		2 = vertical
		4 = diagonal1 (topright -> bottomleft)
		8 = diagonal2 (topleft -> bottomright)
	*/
	public void Attacked (int damage, int attackType)
	{
		if ((attackType & weakness) == 0)
			damage /= 2;
		
		DecrementHealth(damage);
	}
	
	void DecrementHealth (int damage)
	{
		health -= damage;
		Debug.Log("Taking " + damage + " damage! " + health + " health remaining.");
		if (health <= 0)
			Die();
	}
	
	abstract public void Die ();
}
