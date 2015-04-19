using UnityEngine;
using System.Collections;

public class MonolithControllerScript : MonoBehaviour {

    public Object canvasUIPrefab;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(canvasUIPrefab);
    }
}
