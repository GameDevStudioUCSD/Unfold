using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public string creditsFile;

	public Text creditsText;

	public float speed = 1.0f;

	public void Awake() {
		this.creditsText.text = File.ReadAllText(Application.dataPath + this.creditsFile);
	}

	public void Update() {
		this.creditsText.transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
