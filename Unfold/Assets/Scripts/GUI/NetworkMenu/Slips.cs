using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slips : MonoBehaviour {

    public bool debugOn = false;
    private SpriteController spriteController;
    private Image image;

    void Start()
    {
        image = this.GetComponent<Image>();
        spriteController = new SpriteController();
        image.sprite = spriteController.GetRandomSlip();
        if (debugOn)
            Debug.Log(image);
    }

}
