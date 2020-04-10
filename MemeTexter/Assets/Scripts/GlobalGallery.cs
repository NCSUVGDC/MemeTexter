using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGallery : object
{
    public static List<Meme> globalGallery;

    public static void AddGlobalMeme(Meme m)
    {
        globalGallery.Add(m);
    }
}
