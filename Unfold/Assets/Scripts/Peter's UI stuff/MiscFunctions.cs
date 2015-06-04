using UnityEngine;

/// <summary>
/// Class to hold miscellanous functions and wrappers that do not
/// belong to any particular prefab or object.
/// </summary>
public class MiscFunctions : MonoBehaviour {

	public Object endScreenUIPrefab;
	
	/// <summary>
	/// Load the scene specified by sceneName.
	/// </summary>
	/// <param name="sceneName">The scene name.</param>
	public void Load(string sceneName) {
		Application.LoadLevel(sceneName);
	}
    public void DeleteGameObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj)
        {
            GameObject.Destroy(obj);
        }
    }
}
