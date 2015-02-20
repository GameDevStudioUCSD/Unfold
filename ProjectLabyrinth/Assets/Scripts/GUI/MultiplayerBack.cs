using UnityEngine;
using System.Collections;

public class MultiplayerBack : MonoBehaviour {

    public string nextScene = "MainMenu";

	void OnTriggerEnter()
    {
        Application.LoadLevel(nextScene);
    }
}
