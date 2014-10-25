using UnityEngine;
using System.Collections;

public class Wave_Movement : MonoBehaviour {
    public float factor = 90;
    public float frequency = 1;
    private float amplitude;
	// Use this for initialization
	void Start () {
        amplitude = transform.position.y * 2;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(0, Mathf.Sin(transform.position.y * Mathf.PI * Time.deltaTime)/factor, 0, Space.Self);
        transform.position += amplitude*(Mathf.Sin(2*Mathf.PI*frequency*Time.time) - Mathf.Sin(2*Mathf.PI*frequency*(Time.time - Time.deltaTime)))*transform.up;
	}
}
