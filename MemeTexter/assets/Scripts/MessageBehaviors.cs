using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehaviors : MonoBehaviour
{
    public InputField textField;
    public GameObject contentMessages;
    public GameObject enemyMessages;
    public GameObject matchMessages;
    public HandleScrolling handler;
    public Image image;
    public Match match;

    public void SendMessage()
    {
        if (textField.text != "")
        {
            //spawn the real text message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);
            handler.mostRecentMessage = newMessage.GetComponent<RectTransform>();

            //send fake messages to the enemey and match queues 
            GameObject newMessage2 = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage2.transform.SetParent(enemyMessages.transform, false);
            newMessage2.GetComponent<CanvasGroup>().alpha = 0;

            GameObject newMessage3 = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage3.transform.SetParent(matchMessages.transform, false);
            newMessage3.GetComponent<CanvasGroup>().alpha = 0;

            //detect if the user intends to start a match
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
            //spawn the real match message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MatchMessage"));
            newMessage.transform.SetParent(matchMessages.transform, false);
            handler.mostRecentMessage = newMessage.GetComponent<RectTransform>();

            //send invisible copies to the user and enemy queues
            GameObject newMessage2 = (GameObject)Instantiate(Resources.Load("MatchMessage"));
            newMessage2.transform.SetParent(contentMessages.transform, false);
            newMessage2.GetComponent<CanvasGroup>().alpha = 0;

            GameObject newMessage3 = (GameObject)Instantiate(Resources.Load("MatchMessage"));
            newMessage3.transform.SetParent(enemyMessages.transform, false);
            newMessage3.GetComponent<CanvasGroup>().alpha = 0;
        } else
        {
            //spawn the real user message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage.transform.SetParent(contentMessages.transform, false);
            newMessage.GetComponent<MemeMessage>().imageObj.GetComponent<Image>().sprite = image.sprite;
            handler.mostRecentMessage = newMessage.GetComponent<RectTransform>();

            //send invisible copies to the enemy and match queues
            GameObject newMessage2 = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage2.transform.SetParent(enemyMessages.transform, false);
            newMessage2.GetComponent<CanvasGroup>().alpha = 0;

            GameObject newMessage3 = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage3.transform.SetParent(matchMessages.transform, false);
            newMessage3.GetComponent<CanvasGroup>().alpha = 0;
        }
        
    }
}
