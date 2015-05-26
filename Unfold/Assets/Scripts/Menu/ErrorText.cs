using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorText : MonoBehaviour {
    public Text errorText;
    public void Start()
    {
        RectTransform trans = this.GetComponent<RectTransform>();
        GameObject errors = GameObject.Find("Errors");
        if(errors != null)
        {
            trans.SetParent(errors.GetComponent<Transform>());
            trans.anchoredPosition = Vector2.zero;
        }
    }
    public void SetErrorText(string text)
    {
        if(errorText == null )
        {
            Debug.LogError("Error! Error Text set to null");
            return;
        }
        errorText.text = text;
    }
}
