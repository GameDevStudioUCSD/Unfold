using UnityEngine;
using System.Collections;

public class MultiplayerBack : MonoBehaviour {

    public string nextScene = "MainMenu";

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Application.LoadLevel(nextScene);
        }
    }
}
