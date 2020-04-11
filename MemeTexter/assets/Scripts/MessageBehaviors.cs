using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehaviors : MonoBehaviour
{
    public InputField textField;
    public GameObject contentMessages;
    public Image image;
    public Match match;

    public void SendMessage()
    {
        if (textField.text != "")
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);

            newMessage.GetComponentInChildren<Text>().text = textField.text;
            if (textField.text.ToUpper() == "START!")
            {
                match.matchOngoing = true;
            }
            textField.text = "";
        }
    }

    public void SendMemeMessage()
    {
        if (match.matchOngoing)
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MatchMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);
        } else
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);
            newMessage.GetComponent<MemeMessage>().imageObj.GetComponent<Image>().sprite = image.sprite;
        }
        
    }
}
