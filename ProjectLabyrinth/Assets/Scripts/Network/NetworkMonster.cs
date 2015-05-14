﻿using UnityEngine;
using System.Collections;

public class NetworkMonster :  NetworkCharacter{

    protected override void UpdateAnimationState(int animationState)
    {
        if(animator == null)
        {
            return;
        }
        animator.SetInteger("Status", animationState);
    }
    protected override int DetermineAnimationState()
    {
        if(animator == null)
        {
            return 0;
        }
        int retVal = animator.GetInteger("Status");
        return retVal;
    }
    protected override void NormalizePosition()
    {
        return;
    }
    
}
