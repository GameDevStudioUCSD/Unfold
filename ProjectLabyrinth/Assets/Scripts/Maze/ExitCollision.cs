using UnityEngine;
using System.Collections;


public class ExitCollision : AbstractGUI {

    public GameObject loadResult;
	private PlayerCharacter player;
	//public GameObject skPrefab;

    Rect win = new Rect(frameX + frameWidth/2, frameHeight / 2, 100, 100);

	
	void OnTriggerEnter (Collider other)
	{
        GameObject gameObj = other.gameObject;
        HitDetector hitDetector = gameObj.GetComponent<HitDetector>();
		NetworkView nView = gameObj.GetComponentInParent<NetworkView>();
		if (hitDetector && nView && nView.isMine) {
            Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
			this.player = (PlayerCharacter) hitDetector.GetComponentInParent<PlayerCharacter>();
			//skPrefab.GetComponent<ScoreKeeper>().stats[0].win = true;
            //Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);

			player.data.win = true;
		}
	}
    

}
