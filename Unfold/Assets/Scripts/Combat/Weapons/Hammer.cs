using UnityEngine;
using System.Collections;

/**
 * Author: Runjie Guan
 * The class for the hammer weapon
 * Actually it is unnecessary to add codes here, all the codes that deal with breaking the wall are and have to be in PlayerCharacter.cs
 * Check the codes in attack() for more detail
 */

public class Hammer : Weapon {
	
	public Hammer() {
		cooldown = 1000;
	}
	
	public override void activate () {
	}
	
	public override void deactivate () {
	}
}