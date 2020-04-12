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
        // For each line in MemeData.txt...
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
            if (messageData.Length == 2)
            {
                string message = messageData[0].Trim();
                string type = messageData[1].Trim();
                //Debug.Log(type);

                if (type == "Engage")
                {
                    engageTexts.Add(message);
                } else if (type == "PlayerWin")
                {
                    playerWinTexts.Add(message);
                } else if (type == "PlayerLoss")
                {
                    playerLossTexts.Add(message);
                } else if (type == "PlayerRoundWin")
                {
                    playerRoundWinTexts.Add(message);
                } else if (type == "PlayerRoundLoss")
                {
                    playerRoundLossTexts.Add(message);
                } else if (type == "Tie")
                {
                    tieTexts.Add(message);
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
        }
        return null;
    }
}
