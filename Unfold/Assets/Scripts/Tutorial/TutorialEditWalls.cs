using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialEditWalls : EditWalls {

	public Text txt;
	public string message;

	public override void DestroyWall() {
		txt.text = message;
		base.DestroyWall ();
	}
}
