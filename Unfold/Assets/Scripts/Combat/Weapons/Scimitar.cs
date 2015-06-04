using UnityEngine;
using System.Collections;

public class Scimitar : Weapon {

	public PlayerCharacter player;
	private int basedmg;
	
	public Scimitar() {
		cooldown = 200;
	}
	
	public override void activate () {
		player.bonusDamage = 10;
		player.updateStats();
	}
	
	public override void deactivate () {
		player.bonusDamage = 0;
		player.updateStats();
	}
}
