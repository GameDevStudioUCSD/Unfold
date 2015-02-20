using UnityEngine;
using System.Collections;

public class LobbyNetwork : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        SpawnPlayer();
	}

    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity, 0);
    }
}
