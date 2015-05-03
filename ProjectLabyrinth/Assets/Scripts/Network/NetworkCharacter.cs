using UnityEngine;
using System.Collections;

public class NetworkCharacter : MonoBehaviour {
    Vector3 truePosition;
    Quaternion trueRotation;
    int animationState;
    RectTransform trans;
    NetworkView nView;
    Animator animator;
	// Use this for initialization
    public enum PlayerAnimation 
    {
        Idle,
        Walking 
    }
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
            trueRotation = trans.rotation;
            animationState = (int)DetermineAnimationState();
            stream.Serialize(ref truePosition);
            stream.Serialize(ref trueRotation);
            stream.Serialize(ref animationState);
        }
        else
        { // Receiving
            stream.Serialize(ref truePosition);
            stream.Serialize(ref trueRotation);
            stream.Serialize(ref animationState);
            UpdatePosition(trans.position, truePosition);
            UpdateRotation(trans.rotation, trueRotation);
            UpdateAnimationState((PlayerAnimation)animationState);
        }
	}
    void UpdatePosition(Vector3 a, Vector3 b)
    {
        if (Vector3.Distance(a, b) < .5f)
            return;
        trans.position = Vector3.Lerp(a, b, Time.deltaTime * 5);
    }
    void UpdateRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Angle(a, b) < 10)
            return;
        trans.rotation = Quaternion.Slerp(a, b, Time.deltaTime * 5);
    }
    void UpdateAnimationState(PlayerAnimation animation)
    {
        switch (animation)
        {
            case PlayerAnimation.Idle:
                animator.SetBool("Walking", false);
                break;
            case PlayerAnimation.Walking:
                animator.SetBool("Walking", true);
                break;
        }
    }
    PlayerAnimation DetermineAnimationState()
    {
        PlayerAnimation retVal = PlayerAnimation.Idle;
        if(animator.GetBool("Walking"))
        {
            retVal = PlayerAnimation.Walking;
        }
        return retVal;
    }
}
