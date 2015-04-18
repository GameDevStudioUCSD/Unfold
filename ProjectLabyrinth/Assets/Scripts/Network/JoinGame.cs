using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoinGame : MonoBehaviour {

    private HostData[] gameList;
    private MasterServerManager masterServer;
    private Transform buttonParent;
    public Object buttonPrefab;
	void Start () {
        buttonParent = this.GetComponent<Transform>();
        masterServer = new MasterServerManager();
        gameList = masterServer.GetHostData();
        GameObject currentButtonObj;
        Button currentButton;
        if(gameList == null)
        {
            return;
        }
        for ( int i = 0; i < gameList.Length; i++ )
        {
            currentButtonObj = (GameObject)Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
            currentButtonObj.transform.SetParent(buttonParent);
            currentButton = currentButtonObj.GetComponent<Button>();
            currentButton.onClick.AddListener(() => masterServer.ConnectToGame(i));
        }
	}
	
}
