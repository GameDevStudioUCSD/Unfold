using UnityEngine;
using System.Collections;

public class PickupDropper : MonoBehaviour {
	
	public GameObject[] pickupList;
    public int[] probs;
    public int[] probWeightSummed;
    public int probabilityForNothing;
	public bool debug_On = false;

    private string probError = ": Pickup List Array Length does not have assigned probabilities";
    private int totalWeight;
	// Use this for initialization
    public void Start()
    {
        
    }
	public void dropItem(float x, float z) {
        int numberOfDrops = 1;
        if (debug_On)
            numberOfDrops = 100;
        for(int i = 0; i < numberOfDrops; i++)
        {
            System.Random rnd = new System.Random();
            // Check if the pickups and probabilities match in size
            if (pickupList.Length != probs.Length)
            {
                Debug.LogError(this + probError);
            }
            else
            {
                CalculateTotalWeight();
                int rand = rnd.Next(0, totalWeight);
                GameObject itemToSpawn = FindSpawnItem(rand);
                if (debug_On)
                    Debug.Log("Dropped an Item of type: " + itemToSpawn);
                if (itemToSpawn != null)
                    Network.Instantiate(itemToSpawn, new Vector3(x, 1, z), Quaternion.identity, 0);
            }
        }
		
		
	}
    private void CalculateTotalWeight()
    {
        totalWeight = probabilityForNothing;
        probWeightSummed = new int[probs.Length];
        for( int i = 0; i < probs.Length; i++)
        {
            totalWeight += probs[i];
            probWeightSummed[i] = probs[i];
            if (i > 0)
                probWeightSummed[i] += probWeightSummed[i - 1];
        }
    }
    private GameObject FindSpawnItem( int random )
    {
        GameObject returnVal = null;
        if (debug_On)
            Debug.Log("Random Value: " + random + "Total Weight: " + totalWeight);
        for (int i = 0; i < pickupList.Length; i++)
        {
            if (probWeightSummed[i] > random)
                return pickupList[i];
        }
        //Debug.LogError(this + "Failed to pick an item to spawn!");
        return returnVal;
    }
}
