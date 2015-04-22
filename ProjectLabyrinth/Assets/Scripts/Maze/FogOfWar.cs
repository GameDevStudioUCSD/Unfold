using System.Collections;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

	public Light playerHasSeen;

	public PlayerCharacter player;

	public MazeInfo mi;

    void Start()
    {
        if(mi == null )
        {
            Debug.LogError("Error: MazeInfo not set");
        }
    }
	void Update() {
        if (!mi.exists)
        {
            return;
        }
        int initRow = (int)Mathf.Round(player.transform.position.x / mi.getWallSize());
        int initCol = (int)Mathf.Round(player.transform.position.z / mi.getWallSize());
        Square currWalls = mi.getWalls()[initRow, initCol];
        if (!currWalls.playerVisited)
        {
            Light.Instantiate(this.playerHasSeen, new Vector3(initRow * mi.wallSize, 5, initCol * mi.wallSize), Quaternion.identity);
            currWalls.playerVisited = true;
        }
        
		
	}
}
