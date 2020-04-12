using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                    //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;        //Allows other scripts to call functions from SoundManager.                
    public float lowPitchRange = .95f;                //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    public AudioClip idleMusic;
    public List<AudioClip> battleMusic;

    public AudioClip send;
    public AudioClip receive;
    public AudioClip button;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);



    }


    //Used to play the idle music (exiting combat)
    public void PlayIdle()
    {
        musicSource.clip = idleMusic;

        //Play the clip.
        musicSource.Play();
    }


    //Used to play battle music (entering combat)
    public void PlayRandomBattle()
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, battleMusic.Count);

        //Set the clip to the clip at our randomly chosen index.
        musicSource.clip = battleMusic[randomIndex];

        //Play the clip.
        musicSource.Play();
    }

    public void PlaySend()
    {
        efxSource.clip = send;
        efxSource.Play();
    }

    public void PlayReceive()
    {
        efxSource.clip = receive;
        efxSource.Play();
    }

    public void PlayButton()
    {
        efxSource.clip = button;
        efxSource.Play();
    }


}
