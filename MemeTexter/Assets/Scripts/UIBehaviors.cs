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
            GameObject newPage = (GameObject)Instantiate(Resources.Load("TestChat"));
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

    public void OpenRunningChats(GameObject currentPage)
    {
        chatsPage.SetActive(true);
        currentPage.SetActive(false);
    }
}
