using UnityEngine;
using System.Collections;

public class PickupDropper: MonoBehaviour {
	
	public GameObject[] pickupList;
	public uint[] weightedProbability; //Should be one index longer than pickupList and last element should be 0
	public int maxDrop;
	public bool debug_On = false;
	private System.Random rnd;
	public int spawnOneItem;
	
	private string probError = ": Pickup List Array Length does not have assigned probabilities";
	// Use this for initialization
	public void Start()
	{
		   	
	}

	public void dropItem(float x, float z) {
		rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
		int numberOfDrops = rnd.Next(maxDrop);
		if (debug_On)
			numberOfDrops = 100;

		if (spawnOneItem != -1) {
			GameObject itemToSpawn = FindSpawnItem (spawnOneItem);
			Network.Instantiate (itemToSpawn, new Vector3 (x, 1, z), Quaternion.identity, 0);
		} 
		else {
			for (int i = 0; i < numberOfDrops; i++) {
				int rand = rnd.Next (0, pickupList.Length + 1) - 1;
				
				if (rand == -1) {
					break;
				} else {
					GameObject itemToSpawn = FindSpawnItem (rand);
					if (debug_On)
						Debug.Log ("Dropped an Item of type: " + itemToSpawn);
					if (itemToSpawn != null)
						Network.Instantiate (itemToSpawn, new Vector3 (x, 1, z), Quaternion.identity, 0);
				}
			}
		}
		
		
	}

	private GameObject FindSpawnItem( int i )
	{
		int random = rnd.Next(weightedProbability.Length);
		return (weightedProbability[i] > weightedProbability[random])? pickupList[i]:pickupList[random];
    }
}
