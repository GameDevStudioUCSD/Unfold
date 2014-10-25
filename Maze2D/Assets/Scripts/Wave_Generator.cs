using UnityEngine;
using System.Collections;

public class Wave_Generator : MonoBehaviour {
    public GameObject wave;
	// Use this for initialization
	void Start () {
        for (int z = 0; z < 100; z++)
        {
            for (int x = 0; x < 100; x++)
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
