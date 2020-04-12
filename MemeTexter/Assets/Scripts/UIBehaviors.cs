using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            newPage.GetComponent<Match>().nameObject.GetComponent<Text>().text = chat.user.userName;
            chat.otherPage = newPage;
            chat.chatGallery = newPage.GetComponentInChildren<Gallery>();
            chat.chatGallery.CreateGallery();

            GameObject otherPage = chat.otherPage;
            otherPage.GetComponent<Match>().user = chat.gameObject.GetComponent<User>();
        }
        else
        {
            chat.otherPage.SetActive(true);
            chat.chatGallery.UpdateGallery();
        }

        gameObject.transform.SetAsFirstSibling();
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

    /**
     * Method to create a new chat.
     * Instatiates a new chat prefab and gives it a new generated User
     */
    public void NewChat()
    {
        GameObject newChat = (GameObject)Instantiate(Resources.Load("ChatButton"));
        newChat.transform.SetParent(contentChats.transform, false);

        GenerateUser(newChat);

        newChat.GetComponent<Chat>().user = newChat.GetComponent<User>();
        newChat.GetComponentInChildren<Text>().text = newChat.GetComponent<User>().userName;

        newChat.transform.SetAsFirstSibling();
    }

    public void NewMeme()
    {
        if (GlobalGallery.GetPlayerUnowned().Count > 0)
        {
            int index = Random.Range(0, GlobalGallery.GetPlayerUnowned().Count);
            GlobalGallery.AddPlayerMeme(GlobalGallery.GetPlayerUnowned().ToArray()[index]);
        }
    }

    /**
     * Method to return to the home page with all open chats
     */
    public void OpenRunningChats(GameObject currentPage)
    {
        chatsPage.SetActive(true);
        currentPage.SetActive(false);
    }

    /**
     * Method used to generate a new username
     */
    private void GenerateUser(GameObject chat)
    {
        //randomly get name, somehow (need a list of names somewhere, similar implementation to memes?)
        string name = GlobalGallery.GetUserName();

        //randomly generate difficulty (for now I guess?)
        int difficulty = Random.Range(1, GlobalGallery.GetPlayerGallery().Count);

        chat.AddComponent<User>();
        chat.GetComponent<User>().userName = name;
        chat.GetComponent<User>().difficulty = difficulty;
    }


}
