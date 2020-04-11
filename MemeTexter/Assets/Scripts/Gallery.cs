using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    // Memes to be used in the current ChatPage, should contain all of player's memes
    List<Meme> ownedMemes = new List<Meme>();
    public GameObject contentMessages;
    public Match match;

    /// <summary>
    /// Updates the chat's gallery with any memes that have been obtained
    /// since the last time the chat was opened. Gets called when the chat
    /// is opened.
    /// </summary>
    public void UpdateGallery()
    {
        // This is what I call "The Pokemon Go Approach", because I
        // guarantee that they have their stuff searching at O(n^2)
        // --Joseph
        if (ownedMemes.Count != GlobalGallery.GetPlayerGallery().Count)
        {
            
            foreach (Meme meme in GlobalGallery.GetPlayerGallery())
            {
                if (!ownedMemes.Contains(meme))
                {
                    GameObject memeButton = (GameObject)Instantiate(Resources.Load("Meme"));
                    memeButton.GetComponent<MessageBehaviors>().contentMessages = contentMessages;
                    memeButton.GetComponent<MessageBehaviors>().match = match;
                    memeButton.GetComponent<Image>().sprite = meme.GetImageSprite();
                    memeButton.transform.SetParent(gameObject.transform, false);
                    meme.SetButton(memeButton);
                    ownedMemes.Add(meme);
                }

                if (ownedMemes.Count == GlobalGallery.GetPlayerGallery().Count)
                {
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Creates the chat's gallary the first time the ChatPage is opened. Gets
    /// called when the chat is opened for the first time.
    /// </summary>
    public void CreateGallery()
    {
        Meme[] tempmemes = new Meme[GlobalGallery.GetPlayerGallery().Count];
        GlobalGallery.GetPlayerGallery().CopyTo(tempmemes);
        
        foreach(Meme temp in tempmemes)
        {
            ownedMemes.Add(temp);
        }

        foreach (Meme meme in ownedMemes)
        {
            GameObject memeButton = (GameObject)Instantiate(Resources.Load("Meme"));
            memeButton.GetComponent<MessageBehaviors>().contentMessages = contentMessages;
            memeButton.GetComponent<MessageBehaviors>().match = match;
            memeButton.GetComponent<Image>().sprite = meme.GetImageSprite();
            memeButton.transform.SetParent(gameObject.transform, false);
            meme.SetButton(memeButton);
        }
        
    }





    // Not sure what to do with this
    public void AddMeme(Sprite img)
    {
        //construct a meme
        GameObject newMeme = (GameObject)Instantiate(Resources.Load("TextMessage"));
        newMeme.GetComponentInChildren<Image>().sprite = img;
        newMeme.GetComponent<Meme>();

        //add to the scrollview


        //pass to global addmeme
        //GlobalGallery.AddGlobalMeme(newMeme.GetComponent<Meme>());
    }
}
