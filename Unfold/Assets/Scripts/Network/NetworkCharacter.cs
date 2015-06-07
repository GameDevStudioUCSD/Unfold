using UnityEngine;
using System.Collections;

public abstract class NetworkCharacter : MonoBehaviour {
    public Vector3 truePosition;
    protected Quaternion trueRotation;
    protected int animationState;
    protected float lastNetworkMessage, timeBetweenNetworkMessage, lerpVal;
    protected float startTime, endTime, currentTime;
    protected Transform trans;
    protected NetworkView nView;
    public int modVal = 2;
    protected int updateCounter;

    // Variable used by oscillation dector for number of previous oscillations
    protected int differenceCounter;
    // Variable to store last y value for oscillation checks
    protected float lastY;
    // Constant to define greatest possible differences before we cease vector
    // normalization
    protected const uint DIFFERENCETHRESHOLD = 4;
    protected Animator animator;
    protected bool hasStarted = false;
    
    protected abstract void UpdateAnimationState(int animationState);
    protected abstract int DetermineAnimationState();
    protected abstract void NormalizePosition();

	void Start () {
        hasStarted = true;
        nView = this.GetComponent<NetworkView>();
        trans = this.GetComponent<Transform>();
        if (trans == null)
            this.GetComponent<Transform>();
        animator = this.GetComponent<Animator>();
        if(animator == null)
            animator = this.GetComponentInChildren<Animator>();
        lastNetworkMessage = Time.time;
        timeBetweenNetworkMessage = lastNetworkMessage;
        updateCounter = 0;
        truePosition = trans.position;
        lastY = 0;
        differenceCounter = 0;
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
            NormalizePosition();
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
            UpdateAnimationState(animationState);
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
        if (Vector3.Distance(a, b) < .1f)
            return;
        trans.position = Vector3.Lerp(a, b, lerpVal);
    }

    void UpdateRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Angle(a, b) < 1)
            return;
        trans.rotation = Quaternion.Slerp(a, b, lerpVal);
    }
    
}
