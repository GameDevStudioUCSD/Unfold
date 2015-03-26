using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {

	// Use this for initialization
    private float destroyTimer;
	void Start () {
        destroyTimer = Time.time + 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= destroyTimer)
            Destroy(this.gameObject);
	}
}
