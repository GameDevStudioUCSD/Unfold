using UnityEngine;
using System.Collections;

public class RemoveConnectUI : MonoBehaviour {

    private int numberToDestroy = 5;
    private int numberFailedToDestroy = 0;
    void Update()
    {

        if (numberFailedToDestroy <= numberToDestroy)
        {

            GameObject connUI = GameObject.Find("ConnectionInfoCanvas(Clone)");
            if (connUI != null)
            {
                Destroy(connUI);
            }
            else
                numberToDestroy++;
        }
        else
            numberToDestroy = 0;
    }
	
}
