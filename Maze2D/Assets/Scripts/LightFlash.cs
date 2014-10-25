using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour
{
    public float duration = 1.0F;
    public Color color0 = Color.red;
    public Color color1 = Color.blue;
    private Light myLight;


    void Start()
    {
        myLight = GetComponent<Light>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
        }
        float t = Mathf.PingPong(Time.time, duration) / duration;
        light.color = Color.Lerp(color0, color1, t);
    }
}