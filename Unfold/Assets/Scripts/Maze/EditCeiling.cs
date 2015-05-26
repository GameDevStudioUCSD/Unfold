using UnityEngine;
using System.Collections;

public class EditCeiling : MonoBehaviour {
    private Transform ceilingTransform;
    void Start()
    {
        GameObject mazeRoot = GameObject.Find("Maze");
        if (mazeRoot != null)
        {
            ceilingTransform = GetComponent<Transform>();
            ceilingTransform.parent = mazeRoot.GetComponent<Transform>();
        }
    }
    [RPC]
    public void ModifyCeilingSize(float wallSize, int rows, int cols)
    {
		this.transform.position -= new Vector3(wallSize / 2, 0, wallSize / 2);
        transform.localScale += new Vector3((wallSize * rows / 10) - 1, 0, (wallSize * cols / 10) - 1);
    }
    [RPC]
    public void UpdateTexture( int texture)
    {
        TextureController.TextureChoice actualTexture;
        actualTexture = (TextureController.TextureChoice) texture;
        TextureController tController = new TextureController(actualTexture);
        GetComponent<Renderer>().material.mainTexture = tController.GetCeilingTexture();
    }
}
