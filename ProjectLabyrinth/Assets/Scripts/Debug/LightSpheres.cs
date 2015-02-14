using UnityEngine;
using System.Collections;

public class LightSpheres : MonoBehaviour {
    public float red = 1;
    public float blue = 1;
    public float green = 1;
    private float savedR, savedB, savedG;
    private bool hasUpdated;
    private Color color;
	// Use this for initialization
	void Start () {
        savedR = red;
        savedB = blue;
        savedG = green;
        color = new Color(savedR, savedG, savedB);
        light.color = color;
	}
	
	// Update is called once per frame
	void Update () {
        hasUpdated = false;
        if( red != savedR )
        {
            savedR = red;
            hasUpdated = true;
        }
        if( green != savedG )
        {
            savedG = green;
            hasUpdated = true;
        }
        if( blue != savedB )
        {
            savedB = blue;
            hasUpdated = true;
        }
        if( hasUpdated )
        {
            color = new Color(savedR, savedG, savedB);
            light.color = color;
        }
	}
    public static float CalculateRGBValue( int value, int maxValue )
    {
        float returnValue = value / maxValue;
        return returnValue;
    }
}
