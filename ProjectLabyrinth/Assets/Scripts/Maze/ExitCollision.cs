using UnityEngine;
using System.Collections;


public class ExitCollision : AbstractGUI {

    public GameObject loadResult;
    private bool hasReached;
	private PlayerCharacter player;
	public GameObject skPrefab;

    Rect win = new Rect(frameX + frameWidth/2, frameHeight / 2, 100, 100);

	
	void OnTriggerEnter (Collider other)
	{
		HitDetector hitDetector = (HitDetector)other.gameObject.GetComponent("HitDetector");
		if (hitDetector) {
			this.player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			this.hasReached = true;
			skPrefab.GetComponent<ScoreKeeper>().stats[0].win = true;
            Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
			player.data.win = true;
		}
	}
    void OnGUI()
    {
        if (hasReached)
        {
            if (GUI.Button(win, "Victory!"))
            {
                
            }
        }
    }

}
