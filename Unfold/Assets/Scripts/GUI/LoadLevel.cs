using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
    public int levelToLoadIndex = 2;
    public int loadingIndex = 1;
	void Start () 
    {
        Application.LoadLevel(loadingIndex);
        DontDestroyOnLoad(transform.gameObject);
	}
    void Update()
    {
        if(Application.loadedLevel == loadingIndex)
        {
            Application.LoadLevel(levelToLoadIndex);
            Destroy(transform.gameObject);
        }
    }
}
