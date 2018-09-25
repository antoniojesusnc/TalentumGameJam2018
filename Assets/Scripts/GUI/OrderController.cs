using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour {

    public int _amount;
    public EDoneness _doneness;

	public void SetProperties(int amount, EDoneness doneness)
    {
        _amount = amount;
        _doneness = doneness;
    }

    public void AddFish(EspetoController espeto)
    {
        if(espeto.Doneness == _doneness)
        {
            --_amount;
            if(_amount <= 0)
            {
                Finish();
            }
            else
            {
                UpdateGUI();
            }
        }
        else
        {
            Debug.Log("OrderController Bad Espeto");
        }
    }

    private void UpdateGUI()
    {
        Debug.Log("OrderController UpdateGUI");
    }

    private void Finish()
    {
        Debug.Log("OrderController Finish");
    }
}
