using UnityEngine;
using System.Collections;

public class EditFloor : MonoBehaviour {
    [RPC]
    public void ModifyFloorSize(float wallSize, int rows, int cols)
    {
        transform.localScale += new Vector3((wallSize * rows / 10), 0, (wallSize * cols / 10));
    }
    [RPC]
    public void UpdateTexture( int texture)
    {
        TextureController.TextureChoice actualTexture;
        actualTexture = (TextureController.TextureChoice) texture;
        TextureController tController = new TextureController(actualTexture);
        GetComponent<Renderer>().material.mainTexture = tController.GetFloorTexture();
    }
}
