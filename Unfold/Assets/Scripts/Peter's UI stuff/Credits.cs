using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public string creditsFile;

	public Text creditsText;

	public float speed = 1.0f;

	public void Awake() {
		try {
			this.creditsText.text = File.ReadAllText(Application.dataPath + this.creditsFile);
		} catch (IOException e) {
		}
	}

	public void Update() {
		this.creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
