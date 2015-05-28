using UnityEngine;
using System.Collections;

public class NetworkCamera : MonoBehaviour {

	// Use this for initialization
    private AudioListener audio;
	void Start () {
        audio = GetComponent<AudioListener>();
		if (GetComponent<NetworkView>().isMine)
		{
			GetComponent<Camera>().enabled = true;
            audio.enabled = true;
		}
		else
		{
			GetComponent<Camera>().enabled = false;
			audio.enabled = false;
		}
	}
	
}
