using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateGame : MonoBehaviour {

    private string gameName;
    private TextureController.TextureChoice gameType;
    private MasterServerManager masterServer;
    private bool isTryingToCreateGame = false;
    private GameObject playerLocal, uiMenu, gameTypeObj;
    public bool debugOn = false;
    public int maxPlayerCount = 4;
    public int portNumber = 26500;
    public Text gameNameInputField;
    public Transform gameTypeToggleGroup;
    public delegate void LoadWaitingRoom();

    private enum HostError
    {
        AlreadyMakingGame,
        PublicFeldsUndefined,
        GameNameInvalid,
        GameTypeIssue,
        ServerFailedToInstantiate,
        FailedToConnectToMasterServer
    };
    public void TryCreateGame()
    {
        if(isTryingToCreateGame)
        {
            HandleError(HostError.AlreadyMakingGame);
            return;
        }
        isTryingToCreateGame = true;
        if(!IsPublicFieldsDefined())
        {
            HandleError(HostError.PublicFeldsUndefined);
            return;
        }
        if(!IsGameNameValid())
        {
            HandleError(HostError.GameNameInvalid);
            return;
        }
        if(!DetermineGameType())
        {
            HandleError(HostError.GameTypeIssue);
            return;
        }
        if(!StartServer())
        {
            HandleError(HostError.ServerFailedToInstantiate);
            return;
        }
    }
    private bool IsPublicFieldsDefined()
    {
        bool retVal = true;
        if(maxPlayerCount < 1)
        {
            retVal = false;
            Debug.LogError("Player Count set too low");
        }
        if(portNumber < 1)
        {
            retVal = false;
            Debug.LogError("Port Number set too low");
        }
        if(gameNameInputField == null)
        {
            retVal = false;
            Debug.LogError("Please define a Game Name Input Field");
        }
        if(gameTypeToggleGroup == null)
        {
            retVal = false;
            Debug.LogError("Please define a Game Type Toggle Group");
        }
        return retVal;
    }
    /// <summary>
    /// This method ensures the game name is valid and will register on the 
    /// master server. It will also set the gameName field if the name is valid 
    /// </summary>
    /// <returns>Returns true if the name is valid; Else false</returns>
    private bool IsGameNameValid()
    {
        bool retVal = false;
        if( gameNameInputField.text.Length != 0)
        {
            retVal = true;
            gameName = gameNameInputField.text;
        }
        else
        {
            Debug.LogWarning("Game name not valid!");
        }
        return retVal;
    }
    /// <summary>
    /// This method examines each toggle within the toggle group to determine 
    /// which game type the host has chosen.
    /// </summary>
    /// <returns>True if successful, False on failure</returns>
    private bool DetermineGameType()
    {
        bool retVal = true;
        GameObject toggleGameObj;
        string levelName = "";
        Toggle currentToggle;
        foreach(Transform child in gameTypeToggleGroup)
        {
            toggleGameObj = child.gameObject;
            currentToggle = toggleGameObj.GetComponent<Toggle>();
            if(currentToggle.isOn)
            {
                levelName = toggleGameObj.name;
                break;
            }
        }
        switch (levelName)
        {
            case "Forest":
                gameType = TextureController.TextureChoice.Forest;
                break;
            case "Cave":
                gameType = TextureController.TextureChoice.Cave;
                break;
            case "Corn":
                gameType = TextureController.TextureChoice.Corn;
                break;
            case "Mansion":
                gameType = TextureController.TextureChoice.Mansion;
                break;
            default:
                retVal = false;
                Debug.LogWarning("Something went wrong while determining Game type");
                break;

        }
        return retVal;
    }
    private bool StartServer()
    {
        bool retVal = true;
        int mpc = maxPlayerCount;
        int pn = portNumber;
        NetworkConnectionError error;
        error = Network.InitializeServer(mpc, pn, !Network.HavePublicAddress());
        if(error != NetworkConnectionError.NoError)
        {
            Debug.LogError("Error while instantiating server: " + error);
            retVal = false;
        }
        return retVal;
    }
    private bool RegisterOnMasterServer()
    {
        bool retVal = true;
        masterServer = new MasterServerManager();
        masterServer.RegisterServer(gameName, gameType);
        return retVal;
    }
    private void HandleError(HostError error)
    {
        if(error != HostError.AlreadyMakingGame)
        {
            isTryingToCreateGame = false;
        }
    }
    private void EnterGame()
    {
        Destroy(uiMenu);
        MenuWalk walkScript = playerLocal.GetComponent<MenuWalk>();
        walkScript.DefineLerp(walkScript.endMarker, walkScript.portal);
        gameTypeObj.GetComponent<MazeType>().SetGameType(gameType);
    }
    void Start()
    {
        playerLocal = GameObject.Find("PlayerLocal");
        uiMenu = GameObject.Find("Prototype Book UI(Clone)");
        gameTypeObj = GameObject.Find("GameType");
    }
    void OnServerInitialized()
    {
        if (debugOn)
        {
            Debug.Log("Server Initialized");
        }
        RegisterOnMasterServer(); 
    }
    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Debug.LogWarning("Could not connect to master server: " + info);
        HandleError(HostError.FailedToConnectToMasterServer);
    }
    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (debugOn)
        {
            Debug.Log("Master Server: " + msEvent);
        }
        if( msEvent == MasterServerEvent.RegistrationSucceeded )
        {
            EnterGame();
        }
    }

}
