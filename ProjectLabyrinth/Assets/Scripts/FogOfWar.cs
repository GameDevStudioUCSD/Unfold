using System.Collections;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

	public Light playerHasSeen;

	public PlayerCharacter player;

	void Update() {
		Light.Instantiate(this.playerHasSeen, player.transform.position, Quaternion.identity);
	}
}
