using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to prevent UI overlap during networked play.
/// </summary>
public class NetworkCNCamera : MonoBehaviour {

	/// <summary>
	/// The camera for rendering elements of the user interface.
	/// </summary>
	public Camera UICamera;

	/// <summary>
	/// The canvas for displaying elements of the user interface.
	/// </summary>
	public Canvas UICanvas;

	/// <summary>
	/// Object initialization method.
	/// </summary>
	void OnEnable() {
		if (!this.GetComponent<NetworkView>().isMine) {
			this.UICamera.gameObject.SetActive(false);
			this.UICanvas.gameObject.SetActive(false);
		}
	}
}
