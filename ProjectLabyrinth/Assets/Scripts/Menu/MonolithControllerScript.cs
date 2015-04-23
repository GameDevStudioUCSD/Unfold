using UnityEngine;
using System.Collections;

public class MonolithControllerScript : MonoBehaviour {

    public Object canvasUIPrefab;
    public GameObject title;

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player;
        player = other.gameObject.GetComponent<PlayerCharacter>();
        player.animator.SetBool("Walking", false);
        Instantiate(canvasUIPrefab);
        Destroy(this.GetComponent<Renderer>());
        Destroy(title);
    }
}
