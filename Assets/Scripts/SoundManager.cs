using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    public AudioSource currentMusic;
    public AudioSource fireSFXLoop;
    public AudioSource oneLifeLeftMusic;
    public AudioSource baseMusic;
    public AudioSource failureMusic;

    // Use this for initialization
    void Awake ()
    {
        currentMusic = baseMusic;
        currentMusic.Play();
        fireSFXLoop.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayBaseMusic()
    {
        currentMusic.Stop();
        currentMusic = baseMusic;
        currentMusic.Play();
    }

    public void PlayDefeat(){
        currentMusic.Stop();
        currentMusic = failureMusic;
        currentMusic.Play();
    }

    public void OneLifeLeft(){
        currentMusic.Stop();
        fireSFXLoop.Stop();
        currentMusic = oneLifeLeftMusic;
        currentMusic.Play();
    }
}
