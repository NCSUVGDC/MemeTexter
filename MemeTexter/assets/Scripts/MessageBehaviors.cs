using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBehaviors : MonoBehaviour
{
    public InputField textField;
    public GameObject matchMessages;

    public Image image;
    public Meme userMeme;
    public Match match;

    public void SendMessage()
    {
        if (textField.text != "")
        {
            //spawn the real text message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage.transform.SetParent(matchMessages.transform, false);

            //detect if the user intends to start a match
            newMessage.GetComponentInChildren<Text>().text = textField.text;
            if (textField.text.ToUpper() == "START!")
            {
                match.matchOngoing = true;
                SendEnemyMessage(MessageType.Engage);
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
            newMessage.GetComponent<TextMessage>().playerImg.GetComponent<Image>().sprite = image.sprite;
            MessageType matchStatus = match.TakeTurn(newMessage, userMeme);
            SendEnemyMessage(matchStatus);

        } else
        {
            //spawn the real user message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage.transform.SetParent(matchMessages.transform, false);
            newMessage.GetComponent<MemeMessage>().imageObj.GetComponent<Image>().sprite = image.sprite;

        }
        
    }

    public void SendEnemyMessage(MessageType type)
    {
        string enemyMessage = GlobalGallery.GetEnemyMessage(type);
        GameObject newMessage = (GameObject)Instantiate(Resources.Load("EnemyMessage"));
        newMessage.GetComponentInChildren<Text>().text = enemyMessage;
        newMessage.transform.SetParent(matchMessages.transform, false);

    }


    public enum MessageType
    {
        Engage,
        PlayerWin,
        PlayerLoss,
        PlayerRoundWin,
        PlayerRoundLoss,
        Tie,
        Converse
    }
}


