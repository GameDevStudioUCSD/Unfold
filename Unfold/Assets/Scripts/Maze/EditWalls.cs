using UnityEngine;
using System.Collections;

public class EditWalls : MonoBehaviour {
	public bool debugOn = false;
	private Transform wallTransform;
	public int health = 3;
    public bool canDestroy = true;

	public MazeGeneratorController mazegen;

	void Start() {
        GameObject wallRoot = null;
        if (canDestroy)
            wallRoot = GameObject.Find("Maze");
        else
            wallRoot = GameObject.Find("Maze/OuterWalls");
		if (wallRoot != null) {
			wallTransform = GetComponent<Transform>();
			wallTransform.parent = wallRoot.GetComponent<Transform>();
		}
	}
	private void FindInnerWall() {
		wallTransform = GetComponent<Transform>();
		wallTransform = wallTransform.Find("InnerWall");
	}
	private void SetupWall() {
		FindInnerWall();
		wallTransform.localPosition += (Vector3.right * (.5f));
		if (debugOn) {
			Debug.Log(wallTransform);
		}
	}
	[RPC]
	private void ShrinkWall() {
		SetupWall();
		wallTransform.localScale -= Vector3.forward;
	}
	[RPC]
	private void ExpandWall() {
		SetupWall();
		wallTransform.localScale += Vector3.forward;
	}

	/// <summary>
	/// Try to destory the wall
	/// </summary>
	public virtual void DestroyWall() {
		if (--health <= 0 && canDestroy) {
			Destroy(gameObject);
		}
	}

	[RPC]
	public void UpdateTexture(int texture) {
		Renderer rend;
		FindInnerWall();
		rend = wallTransform.gameObject.GetComponent<Renderer>();
		TextureController.TextureChoice actualTexture;
		actualTexture = (TextureController.TextureChoice) texture;
		TextureController tController = new TextureController(actualTexture);
		rend.material.mainTexture = tController.GetRandomWall();
	}

    [RPC]
    public void MakeIndestructable()
    {
		Renderer rend;
        FindInnerWall();
		rend = wallTransform.gameObject.GetComponent<Renderer>();
        rend.material.mainTexture = TextureController.GetWarningWallTexture();
        SetCanDestroy(false);
    }
    public void SetCanDestroy(bool val)
    {
        canDestroy = val;
    }
}
