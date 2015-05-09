using UnityEngine;
using System.Collections;

public class EditWalls : MonoBehaviour {
	public bool debugOn = false;
	//public GameObject container;
	private Transform wallTransform;
	public int health = 3;
	void Start() {
		GameObject mazeRoot = GameObject.Find("Maze");
		if (mazeRoot != null) {
			wallTransform = GetComponent<Transform>();
			wallTransform.parent = mazeRoot.GetComponent<Transform>();
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
	public void DestroyWall() {
		if (--health <= 0) {
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
}
