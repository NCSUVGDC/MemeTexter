using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviors : MonoBehaviour
{
    public static GameObject canvas;
    public static GameObject chatsPage;
    public static GameObject contentChats;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas");
        }

        if (chatsPage == null)
        {
            chatsPage = GameObject.Find("RunningChats");
        }

        if (contentChats == null)
        {
            contentChats = GameObject.Find("ContentChats");
        }

        
    }

    public void OpenChat(Chat chat)
    {
        if (chat.otherPage == null)
        {
            GameObject newPage = (GameObject)Instantiate(Resources.Load("ChatPage"));
            newPage.transform.SetParent(canvas.transform, false);
            chat.otherPage = newPage;
            chat.chatGallery = newPage.GetComponentInChildren<Gallery>();
            chat.chatGallery.CreateGallery();
        }
        else
        {
            chat.otherPage.SetActive(true);
            chat.chatGallery.UpdateGallery();
        }
    }

    public void OpenSettings(Chat chat)
    {
        if (chat.otherPage == null)
        {
            GameObject newPage = (GameObject)Instantiate(Resources.Load("TestSettings"));
            newPage.transform.SetParent(canvas.transform, false);
            chat.otherPage = newPage;
        }
        else
        {
            chat.otherPage.SetActive(true);
        }
    }

    public void NewChat()
    {
        GameObject newChat = (GameObject)Instantiate(Resources.Load("ChatButton"));
        newChat.transform.SetParent(contentChats.transform, false);
    }

    public void NewMeme()
    {
        if (GlobalGallery.GetPlayerUnowned().Count > 0)
        {
            int index = Random.Range(0, GlobalGallery.GetPlayerUnowned().Count);
            GlobalGallery.AddPlayerMeme(GlobalGallery.GetPlayerUnowned().ToArray()[index]);
        }
    }

    public void OpenRunningChats(GameObject currentPage)
    {
        chatsPage.SetActive(true);
        currentPage.SetActive(false);
    }


}
