using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slips : MonoBehaviour {
    private SpriteController spriteController;
    private Image image;

    void Start()
    {
        image = this.GetComponent<Image>();
        spriteController = new SpriteController();
        image.sprite = spriteController.GetRandomSlip();
    }

}
