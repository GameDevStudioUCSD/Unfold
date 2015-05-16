using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the "fog of war" lights in the game.
/// </summary>
public class FogOfWar : MonoBehaviour {

	/// <summary>
	/// The network view controlling each player.
	/// </summary>
	public NetworkView networkView;

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
		GameObject mazeRoot = GameObject.Find("Maze");
		if (mazeRoot) {
			this.transform.SetParent(mazeRoot.transform);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (this.networkView == null || this.networkView.isMine) {
			if (other.GetComponent<PlayerCharacter>()) {
				this.GetComponent<Light>().enabled = true;
			}
		}
	}
}
