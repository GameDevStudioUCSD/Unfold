using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool isClose { get; set; }

	public abstract void setAttacking(bool state);

	public abstract void stun();
}
