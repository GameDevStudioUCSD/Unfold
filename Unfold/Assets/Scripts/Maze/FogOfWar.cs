using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the "fog of war" lights in the game.
/// </summary>
public class FogOfWar : MonoBehaviour {

	public GameObject roofLight;

    void Start()
    {
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
	
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
		}
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter playerChar = other.GetComponent<PlayerCharacter>();
        NetworkView nView = other.GetComponent<NetworkView>();
        if (playerChar && nView.isMine)
        {
        	roofLight.SetActive(true);
            this.GetComponent<Light>().enabled = true;
        }
    }
}
