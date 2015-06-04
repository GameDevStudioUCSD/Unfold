using UnityEngine;
using System.Collections;

public enum Monster { Spanter, Robird, Inhabitant, Steward, Master }

[System.Serializable]
public class SpawnRate  {

	public bool debug_On = true;
	public string levelName = "Default";
    public int spanterRate;
    public int robirdRate;
    public int inhabitantRate;
    public int stewardRate;
    public int masterRate;
    

    private int[] probs;
    private int[] probWeightSummed;
    private int totalWeight;
    private bool hasSetup = false;

    public Monster SelectMonster()
    {
    	Setup ();
        Monster retVal = Monster.Robird;
        if (debug_On)
        	Debug.Log(levelName + "\tTotal Weight " + totalWeight);
        for (int i = 0; i < probs.Length; i++)
        {
            int rand = Random.Range(0, totalWeight);
            for (int j = 0; j < probs.Length; j++)
            {
                if (debug_On)
                	Debug.Log(rand);
                if (probWeightSummed[j] > rand)
                {
                    retVal = (Monster)j;
                    break;
                }
            }
        }
        return retVal;
    }
    private void Setup()
    {

        totalWeight = 0;
        probs = new int[] { spanterRate, robirdRate, inhabitantRate, stewardRate, masterRate };
        probWeightSummed = new int[probs.Length];
        CalculateTotalWeight();
        hasSetup = true;
    }
    override public string ToString() { 
       string retVal = "Level Name: " + levelName;
       retVal += "\t Spanter: " + spanterRate.ToString();
       retVal += "\t Robird: " + robirdRate.ToString ();
       retVal += "\t Stewart: " + stewardRate.ToString ();
       retVal += "\t Inhabitant: " + inhabitantRate.ToString ();
       retVal += "\t Master: " + masterRate.ToString() + "\n";
       return retVal;
    }

    private void CalculateTotalWeight()
    {
        if (debug_On)
        	Debug.Log("Running CalculateTotalWeight()");
        totalWeight = 0;
        for (int i = 0; i < probs.Length; i++)
        {
        	if (debug_On)
            	Debug.Log("Weight at i: " + i + " = " + probs[i]);
            totalWeight += probs[i];
            probWeightSummed[i] = probs[i];
            if (i > 0)
                probWeightSummed[i] += probWeightSummed[i - 1];
        }
    }
}
