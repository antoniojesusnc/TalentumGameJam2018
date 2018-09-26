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

    public void PlayCookPlus()
    {
        espetoCookPlus.Play();
    }

    public void PlayCookWasted()
    {
        espetoCookWasted.Play();
    }

    public void PlayDragDrop()
    {
        espetoDragDrop.Play();
    }

    public void playRingOrder()
    {
        ringOrder.Play();
    }

    public void PlayRingOrderLost()
    {
        ringOrderLost.Play();
    }
}
