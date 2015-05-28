using UnityEngine;
using System.Collections;

public class Foil : Weapon {

	public AttackDetector detector;

	public Foil() {
		cooldown = 20;
	}
	
	public override void activate () {
		detector.transform.localPosition = new Vector3 (0, 0, 0);
		detector.transform.localScale = new Vector3(7, 3.5f, 7);
	}
	
	public override void deactivate () {
		detector.transform.localPosition = new Vector3(0, 0.4f, 2);
		detector.transform.localScale = new Vector3(2.5f, 3.5f, 3.5f);
	}
}
