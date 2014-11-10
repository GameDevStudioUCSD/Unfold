using UnityEngine;
using System.Collections;

public class Wave_Generator : MonoBehaviour {
    public GameObject wave;
    public int size = 20;
	// Use this for initialization
	void Start () {
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {

                Instantiate(wave, new Vector3(x, 
                    Mathf.Sin(Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(z,2)))
                    , z), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
