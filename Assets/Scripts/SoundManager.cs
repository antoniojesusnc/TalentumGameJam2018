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
        currentMusic = oneLifeLeftMusic;
        currentMusic.Play();
        fireSFXLoop.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void PlayBaseMusic()
    {
        currentMusic.Stop();
        currentMusic = baseMusic;
        currentMusic.Play();
    }

    private void PlayDefeat(){
        currentMusic.Stop();
        currentMusic = failureMusic;
        currentMusic.Play();
    }

    private void OneLifeLeft(){
        currentMusic.Stop();
        fireSFXLoop.Stop();
        currentMusic = oneLifeLeftMusic;
        currentMusic.Play();
    }
}
