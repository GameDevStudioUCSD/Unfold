using UnityEngine;

/// <summary>
/// Wrapper for Application.LoadLevel() to load the next scene
/// </summary>
public class LoadScene : MonoBehaviour {

	/// <summary>
	/// Load the scene specified by sceneName.
	/// </summary>
	/// <param name="sceneName">The scene name.</param>
	public void Load(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
