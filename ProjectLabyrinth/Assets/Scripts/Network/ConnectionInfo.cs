using UnityEngine;
using System.Collections;

public class ConnectionInfo : MonoBehaviour {

    public string[] ipAddress;
    public int portNumber = 26500;
	// Use this for initialization
	void Start () {
        GameObject gameObj = this.GetComponent<Transform>().gameObject;
        this.name = "ConnectionInfo";
        GameObject.DontDestroyOnLoad(gameObj);
	}
    public void setInfo(string[] ip, int port)
    {
        ipAddress = ip;
        portNumber = port;
    }
}
