using UnityEngine;
using System.Collections;

public class Mortality : IFightable {
	public override void Die()
	{
		//TODO Write up a lose condition and terminate player.
		Destroy(gameObject);
		Debug.Log("DEAD!");
	}

}
