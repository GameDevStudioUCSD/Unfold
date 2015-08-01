using UnityEngine;
using System.Collections;
using System.Linq;

public class PickupDropper: MonoBehaviour {
	
	public GameObject[] pickupList;
	public uint[] weightedProbability; //Should be one index longer than pickupList and last element should be 0
	public int maxDrop;
	public bool debug_On = false;
	private System.Random rnd;
	public int spawnOneItem;
	public bool tutorialMode = false;
	
	private string probError = ": Pickup List Array Length does not have assigned probabilities";
	// Use this for initialization
	public void Start()
	{
		   	
	}

	public void dropItem(float x, float z) {
		rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
		int numberOfDrops = rnd.Next(maxDrop + 1);
		if (debug_On)
			numberOfDrops = 100;
		if (tutorialMode)
			numberOfDrops = 1;
		for(int i = 0; i < numberOfDrops; i++)
		{
			Debug.Log ("Dropping stuff");
			int rand = rnd.Next(0, pickupList.Length + 1) - 1;
			
			if (rand == -1)
			{
				Debug.Log("Break Loop");
				break;
			}
			else
			{
				GameObject itemToSpawn = FindSpawnItem(rand);
				if (debug_On)
					Debug.Log("Dropped an Item of type: " + itemToSpawn);
				if (itemToSpawn != null)
					Network.Instantiate(itemToSpawn, new Vector3(x, 1, z), Quaternion.identity, 0);

			}
		}
	}

	private GameObject FindSpawnItem( int i )
	{
		int random = rnd.Next(weightedProbability.Length);
		int maxIndex = weightedProbability.ToList().IndexOf(weightedProbability.Max());
		return (weightedProbability[i] > random)? pickupList[i]:pickupList[maxIndex];
    }
}
