using System;
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

	// Time before next attack is available
	public float attackDelay;

	// How fast the character can navigate the maze
	public float moveSpeed;
}
