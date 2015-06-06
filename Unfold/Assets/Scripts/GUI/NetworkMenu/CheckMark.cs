using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This script will attach to the checkmark UI Objects inside the Create Game
/// menu. It will select a random doodle dude to set as the checkmark icon each
/// time the player selects a level type.
/// </summary>
public class CheckMark : MonoBehaviour {
    private SpriteController spriteController;
    private Image image;

	void Start () {
        image = this.GetComponent<Image>();
        spriteController = new SpriteController();
        image.sprite = spriteController.GetRandomDoodle();
	}
    public void UpdateDoodle()
    {
        image.sprite = spriteController.GetRandomDoodle();
    }

	
}
