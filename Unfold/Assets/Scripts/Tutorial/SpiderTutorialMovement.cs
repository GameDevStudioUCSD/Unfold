using UnityEngine;
using System.Collections;

public class SpiderTutorialMovement : TutorialMovement {
	
	bool forward = true;
	int counter = 0;
	
	public override void maneuver()
	{
		if(!isClose)
			transform.Translate (Vector3.forward * SPEED);
	}

	// For when the monster is really close
	public override void doClose(Transform player) {
		transform.LookAt (new Vector3(player.position.x, 0, player.position.z));
	}
	
	// special for birds, but needs to be defined for others
	public override bool canAttack() {
		return true;
	}
	
	public override void doAttack() {
		if(this.forward) {
			this.transform.Translate (Vector3.forward * .2f);
			this.counter += 2;
		}
		
		if(this.counter == 10) {
			this.forward = false;
		}
		
		if(!this.forward) {
			this.transform.Translate (Vector3.back * .1f);
			this.counter -= 1;
		}
		
		if(!this.forward && counter == 0) {
			this.attacking = false;
			this.forward = true;
		}
		
	}
	
}
