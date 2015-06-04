﻿using UnityEngine;
using System.Collections;

public class PickupDropper: MonoBehaviour {
	
	public GameObject[] pickupList;
	public uint[] weightedProbability; //Should be one index longer than pickupList
	public int maxDrop;
	public bool debug_On = false;
	private System.Random rnd;
	
	private string probError = ": Pickup List Array Length does not have assigned probabilities";
	// Use this for initialization
	public void Start()
	{
		rnd = new System.Random(System.Guid.NewGuid().GetHashCode());   	
	}

	public void dropItem(float x, float z) {
		int numberOfDrops = rnd.Next(maxDrop + 1);
		if (debug_On)
			numberOfDrops = 100;
		for(int i = 0; i < numberOfDrops; i++)
		{
			int rand = rnd.Next(0, pickupList.Length + 1) - 1;
			
			if (rand == -1)
			{
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
		return (weightedProbability[i] > weightedProbability[random])? pickupList[i]:pickupList[random];
    }
}
