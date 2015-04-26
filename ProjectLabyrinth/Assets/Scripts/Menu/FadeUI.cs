using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class FadeUI : MonoBehaviour {
    public Graphic[] elementsToFade;
    public float lengthOfFade = 2;
    public float lengthBeforeFade = 2;
    private Color[] originalColors;
    private Color[] endColors;
    private float startTime ;
	void Start () {
        originalColors = new Color[elementsToFade.Length];
        endColors = new Color[elementsToFade.Length];
        int i = 0;
        float r, g, b;
        // Save a copy of each original color
        foreach (Graphic elem in elementsToFade)
        {
            r = elem.color.r;
            g = elem.color.g;
            b = elem.color.b;
            originalColors[i] = new Color(r, g, b, 1);
            endColors[i] = new Color(r, g, b, 0);
            i++;
        }
        // Set the start and end times
        startTime = Time.time + lengthBeforeFade;
	}
	// Update is called once per frame
	void Update () {
        if (Time.time < startTime)
            return;
        float percentToLerp = (Time.time - startTime)/lengthOfFade;
        int i = 0;
        foreach(Graphic elem in elementsToFade)
        {
            elem.color = Color.Lerp(originalColors[i], endColors[i], percentToLerp);
            i++;
        }
        if (Time.time - startTime > lengthOfFade)
            Destroy(this.GetComponent<Transform>().gameObject);
	}
}
