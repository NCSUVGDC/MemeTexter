using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehaviors : MonoBehaviour
{
    public static GameObject canvas;
    public static InputField textField;
    public static GameObject contentMessages;

    private void Awake()
    {

        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }

        if (textField == null)
        {
            textField = GameObject.Find("TextField").GetComponent<InputField>();
        }

        if (contentMessages == null)
        {
            contentMessages = GameObject.Find("ContentMessages");
        }

    }

    public void SendMessage()
    {
        GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
        newMessage.transform.SetParent(contentMessages.transform, false);

        newMessage.GetComponentInChildren<Text>().text = textField.text;
        textField.text = "";
    }
}
