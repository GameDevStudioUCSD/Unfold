using UnityEngine;
using System.Collections;

public class ModifyWallSize : MonoBehaviour {
    public bool debugOn;
    private Transform wallTransform;
    private void SetupWall()
    {
        wallTransform = GetComponent<Transform>();
        wallTransform = wallTransform.Find("InnerWall");
        wallTransform.localPosition += (Vector3.right * (.5f));
        if(debugOn)
        {
            Debug.Log(wallTransform);
        }
    }
    [RPC]
    private void ShrinkWall()
    {
        SetupWall();
        wallTransform.localScale -= Vector3.forward;
    }
    [RPC]
    private void ExpandWall()
    {
        SetupWall();
        wallTransform.localScale += Vector3.forward;
    }
}
