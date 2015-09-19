using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls behavior of the ingame credits scene.
/// </summary>
public class Credits : MonoBehaviour {

	/// <summary>
	/// File to read from containing credits information.
	/// </summary>

	/// <summary>
	/// Text object displaying the credits in the game UI.
	/// </summary>
	public Text creditsText;

	/// <summary>
	/// Speed at which the credits page scrolls upwards.
	/// </summary>
	public float speed = 1.0f;

	public void Awake() {
		//creditsText.alignment = TextAnchor.MiddleCenter;
	}

	public void Update() {
		this.creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
		Debug.Log (this.creditsText.GetComponent<RectTransform>().position.y);
	}
}
