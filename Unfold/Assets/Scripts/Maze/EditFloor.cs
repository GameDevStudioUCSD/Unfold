using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditFloor : MonoBehaviour {

    public AudioClip mansionMusic;
    public AudioClip forestMusic;
    public AudioClip caveMusic;
    public AudioClip cornMusic;

    private Transform floorTransform;
    private TextureController.TextureChoice levelType;
    private bool isLevelSet = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        GameObject mazeRoot = GameObject.Find("Maze");
        if (mazeRoot != null)
        {
            floorTransform = GetComponent<Transform>();
            floorTransform.parent = mazeRoot.GetComponent<Transform>();
        }
    }

    void Update()
    {
        if(isLevelSet && !audioSource.isPlaying)
        {
            switch (levelType)
            {
                case TextureController.TextureChoice.Cave:
                    SoundController.PlayMusic(audioSource, caveMusic);
                    break;
                case TextureController.TextureChoice.Corn:
                    SoundController.PlayMusic(audioSource, cornMusic);
                    break;
                case TextureController.TextureChoice.Forest:
                    SoundController.PlayMusic(audioSource, forestMusic);
                    break;
                case TextureController.TextureChoice.Mansion:
                    SoundController.PlayMusic(audioSource, mansionMusic);
                    break;
            }
        }
    }

    [RPC]
    public void ModifyFloorSize(float wallSize, int rows, int cols)
    {
		this.transform.position -= new Vector3(wallSize / 2, 0, wallSize / 2);
        transform.localScale += new Vector3((wallSize * rows / 10) - 1, 0, (wallSize * cols / 10) - 1);
    }
    [RPC]
    public void UpdateTexture( int texture)
    {
        TextureController.TextureChoice actualTexture;
        actualTexture = (TextureController.TextureChoice) texture;
        this.levelType = actualTexture;
        this.isLevelSet = true;
        TextureController tController = new TextureController(actualTexture);
        GetComponent<Renderer>().material.mainTexture = tController.GetFloorTexture();
    }
}
