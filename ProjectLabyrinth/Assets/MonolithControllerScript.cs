using UnityEngine;
using System.Collections;

public class MonolithControllerScript : MonoBehaviour {

    public Canvas canvasUIPrefab;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Monolith Triggered!");

        Instantiate(canvasUIPrefab);

    }
}
