using UnityEngine;
using System.Collections;

public class NetworkMonster :  NetworkCharacter{

    protected override void UpdateAnimationState(int animationState)
    {
        animator.SetInteger("Status", animationState);
    }
    protected override int DetermineAnimationState()
    {
        int retVal = animator.GetInteger("Status");
        return retVal;
    }
    
}
