using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour
{

    public bool matchOngoing = false;
    public User user;
    List<Meme> memes;

    int playerScore = 0;
    int enemyScore = 0;

    int difficulty;

    public GameObject nameObject;

    public void Start()
    {
        difficulty = user.difficulty;
        //only do these instructions if fighting against a normal player
        
        memes = GlobalGallery.GetGlobalGallery();
        
    }

    public MessageBehaviors.MessageType TakeTurn(GameObject matchMessage, Meme playerMeme)
    {


        int rand = Random.Range(0, memes.Count);
        Meme enemyMeme = memes[rand];

        //this line grabs the enemyMeme section of the matchMessage and changes it to the right image
        matchMessage.GetComponent<TextMessage>().opponentImg.GetComponent<Image>().sprite = enemyMeme.GetImageSprite();

        MessageBehaviors.MessageType type = MessageBehaviors.MessageType.Tie;

        if (playerMeme.GetMemeType() == Meme.MemeType.DeepFried)
        {
            switch (enemyMeme.GetMemeType())
            {
                case Meme.MemeType.Reaction:
                    playerScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.Text:
                    enemyScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundLoss;
                    break;
                default:
                    break;
            }
        } else if (playerMeme.GetMemeType() == Meme.MemeType.Reaction)
        {
            switch (enemyMeme.GetMemeType())
            {
                case Meme.MemeType.Text:
                    playerScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.DeepFried:
                    enemyScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundLoss;
                    break;
                default:
                    break;
            }
        } else if (playerMeme.GetMemeType() == Meme.MemeType.Text)
        {
            switch (enemyMeme.GetMemeType())
            {
                case Meme.MemeType.DeepFried:
                    playerScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.Reaction:
                    enemyScore++;
                    type = MessageBehaviors.MessageType.PlayerRoundLoss;
                    break;
                default:
                    break;
            }
        }

        matchMessage.GetComponent<TextMessage>().scoreText.GetComponent<Text>().text = enemyScore + ":" + playerScore;

        if (playerScore > difficulty)
        {
            matchOngoing = false;
            return MessageBehaviors.MessageType.PlayerWin;
        }

        return type;
    }
}
