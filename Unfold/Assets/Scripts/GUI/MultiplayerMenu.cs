using UnityEngine;
using System.Collections;

public class MultiplayerMenu : MonoBehaviour {

    public string nextScene = "MultiplayerMenu";

	void OnTriggerEnter(Collider collider)
    {
        Application.LoadLevel(nextScene);
    }
}
