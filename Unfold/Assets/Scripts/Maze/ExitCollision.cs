using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ExitCollision : AbstractGUI {

    public GameObject loadResult;
	private PlayerCharacter player;
	//public GameObject skPrefab;

    bool hasGameEnded = false;

	
	void OnTriggerEnter (Collider other)
	{
        GameObject gameObj = other.gameObject;
        HitDetector hitDetector = gameObj.GetComponent<HitDetector>();
		NetworkView nView = gameObj.GetComponentInParent<NetworkView>();
		if (!hasGameEnded && hitDetector && nView && nView.isMine) {
            Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
            this.player = (PlayerCharacter)hitDetector.GetComponentInParent<PlayerCharacter>();
			//skPrefab.GetComponent<ScoreKeeper>().stats[0].win = true;
            //Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
			player.data.win = true;
		}
        else if(!hasGameEnded && hitDetector)
        {
            GameObject youLose = Instantiate(loadResult);
            foreach (Transform childTrans in youLose.transform)
            {
                Text text = childTrans.gameObject.GetComponent<Text>();
                if (text != null)
                {
                    text.fontSize = 32;
                    text.text = "You lose...";
                }
            }
        }
	}
    

}
