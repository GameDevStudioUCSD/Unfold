using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public bool debug_On;
	public GameObject playerPrefab;
	public GameObject mazeGenerator;
	private MazeGeneratorController mapCreator;
	
	
	private void Start()
	{
		if (debug_On)
			Debug.Log("Tried to start match\nGetting MazeGeneratorController script");
		mapCreator = (MazeGeneratorController)mazeGenerator.GetComponent(typeof(MazeGeneratorController));
		if (debug_On)
			Debug.Log("Running Start() on MazeGeneratorController");
		mapCreator.Start();
		if (debug_On)
			Debug.Log("Running createWalls() on MazeGeneratorController");
		mapCreator.createWalls();
		if (debug_On)
			Debug.Log("Running SetSpawnLocations() on MazeGeneratorController");
		mapCreator.SetSpawnLocations();
		
		Instantiate (playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}
	
	
}
