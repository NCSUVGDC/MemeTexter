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
    public Gallery gallery;

    public void SendMessage()
    {
        if (textField.text != "")
        {
            //spawn the real text message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("TextMessage"));
            newMessage.transform.SetParent(matchMessages.transform, false);
            Canvas.ForceUpdateCanvases();
            matchMessages.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
            SoundManager.instance.PlaySend();

            //detect if the user intends to start a match
            newMessage.GetComponentInChildren<Text>().text = textField.text;
            if (textField.text.ToUpper() == "START!")
            {
                match.matchOngoing = true;
                SendEnemyMessage(MessageType.Engage);
                SoundManager.instance.PlayRandomBattle();
            } else
            {
                SendEnemyMessage(MessageType.Converse);
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
            newMessage.GetComponent<TextMessage>().playerTxt.GetComponent<Text>().text = userMeme.GetMemeType().ToString();
            //gallery.DisableMeme(userMeme.GetButton());
            Canvas.ForceUpdateCanvases();
            matchMessages.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
            MessageType matchStatus = match.TakeTurn(newMessage, userMeme, gallery);
            SendEnemyMessage(matchStatus);
            if (matchStatus == MessageType.PlayerWin)
            {
                SendEnemyReward(match.RewardCheck());
            }
        } else
        {
            //spawn the real user message
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("MemeMessage"));
            newMessage.transform.SetParent(matchMessages.transform, false);
            newMessage.GetComponent<MemeMessage>().imageObj.GetComponent<Image>().sprite = image.sprite;
            Canvas.ForceUpdateCanvases();
            matchMessages.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
            SoundManager.instance.PlaySend();
        }
        
    }

    public void SendEnemyMessage(MessageType type)
    {
        string enemyMessage = GlobalGallery.GetEnemyMessage(type);
        GameObject newMessage = (GameObject)Instantiate(Resources.Load("EnemyMessage"));
        newMessage.GetComponentInChildren<Text>().text = enemyMessage;
        newMessage.transform.SetParent(matchMessages.transform, false);
        SoundManager.instance.PlayReceive();

        if (type == MessageType.PlayerLoss)
        {
            newMessage = (GameObject)Instantiate(Resources.Load("EnemyMessage"));
            newMessage.GetComponentInChildren<Text>().text = "You Lost!";
            newMessage.transform.SetParent(matchMessages.transform, false);
        }

        Canvas.ForceUpdateCanvases();
        matchMessages.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
    }

    public void SendEnemyReward(bool earned)
    {
        if (earned)
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("EnemyMessage"));
            newMessage.GetComponentInChildren<Text>().text = "Here, take this meme";
            newMessage.transform.SetParent(matchMessages.transform, false);
        } else
        {
            GameObject newMessage = (GameObject)Instantiate(Resources.Load("EnemyMessage"));
            newMessage.GetComponentInChildren<Text>().text = "That was no fair! You're not getting a meme";
            newMessage.transform.SetParent(matchMessages.transform, false);
        }

        Canvas.ForceUpdateCanvases();
        matchMessages.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
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


