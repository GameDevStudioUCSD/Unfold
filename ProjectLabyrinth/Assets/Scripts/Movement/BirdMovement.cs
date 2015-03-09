using UnityEngine;
using System.Collections;

public class BirdMovement : MonsterMovement {
	Vector3 vertical = Vector3.up;
	float t = 0f;
	float del = .07f;

	public override void maneuver()
	{
		/*if (transform.position.y < 4.0f)
			transform.Translate (Vector3.up * SPEED);

		else if (transform.position.y > 5.0f)
			transform.Translate (Vector3.up * SPEED);*/
		float change = del * Mathf.Cos (t);
		transform.Translate(Vector3.up * change);
		t += .03f;
	}

	public override void AI()
	{
		transform.Translate (Vector3.forward * SPEED);
		
		if (canTurn && isInCenter ()) {
			Square curr = getCurrSquare(transform.position.x, transform.position.z);
			bool[] sides = getSides (curr, transform.position.x, transform.position.z);
			if (isFork (sides)) {
				bool found = false;
				sides[(direction + 2) % 4] = true; // Don't want to turn around
				
				while (!found) {
					int side = Random.Range (0, 4);
					found = !sides[side];
					
					if (found) {
						turn (side);
					}
				}
			} else if (isCorner(sides)) {
				sides[(direction + 2) % 4] = true;
				
				if(sides[(direction + 1) % 4]) {
					turn ((direction + 3) % 4);
				} else {
					turn ((direction + 1) % 4);
				}
			} else if (isDeadEnd(sides)) {
				turn ((direction + 2) % 4);
			}
			canTurn = false;
		} else if (!canTurn && movingVert () && Mathf.Abs (transform.position.x - Mathf.Round (transform.position.x)) < .2 && 
		           Mathf.Round (transform.position.x) % mazeGen.wallSize == Mathf.Round(mazeGen.wallSize / 2)) {
			
			canTurn = true;
		} else if (!canTurn && movingHoriz () && Mathf.Abs (transform.position.z - Mathf.Round (transform.position.z)) < .2 && 
		           Mathf.Round (transform.position.z) % mazeGen.wallSize == Mathf.Round(mazeGen.wallSize / 2)) {
			
			canTurn = true;
		}
	}
}
