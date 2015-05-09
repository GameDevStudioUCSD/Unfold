using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	void Start() {
	}

	void Update() {
	}

	public int cooldown { get; set; }


	public abstract void activate();
	public abstract void deactivate();
}
