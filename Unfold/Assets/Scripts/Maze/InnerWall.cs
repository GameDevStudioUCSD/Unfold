using UnityEngine;
using System.Collections;

public class InnerWall : MonoBehaviour {

	/// <summary>
	/// Use this to fix the display problem
	/// </summary>
	/// <author>
	/// Anoxic
	/// </author>
	void Start() {
		// Get the mesh
		Mesh theMesh = this.transform.GetComponent<MeshFilter>().mesh;
		// Now store a local reference for the UVs
		Vector2[] theUVs = new Vector2[theMesh.uv.Length];
		theUVs = theMesh.uv;

		// set UV co-ordinates
		// Front
		theUVs[2] = new Vector2(0f, 1f);
		theUVs[3] = new Vector2(.1f, 1.0f);
		theUVs[0] = new Vector2(0f, 0f);
		theUVs[1] = new Vector2(.1f, 0f); 
		// Back
		theUVs[10] = new Vector2(0f, 1f);
		theUVs[11] = new Vector2(.1f, 1.0f);
		theUVs[6] = new Vector2(0f, 0f);
		theUVs[7] = new Vector2(.1f, 0f); 

		// Assign the mesh its new UVs
		theMesh.uv = theUVs;
	}

	// Update is called once per frame
	void Update() {

	}
}
