using UnityEngine;
using System.Collections;

public class BirdMovement : MonsterMovement {

	public override void maneuver()
	{
		if (transform.position.y < 4.0f)
			transform.Translate (Vector3.up * SPEED);
		else
			transform.Translate (Vector3.down * SPEED);
	}

}
