using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ExitCollision : MonoBehaviour {

    public GameObject victoryScreen;
    public GameObject failureScreen1;
    public GameObject failureScreen2;
    
	private PlayerCharacter player;
	//public GameObject skPrefab;

	public GlobalStats globalStats;

    bool hasGameEnded = false;

	
	void OnTriggerEnter (Collider other)
	{
		this.updateGlobalStats(other);
        GameObject gameObj = other.gameObject;
        PickupDetector hitDetector = gameObj.GetComponent<PickupDetector>();
		NetworkView nView = gameObj.GetComponentInParent<NetworkView>();
		if (!hasGameEnded && hitDetector && nView && nView.isMine) {
			this.performWin(hitDetector);
            
		}
        else if(!hasGameEnded && hitDetector)
        {
            GameObject youLose = Instantiate(victoryScreen);
			if (UnityEngine.Random.Range(0, 100) % 2 == 0) {
				Object.Instantiate(this.failureScreen1, new Vector3(), Quaternion.identity);
			} else {
				Object.Instantiate(this.failureScreen2, new Vector3(), Quaternion.identity);
			}
        }
	}

	public void updateGlobalStats(Collider other) {
		if (other.GetComponent<HitDetector> () != null) {
			if (!this.globalStats.gameObject.activeSelf) {
				this.globalStats.gameObject.SetActive(true);
				PlayerCharacter winner = (PlayerCharacter)other.GetComponentInParent<PlayerCharacter>();
				this.globalStats.collectData(winner);
			}
		}
	}
    
	protected virtual void performWin(PickupDetector hitDetector) {
		Instantiate(victoryScreen, new Vector3(0, 0, 0), Quaternion.identity);
		this.player = (PlayerCharacter)hitDetector.GetComponentInParent<PlayerCharacter>();
		
		//skPrefab.GetComponent<ScoreKeeper>().stats[0].win = true;
		//Instantiate(loadResult, new Vector3(0, 0, 0), Quaternion.identity);
		
		player.data.win = true;
		
	}
}