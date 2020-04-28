using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGallery : MonoBehaviour
{
    private static GlobalGallery singleton;

    // All of the player's currently owned memes
    private static List<Meme> playerGallery = new List<Meme>();

    // All of the player's currently unowned memes
    private static List<Meme> playerUnowned = new List<Meme>();

    // All of the memes in MemeData.txt
    private static List<Meme> globalGallery = new List<Meme>();

    /// USED IF WE WANT THE MEME LORD TO HAVE SPECIAL MEMES
    // All of the memes in MemeLordData.txt
    private static List<Meme> memeLordGallery = new List<Meme>();

    /// USED IF WE WANT THE MEME LORD TO HAVE SPECIAL MEMES
    // private static void FillMemeLordGallery()
    // public static List<Meme> GetMemeLordGallery()


    //all of the available usernames for new opponents
    private static List<string> userNames = new List<string>();

    //set up lists for dialog
    private static List<string> engageTexts = new List<string>();
    private static List<string> playerWinTexts = new List<string>();
    private static List<string> playerLossTexts = new List<string>();
    private static List<string> playerRoundWinTexts = new List<string>();
    private static List<string> playerRoundLossTexts = new List<string>();
    private static List<string> tieTexts = new List<string>();
    private static List<string> conversationTexts = new List<string>();

    //set up lists for memeLord
    private static List<string> engageTextsLord = new List<string>();
    private static List<string> playerWinTextsLord = new List<string>();
    private static List<string> playerLossTextsLord = new List<string>();
    private static List<string> playerRoundWinTextsLord = new List<string>();
    private static List<string> playerRoundLossTextsLord = new List<string>();
    private static List<string> tieTextsLord = new List<string>();
    private static List<string> conversationTextsLord = new List<string>();

    //set up lists for Mom
    private static List<string> engageTextsMom = new List<string>();
    private static List<string> playerWinTextsMom = new List<string>();
    private static List<string> playerLossTextsMom = new List<string>();
    private static List<string> playerRoundWinTextsMom = new List<string>();
    private static List<string> playerRoundLossTextsMom = new List<string>();
    private static List<string> tieTextsMom = new List<string>();
    private static List<string> conversationTextsMom = new List<string>();

    //set up lists for Rival
    private static List<string> engageTextsRival = new List<string>();
    private static List<string> playerWinTextsRival = new List<string>();
    private static List<string> playerLossTextsRival = new List<string>();
    private static List<string> playerRoundWinTextsRival = new List<string>();
    private static List<string> playerRoundLossTextsRival = new List<string>();
    private static List<string> tieTextsRival = new List<string>();
    private static List<string> conversationTextsRival = new List<string>();



    void Awake()
    {
        // Singleton stuff
        if (singleton != null)
            Destroy(singleton);
        else
            singleton = this;

        DontDestroyOnLoad(this);

        // Fill the global gallery if it's empty
        if (globalGallery.Count == 0)
        {
            FillGlobalGallery();
        }

        if (engageTexts.Count + playerWinTexts.Count + playerLossTexts.Count 
            + playerRoundWinTexts.Count + playerRoundLossTexts.Count == 0)
        {
            FillGlobalDialogs();
        }

        // Fill the list of memes the player does not own if both the
        // player's gallery and their list of unowned memes is empty
        if (playerGallery.Count == 0 && playerUnowned.Count == 0)
        {
            foreach (Meme m in globalGallery)
            {
                playerUnowned.Add(m);
            }
        }

        //Fill the list of names that can be used for new users
        if (userNames.Count == 0)
        {
            FillUserNames();
        }

        UIBehaviors.NewMeme();
        UIBehaviors.NewMeme();
        UIBehaviors.NewMeme();
    }

    /// <summary>
    /// Gets the player's overall gallery of memes
    /// </summary>
    /// <returns> List of player's memes </returns>
    public static List<Meme> GetPlayerGallery()
    {
        return playerGallery;
    }

    /// <summary>
    /// Gets the memes not owned by the player
    /// </summary>
    /// <returns> List of unowned memes </returns>
    public static List<Meme> GetPlayerUnowned()
    {
        return playerUnowned;
    }

    /// <summary>
    /// Gets the global gallery of memes uploaded from Resources/MemeFiles/MemeData.txt
    /// </summary>
    /// <returns> List of all memes uploaded </returns>
    public static List<Meme> GetGlobalGallery()
    {
        return globalGallery;
    }

    /// <summary>
    /// Adds a meme to the player's gallery of memes and removes it from the list of
    /// unowned memes
    /// </summary>
    /// <param name="m"> Meme being added to player's gallery </param>
    public static void AddPlayerMeme(Meme m)
    {
        if (playerUnowned.Remove(m))
        {
            playerGallery.Add(m);
        }
    }

    /// <summary>
    /// Fills the global meme gallery with memes from Resources/MemeFiles/MemeData.txt
    /// </summary>
    private static void FillGlobalGallery()
    {
        TextAsset textFile = (TextAsset)Resources.Load("MemeFiles/MemeData");

        // For each line in MemeData.txt...
        string[] lines = textFile.text.Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            // If the line has the correct number of arguments...
            string[] memeData = lines[i].Split(","[0]);
            if (memeData.Length == 4)
            {
                // Parse the comma-separated line
                string title = memeData[0].Trim();
                string creator = memeData[1].Trim();
                string type = memeData[2].Trim();
                string imageFile = memeData[3].Trim();

                // Select the appropriate type
                Meme.MemeType memeType = Meme.MemeType.Max;
                bool validMeme = true;
                if (type == "DeepFried")
                {
                    memeType = Meme.MemeType.DeepFried;
                }
                else if (type == "Text")
                {
                    memeType = Meme.MemeType.Text;
                }
                else if (type == "Reaction")
                {
                    memeType = Meme.MemeType.Reaction;
                }
                else
                {
                    validMeme = false;
                }

                // Find the image
                Sprite imageSprite = Resources.Load<Sprite>("MemeFiles/" + imageFile);

                if (validMeme)
                {
                    Meme newMeme = new Meme(title, creator, memeType, imageSprite);
                    globalGallery.Add(newMeme);
                }
            }
        }
    }

    /// <summary>
    /// Fills the list of usernames with names from Resources/MemeFiles/NameData.txt
    /// </summary>
    private static void FillUserNames()
    {
        TextAsset textFile = (TextAsset)Resources.Load("MemeFiles/NameData");
        // For each line in NameData.txt...
        string[] lines = textFile.text.Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            userNames.Add(lines[i]);
        }
    }

    /// <summary>
    /// Gets the global list of names generated from Resources/MemeFiles/NameData.txt
    /// </summary>
    /// <returns> List of all names still available</returns>
    public static string GetUserName()
    {
        int rand = Random.Range(0, userNames.Count);
        string s = userNames[rand];
        userNames.RemoveAt(rand);
        return s;
    }


    private static void FillGlobalDialogs()
    {
        TextAsset textFile = (TextAsset)Resources.Load("MemeFiles/MessageData");

        //for each line in MessageData.txt...
        string[] lines = textFile.text.Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] messageData = lines[i].Split(","[0]);
            //Debug.Log(lines[i]);
            if (messageData.Length == 3)
            {
                string message = messageData[0].Trim();
                string type = messageData[1].Trim();
                //Debug.Log(type);

                if (type == "Engage")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            engageTexts.Add(message);
                            break;
                        case "Mom":
                            engageTextsMom.Add(message);
                            break;
                        case "Rival":
                            engageTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            engageTextsLord.Add(message);
                            break;
                    }
                } else if (type == "PlayerWin")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            playerWinTexts.Add(message);
                            break;
                        case "Mom":
                            playerWinTextsMom.Add(message);
                            break;
                        case "Rival":
                            playerWinTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            playerWinTextsLord.Add(message);
                            break;
                    }
                } else if (type == "PlayerLoss")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            playerLossTexts.Add(message);
                            break;
                        case "Mom":
                            playerLossTextsMom.Add(message);
                            break;
                        case "Rival":
                            playerLossTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            playerLossTextsLord.Add(message);
                            break;
                    }
                } else if (type == "PlayerRoundWin")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            playerRoundWinTexts.Add(message);
                            break;
                        case "Mom":
                            playerRoundWinTextsMom.Add(message);
                            break;
                        case "Rival":
                            playerRoundWinTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            playerRoundWinTextsLord.Add(message);
                            break;
                    }
                } else if (type == "PlayerRoundLoss")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            playerRoundLossTexts.Add(message);
                            break;
                        case "Mom":
                            playerRoundLossTextsMom.Add(message);
                            break;
                        case "Rival":
                            playerRoundLossTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            playerRoundLossTextsLord.Add(message);
                            break;
                    }
                } else if (type == "Tie")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            tieTexts.Add(message);
                            break;
                        case "Mom":
                            tieTextsMom.Add(message);
                            break;
                        case "Rival":
                            tieTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            tieTextsLord.Add(message);
                            break;
                    }
                } else if (type == "Converse")
                {
                    switch (messageData[2].Trim())
                    {
                        case "Norm":
                            conversationTexts.Add(message);
                            break;
                        case "Mom":
                            conversationTextsMom.Add(message);
                            break;
                        case "Rival":
                            conversationTextsRival.Add(message);
                            break;
                        case "MemeLord":
                            conversationTextsLord.Add(message);
                            break;
                    }
                }
            }
        }
    }

    public static string GetEnemyMessage(MessageBehaviors.MessageType type)
    {
        int rand;
        if (type == MessageBehaviors.MessageType.Engage)
        {
            rand = Random.Range(0, engageTexts.Count);
            return (engageTexts[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerWin)
        {
            rand = Random.Range(0, playerWinTexts.Count);
            return (playerWinTexts[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerLoss)
        {
            rand = Random.Range(0, playerLossTexts.Count);
            return (playerLossTexts[rand]);
        } else if (type == MessageBehaviors.MessageType.PlayerRoundWin)
        {
            rand = Random.Range(0, playerRoundWinTexts.Count);
            return (playerRoundWinTexts[rand]);
        } else if (type == MessageBehaviors.MessageType.PlayerRoundLoss)
        {
            rand = Random.Range(0, playerRoundLossTexts.Count);
            return (playerRoundLossTexts[rand]);
        } else if (type == MessageBehaviors.MessageType.Tie)
        {
            rand = Random.Range(0, tieTexts.Count);
            return (tieTexts[rand]);
        } else if (type == MessageBehaviors.MessageType.Converse)
        {
            if (conversationTexts.Count > 0)
            {
                rand = Random.Range(0, conversationTexts.Count);
                return (conversationTexts[rand]);
            }
        }
        return null;
    }

    public static string GetMomMessage(MessageBehaviors.MessageType type)
    {
        int rand;
        if (type == MessageBehaviors.MessageType.Engage)
        {
            rand = Random.Range(0, engageTexts.Count);
            return (engageTexts[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerWin)
        {
            rand = Random.Range(0, playerWinTexts.Count);
            return (playerWinTextsMom[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerLoss)
        {
            rand = Random.Range(0, playerLossTexts.Count);
            return (playerLossTextsMom[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundWin)
        {
            rand = Random.Range(0, playerRoundWinTexts.Count);
            return (playerRoundWinTextsMom[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundLoss)
        {
            rand = Random.Range(0, playerRoundLossTexts.Count);
            return (playerRoundLossTextsMom[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Tie)
        {
            rand = Random.Range(0, tieTexts.Count);
            return (tieTextsMom[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Converse)
        {
            rand = Random.Range(0, conversationTexts.Count);
            return (conversationTextsMom[rand]);
        }
        return null;
    }

    public static string GetLordMessage(MessageBehaviors.MessageType type)
    {
        int rand;
        if (type == MessageBehaviors.MessageType.Engage)
        {
            rand = Random.Range(0, engageTexts.Count);
            return (engageTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerWin)
        {
            rand = Random.Range(0, playerWinTexts.Count);
            return (playerWinTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerLoss)
        {
            rand = Random.Range(0, playerLossTexts.Count);
            return (playerLossTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundWin)
        {
            rand = Random.Range(0, playerRoundWinTexts.Count);
            return (playerRoundWinTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundLoss)
        {
            rand = Random.Range(0, playerRoundLossTexts.Count);
            return (playerRoundLossTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Tie)
        {
            rand = Random.Range(0, tieTexts.Count);
            return (tieTextsLord[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Converse)
        {
            rand = Random.Range(0, conversationTexts.Count);
            return (conversationTextsLord[rand]);
        }
        return null;
    }

    public static string GetRivalMessage(MessageBehaviors.MessageType type)
    {
        int rand;
        if (type == MessageBehaviors.MessageType.Engage)
        {
            rand = Random.Range(0, engageTexts.Count);
            return (engageTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerWin)
        {
            rand = Random.Range(0, playerWinTexts.Count);
            return (playerWinTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerLoss)
        {
            rand = Random.Range(0, playerLossTexts.Count);
            return (playerLossTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundWin)
        {
            rand = Random.Range(0, playerRoundWinTexts.Count);
            return (playerRoundWinTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.PlayerRoundLoss)
        {
            rand = Random.Range(0, playerRoundLossTexts.Count);
            return (playerRoundLossTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Tie)
        {
            rand = Random.Range(0, tieTexts.Count);
            return (tieTextsRival[rand]);
        }
        else if (type == MessageBehaviors.MessageType.Converse)
        {
            rand = Random.Range(0, conversationTexts.Count);
            return (conversationTextsRival[rand]);
        }
        return null;
    }

}
