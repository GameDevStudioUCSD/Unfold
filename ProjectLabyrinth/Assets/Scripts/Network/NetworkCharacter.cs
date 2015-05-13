using UnityEngine;
using System.Collections;

public abstract class NetworkCharacter : MonoBehaviour {
    protected Vector3 truePosition;
    protected Quaternion trueRotation;
    protected int animationState;
    protected float lastNetworkMessage, timeBetweenNetworkMessage, lerpVal;
    protected float startTime, endTime, currentTime;
    protected RectTransform trans;
    protected NetworkView nView;
    public int modVal = 2;
    protected int updateCounter;
        
    protected Animator animator;
    protected bool hasStarted = false;
    public enum PlayerAnimation 
    {
        Idle,
        Walking 
    }
	void Start () {
        hasStarted = true;
        nView = this.GetComponent<NetworkView>();
        trans = this.GetComponent<RectTransform>();
        animator = this.GetComponentInChildren<Animator>();
        lastNetworkMessage = Time.time;
        timeBetweenNetworkMessage = lastNetworkMessage;
        updateCounter = 0;
	}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream == null || !hasStarted)
            return;
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
            float tempTime = Time.time;
            timeBetweenNetworkMessage = tempTime;
            timeBetweenNetworkMessage -= lastNetworkMessage;
            lastNetworkMessage = tempTime;
            SetTimes();
            stream.Serialize(ref truePosition);
            stream.Serialize(ref trueRotation);
            stream.Serialize(ref animationState);
        }
	}
    void Update()
    {
        if(!nView.isMine && updateCounter % modVal == 0)
        {
            currentTime = (Time.time - lastNetworkMessage);
            lerpVal = (currentTime / endTime);
            UpdatePosition(trans.position, truePosition);
            UpdateRotation(trans.rotation, trueRotation);
            UpdateAnimationState((PlayerAnimation)animationState);
        }
        updateCounter++;
    }
    void SetTimes()
    {
        startTime = Time.time - lastNetworkMessage;
        endTime = startTime + timeBetweenNetworkMessage;
    }
    void UpdatePosition(Vector3 a, Vector3 b)
    {
        if (a == null || b == null)
            return;
        if (Vector3.Distance(a, b) < .1f)
            return;
        trans.position = Vector3.Lerp(a, b, Time.deltaTime * 5);
    }
        
    void UpdateRotation(Quaternion a, Quaternion b)
    {
        if (a == null || b == null)
            return;
        if (Quaternion.Angle(a, b) < 1)
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
