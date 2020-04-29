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
    public Gallery gallery;

    public void Start()
    {
        difficulty = user.difficulty;
        //only do these instructions if fighting against a normal player
        
        memes = GlobalGallery.GetGlobalGallery();
        
    }

    public MessageType TakeTurn(GameObject matchMessage, Meme playerMeme, Gallery gallery)
    {
        int rand = Random.Range(0, memes.Count);
        Meme enemyMeme = memes[rand];

        //this line grabs the enemyMeme section of the matchMessage and changes it to the right image
        matchMessage.GetComponent<TextMessage>().opponentImg.GetComponent<Image>().sprite = enemyMeme.GetImageSprite();
        matchMessage.GetComponent<TextMessage>().opponentTxt.GetComponent<Text>().text = enemyMeme.GetMemeType().ToString();

        MessageType type = MessageType.Tie;

        if (playerMeme.GetMemeType() == Meme.MemeType.DeepFried)
        {
            switch (enemyMeme.GetMemeType())
            {
                case Meme.MemeType.Reaction:
                    playerScore++;
                    type = MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.Text:
                    enemyScore++;
                    type = MessageType.PlayerRoundLoss;
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
                    type = MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.DeepFried:
                    enemyScore++;
                    type = MessageType.PlayerRoundLoss;
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
                    type = MessageType.PlayerRoundWin;
                    break;
                case Meme.MemeType.Reaction:
                    enemyScore++;
                    type = MessageType.PlayerRoundLoss;
                    break;
                default:
                    break;
            }
        }

        matchMessage.GetComponent<TextMessage>().scoreText.GetComponent<Text>().text = enemyScore + ":" + playerScore;

        if (playerScore >= difficulty)
        {
            matchOngoing = false;
            SoundManager.instance.PlayIdle();
            playerScore = 0;
            enemyScore = 0;
            //gallery.EnableMemes();
            return MessageType.PlayerWin;
        }

        if (enemyScore >= difficulty)
        {
            matchOngoing = false;
            SoundManager.instance.PlayIdle();
            playerScore = 0;
            enemyScore = 0;
            //gallery.EnableMemes();
            return MessageType.PlayerLoss;
        }

        
        if (gallery.disabledMemes.Count >= GlobalGallery.GetPlayerGallery().Count)
        {
            matchOngoing = false;
            SoundManager.instance.PlayIdle();
            playerScore = 0;
            enemyScore = 0;
            //gallery.EnableMemes();
            return MessageType.PlayerLoss;
        }

        return type;
    }

    public bool RewardCheck()
    {
        if (difficulty > (float)GlobalGallery.GetPlayerGallery().Count / 5)
        {
            return true;
        }
        return false;
    }

}
