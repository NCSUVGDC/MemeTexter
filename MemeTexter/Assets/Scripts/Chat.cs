using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public User user;
    public GameObject otherPage = null;
    public Gallery chatGallery;

    public GameObject userImageObj;
    public GameObject difficultyObj;

    void Start()
    {
        if (userImageObj != null)
        {
            Object[] images = Resources.LoadAll("Profiles", typeof(Sprite));
            int rand = Random.Range(0, images.Length - 1);
            userImageObj.GetComponent<Image>().sprite = (Sprite)images[rand];
        }
        
        if (difficultyObj != null)
        {
            difficultyObj.GetComponent<Text>().text = "Difficulty: " + user.difficulty;
            Debug.Log(user.difficulty);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
