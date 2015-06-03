using UnityEngine;
using System.Collections;

public class NetworkUser : NetworkCharacter{

    public enum PlayerAnimation
    {
        Idle,
        Walking,
        Vertical1,
        Vertical2,
        Horizontal1,
        Horizontal2
    }
    protected override void UpdateAnimationState(int animationState)
    {
        UpdateAnimationState((PlayerAnimation)animationState);
    }
    protected override void NormalizePosition()
    {
        // Detect oscillations
        // If the actual y value isn't equal to the current y value,
        // then there is a strong chance there might be an oscillation
        if(trans.position.y != lastY || differenceCounter > DIFFERENCETHRESHOLD)
        {
            // Increment difference counter
            differenceCounter++;
            // Update lastY
            lastY = trans.position.y;
        }
        // Otherwise, there probably isn't any oscillations
        else
        {
            // Reset difference counter
            differenceCounter = 0;
        }
        // Only normalize the vector if the on a client machine and difference
        // counter does not exceed the threshold
        if (Network.isClient && differenceCounter < DIFFERENCETHRESHOLD)
            truePosition += (1.5f * Vector3.up);

    }
    protected override int DetermineAnimationState()
    {
        PlayerAnimation retVal = PlayerAnimation.Idle;
        int attackState = animator.GetInteger("Attack");
        if (attackState != 0)
        {
            switch (attackState)
            {
                case 1:
                    retVal = PlayerAnimation.Vertical1;
                    break;
                case 2:
                    retVal = PlayerAnimation.Vertical2;
                    break;
                case 3:
                    retVal = PlayerAnimation.Horizontal1;
                    break;
                case 4:
                    retVal = PlayerAnimation.Horizontal2;
                    break;
            }
        }
        else if (animator.GetBool("Walking"))
        {
            retVal = PlayerAnimation.Walking;
        }
        
        return (int)retVal;
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
            case PlayerAnimation.Vertical1:
                animator.SetInteger("Attack", 1);
                break;
            case PlayerAnimation.Vertical2:
                animator.SetInteger("Attack", 2);
                break;
            case PlayerAnimation.Horizontal1:
                animator.SetInteger("Attack", 3);
                break;
            case PlayerAnimation.Horizontal2:
                animator.SetInteger("Attack", 4);
                break;
        }
    }
}
