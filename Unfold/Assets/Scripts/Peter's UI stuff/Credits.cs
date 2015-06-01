using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public Text creditsText;

	public float speed = 1.0f;

	public void Update() {
		this.creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
