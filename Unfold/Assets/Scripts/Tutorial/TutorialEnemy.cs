using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialEnemy : EnemyCharacter {

	public Text txt;
	public string message;
	public Teleport tele;

	public override void Die() {
		txt.text = message;
		if (tele == null) {
			;
		} 
		else {
			tele.active = true;
		}

		base.Die ();
	}
}
