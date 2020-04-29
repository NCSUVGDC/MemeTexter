using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MessageManager : MonoBehaviour
{
    /// <summary>
    /// The list of all possible text messages
    /// </summary>
    public static List<Message> texts = new List<Message>();

    /// <summary>
    /// The only instance of this MessageManager that is allowed to exist
    /// </summary>
    private static MessageManager singleton;

    /// <summary>
    /// Immediately as the MessageManager is created, figure out the singleton stuff and generate the text messages from the MessageData file if it hasn't already been done.
    /// </summary>
    void Awake()
    {
        // Singleton stuff
        if (singleton != null)
            Destroy(singleton);
        else
            singleton = this;

        DontDestroyOnLoad(this);

        //fill the dialogs if empty
        if (texts.Count == 0)
        {
            FillGlobalDialogs();
        }
    }

    /// <summary>
    /// Scrub through the MessageData text file to read in text messages as well as their messageTypes and senderTypes
    /// </summary>
    private static void FillGlobalDialogs()
    {
        TextAsset textFile = (TextAsset)Resources.Load("MemeFiles/MessageData");

        //for each line in MessageData.txt...
        string[] lines = textFile.text.Split("\n"[0]);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] messageData = lines[i].Split(","[0]);
            if (messageData.Length == 3)
            {
                //prepare the Message
                string message;
                message = messageData[0].Trim();

                //prepare the MessageType
                MessageType type = new MessageType();
                if (!Enum.TryParse(messageData[1].Trim(), true, out type))
                {
                    Debug.Log("Can't parse the message type on line: " + i);
                }

                //prepare the SenderType
                SenderType sender = new SenderType();
                if (!Enum.TryParse(messageData[2].Trim(), true, out sender))
                {
                    Debug.Log("Can't parse the sender type on line: " + i);
                }

                //build and add the message to our list
                Message m = new Message(message, type, sender);
                texts.Add(m);
                
            } else
            {
                Debug.Log("Problem with MessageData format on line: " + i);
            }
        }
    }   

    /// <summary>
    /// Generate a new, random message from a given senderType for a given scenario
    /// </summary>
    /// <param name="mType">The type of message to return, AKA the scenario (conversation, battle, etc.)</param>
    /// <param name="sType">The type of sender for this message (Mom, Dad, Rival, etc.)</param>
    /// <returns>A random message with the given constraints</returns>
    public static Message GetMessage(MessageType mType, SenderType sType)
    {
        //this is still kinda ugly
        //iterate through the whole list to get 
        List<Message> tempList = new List<Message>();
        for (int i = 0; i < texts.Count; i++)
        {
            if (mType == texts[i].getMessageType() && sType == texts[i].getSenderType())
            {
                tempList.Add(texts[i]);
            }
        }

        //need to specify UnityEngine here because we are using the "System" library
        int rand = UnityEngine.Random.Range(0, tempList.Count);

        //grab a random entry and return it
        return tempList[rand];
    }
}
