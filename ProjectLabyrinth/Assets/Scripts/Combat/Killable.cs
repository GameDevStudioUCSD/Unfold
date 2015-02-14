using UnityEngine;
using System.Collections;

public class Killable : IFightable {

	public override void Die()
	{
		//Add item drops and destroy instance
		Debug.Log("MONSTER KILLED!");
	}
}
