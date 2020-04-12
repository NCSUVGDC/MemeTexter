using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meme
{
    // Title of meme
    private string title;

    // Creator of meme
    private string creator;
    
    // Type of meme
    private MemeType type;

    // Meme image
    private Sprite imageSprite;

    // True if used in running match, false otherwise
    public bool used = false;

    // Button GameObject for selecting meme
    private GameObject button;
    
    /// <summary>
    /// Enum for types of memes
    /// </summary>
    public enum MemeType
    {
        DeepFried,
        Text,
        Reaction,
        Max
    }

    /// <summary>
    /// Constructs a Meme using the title, creator, type, and image filename
    /// </summary>
    /// <param name="title">Title of the meme</param>
    /// <param name="creator">Creator of the meme</param>
    /// <param name="type">Type of meme</param>
    /// <param name="imageSprite">Meme image</param>
    public Meme(string title, string creator, MemeType type, Sprite imageSprite)
    {
        this.title = title;
        this.creator = creator;
        this.type = type;
        this.imageSprite = imageSprite;
    }
    
    /// <summary>
    /// Gets the title of the meme
    /// </summary>
    /// <returns>Meme title</returns>
    public string GetTitle()
    {
        return title;
    }

    /// <summary>
    /// Gets the creator of the meme
    /// </summary>
    /// <returns>Meme creator</returns>
    public string GetCreator()
    {
        return creator;
    }

    /// <summary>
    /// Gets the type of the meme
    /// </summary>
    /// <returns>Meme type</returns>
    public MemeType GetMemeType()
    {
        return type;
    }

    /// <summary>
    /// Gets the image of the meme
    /// </summary>
    /// <returns>Image sprite</returns>
    public Sprite GetImageSprite()
    {
        return imageSprite;
    }

    /// <summary>
    /// Sets the button GameObject that corresponds to the meme
    /// in a specific chat.
    /// </summary>
    /// <param name="button">Meme UI button for a specific chat</param>
    public void SetButton(GameObject button)
    {
        this.button = button;
    }

    /// <summary>
    /// Gets the button of the meme
    /// </summary>
    /// <returns>Button GameObject</returns>
    public GameObject GetButton()
    {
        return button;
    }

}
