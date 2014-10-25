using UnityEngine;
using System.Collections;

public class Strobe : MonoBehaviour {

	// Use this for initialization
    private Light myLight;
    private bool on = false;


    void Start()
    {
        myLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (on)
            myLight.enabled = !myLight.enabled;
        else
            myLight.enabled = false;
        if (Input.GetKeyUp(KeyCode.Return))
        {
            on = !on;
        }
	}
}
