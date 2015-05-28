using UnityEngine;
using System.Collections;

public class UpdatePositionOnStart : MonoBehaviour {

    public bool updateOnlyIfClient = false;
    public bool updateOnlyIfHost = false;
    public Vector3 translation;
    string err = "You can only choose either updateOnlyIfClient or updateOnlyIfHost!";
	void Start () {
        if (updateOnlyIfClient && updateOnlyIfHost)
        {
            Debug.LogError(err);
        }
        if (updateOnlyIfHost && Network.isClient)
            return;
        if (updateOnlyIfClient && Network.isServer)
            return;
        this.GetComponent<Transform>().position += translation;

	}
	
}
