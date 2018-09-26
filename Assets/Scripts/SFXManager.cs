using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public AudioSource espetoCookPlus;
    public AudioSource espetoCookWasted;
    public AudioSource espetoDragDrop;
    public AudioSource ringOrder;
    public AudioSource ringOrderLost;


    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayCookPlus()
    {
        espetoCookPlus.Play();
    }

    void PlayCookWasted()
    {
        espetoCookWasted.Play();
    }

    void PlayDragDrop()
    {
        espetoDragDrop.Play();
    }

    void playRingOrder()
    {
        ringOrder.Play();
    }

    void PlayRingOrderLost()
    {
        ringOrderLost.Play();
    }
}
