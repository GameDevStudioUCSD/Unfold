using UnityEngine;
using System.Collections;

public class NetworkCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (networkView.isMine)
		{
			camera.enabled = true;
			audio.enabled = true;
		}
		else
		{
			camera.enabled = false;
			audio.enabled = false;
		}
	}
	
}
