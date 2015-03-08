using UnityEngine;
using System.Collections;


public class ExitCollision : AbstractGUI {

	//private GameObject player;
    public GameObject loadResult;
	private Collider collider;
    private bool hasReached;
	private PlayerCharacter player;

    static float frameX = Screen.width * (1 - wRatio) / 2;

    Rect win = new Rect(frameX + frameWidth/2, frameHeight / 2, 100, 100);

	
	void OnTriggerEnter (Collider other)
	{
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			hasReached = true;
		}
	}
    void OnGUI()
    {
        if (hasReached)
        {
            if (GUI.Button(win, "Win the Game!"))
            {
                Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

}
