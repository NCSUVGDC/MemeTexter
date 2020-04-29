using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : object
{
    /// <summary>
    /// Content of the message
    /// </summary>
    public string messageContent;

    /// <summary>
    /// The type of the message from the MessageType enum
    /// </summary>
    public MessageType messageType;

    /// <summary>
    /// The type of this message's sender from the SenderType enum
    /// </summary>
    public SenderType senderType;

    /// <summary>
    /// Constructor for a Message object
    /// </summary>
    /// <param name="mContent">The message content</param>
    /// <param name="mType">The message type (from the MessageType enum)</param>
    /// <param name="sType">The sender type (from the SenderType enum)</param>
    public Message(string mContent, MessageType mType, SenderType sType)
    {
        messageContent = mContent;
        messageType = mType;
        senderType = sType;
    }

    public Message()
    {

    }

    /// <summary>
    /// Get the actual content of the message
    /// </summary>
    /// <returns>the content of the message</returns>
    public string getMessageContent()
    {
        return messageContent;
    }

    /// <summary>
    /// Get the type of message this is (greeting, battleEntry, etc.)
    /// </summary>
    /// <returns>the messageType for this message</returns>
    public MessageType getMessageType()
    {
        return messageType;
    }

    /// <summary>
    /// Get the type of sender this is from (Mom, Enemy, MemeLord, etc.)
    /// </summary>
    /// <returns>the senderType for this message</returns>
    public SenderType getSenderType()
    {
        return senderType;
    }

    /// <summary>
    /// May or may not be used, I imagine this could be useful in a custom save routine
    /// </summary>
    /// <returns>An encoded string of this Message to match the formatting of our MessageData file</returns>
    public override string ToString()
    {
        string returnThis = messageContent;
        returnThis += ",";
        returnThis += messageType.ToString();
        returnThis += ",";
        returnThis += senderType.ToString();
        returnThis += "\n";

        return returnThis;
    }

}

