using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ExitCollision : AbstractGUI {

    public GameObject victoryScreen;
    public GameObject failureScreen1;
    public GameObject failureScreen2;
    
	private PlayerCharacter player;
	//public GameObject skPrefab;

    bool hasGameEnded = false;

	
	void OnTriggerEnter (Collider other)
	{
        GameObject gameObj = other.gameObject;
        HitDetector hitDetector = gameObj.GetComponent<HitDetector>();
		NetworkView nView = gameObj.GetComponentInParent<NetworkView>();
		if (!hasGameEnded && hitDetector && nView && nView.isMine) {
            Instantiate(victoryScreen, new Vector3(0, 0, 0), Quaternion.identity);
            this.player = (PlayerCharacter)hitDetector.GetComponentInParent<PlayerCharacter>();
            
			//skPrefab.GetComponent<ScoreKeeper>().stats[0].win = true;
            //Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
            
			player.data.win = true;
		}
        else if(!hasGameEnded && hitDetector)
        {
            GameObject youLose = Instantiate(victoryScreen);
            foreach (Transform childTrans in youLose.transform)
            {
            	System.Random rnd = new Random();
            	int num = rnd.Next(0,2);
				if (num)
				{
					Instantiate(failureScreen1, new Vector3(0, 0, 0), Quaternion.identity);
				}
				else
				{
					Instantiate(failureScreen2, new Vector3(0, 0, 0), Quaternion.identity);
				}
            }
        }
	}
    

}
