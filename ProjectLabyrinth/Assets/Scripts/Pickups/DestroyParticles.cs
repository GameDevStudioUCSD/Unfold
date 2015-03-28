using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {

	// Use this for initialization
    private float destroyTimer;
	void Start () {
        Transform transform;
        transform = GetComponent<Transform>();
        transform.localPosition = Vector3.zero;
        Quaternion rot = transform.rotation;
        rot.x = 270;
        rot.y = 0;
        rot.z = 0;
        destroyTimer = Time.time + 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= destroyTimer)
            Destroy(this.gameObject);
	}
}
