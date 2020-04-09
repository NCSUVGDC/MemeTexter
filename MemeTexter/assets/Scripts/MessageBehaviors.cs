using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehaviors : MonoBehaviour
{
    public InputField textField;
    public GameObject contentMessages;

    public void SendMessage()
    {
        if (textField.text != "")
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);

            newMessage.GetComponentInChildren<Text>().text = textField.text;
            textField.text = "";
        }
    }
}
