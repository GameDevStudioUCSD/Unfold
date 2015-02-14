using UnityEngine;
using System.Collections;

public class NetworkCamera : MonoBehaviour {

	// Use this for initialization
    private AudioListener audio;
	void Start () {
        audio = GetComponent<AudioListener>();
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
