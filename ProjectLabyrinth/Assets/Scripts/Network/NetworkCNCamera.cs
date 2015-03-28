using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// Used to prevent UI overlap during networked play.
/// 
/// </summary>
public class NetworkCNCamera : MonoBehaviour {

	void Start () {
		if (!GetComponent<NetworkView>().isMine)
		{
            /* Get the transform of the parent object */
            Transform parentObject = transform.parent;

            /* Turn off the UI children in the player prefab if they are not yours */
            parentObject.FindChild("UICanvas").gameObject.SetActive(false);
            parentObject.FindChild("UICamera").gameObject.SetActive(false);
		}
	}
	
}
