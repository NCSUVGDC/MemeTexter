using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{

    public void AddMeme(Sprite img)
    {
        //construct a meme
        GameObject newMeme = (GameObject)Instantiate(Resources.Load("TextMessage"));
        newMeme.GetComponentInChildren<Image>().sprite = img;

        //add to the scrollview


        //pass to global addmeme
        GlobalGallery.AddGlobalMeme(newMeme.GetComponent<Meme>());
    }
}
