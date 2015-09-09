using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// Author: Steven Lee
/// Component: Should be attached to a game panel controller.
/// Description: Holds the functions that are called when the create/join game buttons are clicked.
/// 
/// </summary>
public class GamePanelScript : MonoBehaviour {

    /* Drag the prefabs of the respective menus in the editor */
    public GameObject createGameMenu;
    public GameObject joinGameMenu;
    public AudioClip selectionClip;
    public AudioSource audioSource;
	private GameObject uimenu, playerLocal;
	private NetworkCreateGame tutorial;

	public void Start() {
		playerLocal = GameObject.Find("PlayerLocal");
		uimenu = GameObject.Find ("Book UI(Clone)");
		tutorial = GameObject.Find ("StartTutorial").GetComponent<NetworkCreateGame>();
	}

    /// <summary>
    /// Transitions the menu to the join game menu by turning this panel off and turning the other one on.
    /// </summary>
    public void JoinGame()
    {
		SoundController.PlaySound(audioSource, selectionClip);
        /* Turns this panel off */
        this.transform.parent.gameObject.SetActive(false);
		this.tutorial.active = false;

        /* Turn on the join game panel */
        joinGameMenu.SetActive(true);
    }
    /// <summary>
    /// Transitions the menu to the create game menu by turning this panel off and turning the other one on.
    /// </summary>
    public void CreateGame()
    {
		SoundController.PlaySound(audioSource, selectionClip);
        /* Turns this panel off */
        this.transform.parent.gameObject.SetActive(false);
		this.tutorial.active = false;

        /* Turn on the create game panel */
        createGameMenu.SetActive(true);
    }

	public void doTutorial()
	{
		SoundController.PlaySound (audioSource, selectionClip);

		this.tutorial.active = true;
		Destroy (uimenu);

		Network.InitializeServer (0, 1337, false);
		MenuWalk walkScript = playerLocal.GetComponent<MenuWalk>();
		walkScript.DefineLerp(walkScript.endMarker, walkScript.portal);

	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
}
