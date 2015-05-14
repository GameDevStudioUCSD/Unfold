using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the "fog of war" lights in the game.
/// </summary>
public class FogOfWar : MonoBehaviour {


	void OnDrawGizmosSelected() {
		Color color = Gizmos.color;
		Gizmos.color = Color.blue;

		Rect square = this.GetComponent<RectTransform>().rect;
		Gizmos.DrawWireCube(
			this.transform.position,
			new Vector3(square.width, 0, square.height)
		);

		Gizmos.color = color;
	}

	void OnEnable() {
		GameObject mazeRoot = GameObject.Find("Maze/Fog_of_War");
		if (mazeRoot) {
			this.transform.SetParent(mazeRoot.transform);
            if (Network.isClient)
            {
                mazeRoot.transform.position -= Vector3.up;
            } 
		}
	}

	void OnTriggerEnter(Collider other) {
		if (this.GetComponent<NetworkView>() == null ) {
            PlayerCharacter playerChar = other.GetComponent<PlayerCharacter>();
            NetworkView nView = other.GetComponent<NetworkView>();
			if (playerChar && nView.isMine) {
				this.GetComponent<Light>().enabled = true;
			}
		}
	}
}
