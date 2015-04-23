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
    private int numberOfConnectedPlayers;
    private GameObject currentButtonObj;
    private Button currentButton;
    private TextureController.TextureChoice gameType;
    private SpriteController spriteController;
    private GameObject playerLocal, uiMenu;

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
        spriteController = new SpriteController();
        retry = 0;
        buttonParent = this.GetComponent<Transform>();
        masterServer = new MasterServerManager();
        playerLocal = GameObject.Find("PlayerLocal");
        uiMenu = GameObject.Find("Prototype Book UI(Clone)");
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

    void OnConnectedToServer()
    {
        EnterGame();
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
            gameType = TextureController.TextureChoice.Mansion;
            numberOfConnectedPlayers = 0;
            CreateNewButton("No Games Found!\nClick here to refresh", gameType);
            currentButton.onClick.AddListener(() => BuildButtons());
            if (isSpoofingServer)
            {
                SpoofServer();
            }
            return;
        }
        int gameTypeInt;
        for (int i = 0; i < gameList.Length; i++)
        {
            if(debugOn)
            {
                Debug.Log("Creating button " + i);
            }
            gameTypeInt = int.Parse(gameList[i].comment);
            gameType = (TextureController.TextureChoice)gameTypeInt;
            numberOfConnectedPlayers = gameList[i].connectedPlayers;
            CreateNewButton(gameList[i].gameName, gameType);
            currentButton.onClick.AddListener(() => masterServer.ConnectToGame(i));
        }
        
    }
    private void CreateNewButton(string text, TextureController.TextureChoice tc)
    {
        if(debugOn)
        {
            Debug.Log("Entering CreateNewButton()");
        }
        Sprite playerIcon;
        int index = 0;
        currentButtonObj = (GameObject)Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
        currentButtonObj.transform.SetParent(buttonParent);
        currentButton = currentButtonObj.GetComponent<Button>();
        currentButtonObj.GetComponentInChildren<Text>().text = text;
        currentButtonObj.GetComponent<Image>().color = DetermineButtonColor();
        foreach(Transform child in currentButtonObj.transform)
        {
            if(child.gameObject.name == "Text")
            {
                continue;
            }
            else
            {
                foreach(Transform player in child)
                {
                    if (index < numberOfConnectedPlayers)
                    {
                        playerIcon = spriteController.GetRandomDoodle();
                        player.gameObject.GetComponent<Image>().sprite = playerIcon;
                    }
                    else
                    {
                        player.gameObject.SetActive(false);
                    }
                    index++;
                }
            }
            
        }
    }
    private Color DetermineButtonColor()
    {
        Color retVal = Color.red;
        switch (gameType)
        {
            case TextureController.TextureChoice.Mansion:
                retVal = Color.white;
                break;
            case TextureController.TextureChoice.Corn:
                retVal = Color.green;
                break;
            case TextureController.TextureChoice.Cave:
                retVal = Color.grey;
                break;
            case TextureController.TextureChoice.Forest:
                // Set the color to brown
                retVal = new Color( .278f, .161f, .043f);
                break;
        }
        return retVal;
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

    private void EnterGame()
    {
        Destroy(uiMenu);
        MenuWalk walkScript = playerLocal.GetComponent<MenuWalk>();
        walkScript.DefineLerp(walkScript.endMarker, walkScript.portal);
    }

}
