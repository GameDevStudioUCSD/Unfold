using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoinGame : MonoBehaviour {

    private HostData[] gameList;
    private MasterServerManager masterServer;
    private Transform buttonParent;
    private int retry;
    private GameObject currentButtonObj;
    private Button currentButton;

    public GameObject buttonPrefab;
    public int connectionRetryAttempts = 2;
    public bool debugOn = true;
    public bool isSpoofingServer = false;

    // MonoBehaviour methods
	void Start() {
        if (debugOn)
        {
            Debug.Log("Starting JoinGame.cs");
        }
        if(buttonPrefab == null)
        {
            Debug.LogError("ButtonPrefab not set to a value");
            return;
        }
        retry = 0;
        buttonParent = this.GetComponent<Transform>();
        masterServer = new MasterServerManager();
        BuildButtons();
        
	}
    
    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
        if( retry < connectionRetryAttempts)
        {
            retry++;
            Debug.Log("Retrying... Attempt: " + retry);
            masterServer.RetryConnection();
        }
        else
        {
            Debug.Log("Retry limit reached. Aborting attempts to connect to server.");
            retry = 0;
        }
        
    }

    void OnApplicationQuit()
    {
        Network.Disconnect();
        MasterServer.UnregisterHost();
    }

    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Debug.Log("Could not connect to master server: " + info);
    }

    // Custom methods
    public void BuildButtons()
    {
        if(buttonParent == null)
        {
            return;
        }
        if (debugOn)
        {
            Debug.Log("Entering BuildButtons()");
        }
        ClearButtons();
        gameList = masterServer.GetHostData();
        if(debugOn)
        {
            Debug.Log("GameList: " + gameList.Length);
        }
        if (gameList.Length == 0)
        {
            CreateNewButton("No Games Found!\nClick here to refresh");
            currentButton.onClick.AddListener(() => BuildButtons());
            Debug.Log("No servers registered");
            if (isSpoofingServer)
            {
                SpoofServer();
            }
            return;
        }
        for (int i = 0; i < gameList.Length; i++)
        {
            if(debugOn)
            {
                Debug.Log("Creating button " + i);
            }
            CreateNewButton(gameList[i].gameName);
            currentButton.onClick.AddListener(() => masterServer.ConnectToGame(i));
        }
        
    }
    private void CreateNewButton(string text)
    {
        if(debugOn)
        {
            Debug.Log("Entering CreateNewButton()");
        }
        currentButtonObj = (GameObject)Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
        currentButtonObj.transform.SetParent(buttonParent);
        currentButton = currentButtonObj.GetComponent<Button>();
        currentButtonObj.GetComponentInChildren<Text>().text = text;
    }
    private void ClearButtons()
    {
        if(debugOn)
        {
            Debug.Log("Entering ClearButtons()");
        }
        if (buttonParent != null)
        {
            foreach (Transform child in buttonParent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private void SpoofServer()
    {
        Network.InitializeServer(1, 26500, !Network.HavePublicAddress());
        masterServer.RegisterServer("Spoof dsdfasdfsdfsdfsdhjServer3!", TextureController.TextureChoice.Corn);
    }

}
