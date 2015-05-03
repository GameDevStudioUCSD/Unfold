using UnityEngine;
using System.Collections;

public class NetworkCharacter : MonoBehaviour {
    Vector3 truePosition;
    RectTransform trans;
    NetworkView nView;
    Animator animator;
	// Use this for initialization
	void Start () {
        nView = this.GetComponent<NetworkView>();
        trans = this.GetComponent<RectTransform>();
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if(stream.isWriting)
        { //Sending
            truePosition = trans.position;
            stream.Serialize(ref truePosition);
        }
        else
        { // Receiving
            stream.Serialize(ref truePosition);
            UpdatePosition(trans.position, truePosition);
        }
	}
    void UpdatePosition(Vector3 a, Vector3 b)
    {
        if (Vector3.Distance(a, b) < .5f)
            return;
        trans.position = Vector3.Lerp(a, b, Time.deltaTime * 5);
    }
}
