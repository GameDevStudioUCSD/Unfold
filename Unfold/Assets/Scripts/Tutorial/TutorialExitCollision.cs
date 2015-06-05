using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialExitCollision : ExitCollision {

	public Text txt;

	protected override void performWin(PickupDetector hitDetector) {
		txt.text = "";
		base.performWin (hitDetector);
	}
}
