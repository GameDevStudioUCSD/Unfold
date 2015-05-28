using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleButton : MonoBehaviour {

/// <summary>
/// This script adds a toggle object to a toggle group script at runtime
/// </summary>
	void Start () {
        Toggle toggle = this.GetComponent<Toggle>();
        ToggleGroup group = toggle.group;
        if(group == null)
        {
            Debug.LogError(toggle + " has no toggle group defined!");
            return;
        }
        group.RegisterToggle(toggle);
	}
	
}
