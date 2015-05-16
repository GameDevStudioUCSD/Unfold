using UnityEngine;
using System.Collections;

public class Sword : Weapon {

	public AttackDetector detector;
	
	public Sword() {
		cooldown = 200;
	}
	
	public override void activate () {
		detector.transform.localPosition = new Vector3(0, .4f, 10);
		detector.transform.localScale = new Vector3(2.5f, 3.5f, 20);
	}

	public override void deactivate () {	
		detector.transform.localPosition = new Vector3(0, 0.4f, 2);
		detector.transform.localScale = new Vector3(2.5f, 3.5f, 3.5f);
	}
}
