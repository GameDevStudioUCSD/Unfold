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


    /// <summary>
    /// Transitions the menu to the join game menu by turning this panel off and turning the other one on.
    /// </summary>
    public void JoinGame()
    {
        /* Turns this panel off */
        this.transform.parent.gameObject.SetActive(false);

        /* Turn on the join game panel */
        joinGameMenu.SetActive(true);
    }
    /// <summary>
    /// Transitions the menu to the create game menu by turning this panel off and turning the other one on.
    /// </summary>
    public void CreateGame()
    {
        /* Turns this panel off */
        this.transform.parent.gameObject.SetActive(false);

        /* Turn on the create game panel */
        createGameMenu.SetActive(true);
    }
}
