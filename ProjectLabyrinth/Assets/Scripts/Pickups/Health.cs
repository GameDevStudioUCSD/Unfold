using UnityEngine;
using System.Collections;

public class Health : Pickup {
	// Update is called once per frame
	public override void pickedUp()
	{
		this.player.addHealth(5);
	}
}
