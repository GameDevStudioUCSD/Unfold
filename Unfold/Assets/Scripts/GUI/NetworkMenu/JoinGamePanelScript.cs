using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// Author: Steven Lee
/// Component: Should be attached to a join game panel controller.
/// Description: Contains all the on click functions.
/// 
/// </summary>
public class JoinGamePanelScript : MonoBehaviour {

    /* Drag the prefabs for the respective menus in the editor */
    public GameObject gameMenu;
    public AudioClip selectionClip;
    public AudioSource audioSource;

    /// <summary>
    /// Goes back to the previous menu by turning this menu off and turning the other one on.
    /// </summary>
    public void GoBack()
    {
		SoundController.PlaySound(audioSource, selectionClip);
        /* Turn off this panel */
        this.transform.parent.gameObject.SetActive(false);        

        /* Go back to the menu panel */
        gameMenu.SetActive(true);
    }

}
