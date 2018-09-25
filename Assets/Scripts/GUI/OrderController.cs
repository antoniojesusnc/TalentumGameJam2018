﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    [Header("Control vars")]
    public int _amount;
    public EDoneness _doneness;

    [Header("GUI")]
    public TMPro.TextMeshProUGUI _amountText;
    public Image _image;

    public void SetProperties(int amount, EDoneness doneness)
    {
        _amount = amount;
        _doneness = doneness;
    }

    public void AddFish(EspetoController espeto)
    {
        if (_amount <= 0)
            return;

        if (espeto.Doneness == _doneness)
        {
            --_amount;
            if (_amount <= 0)
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
        _amountText.text = _amount.ToString();
        //_image.sprite = 
    }

    private void Finish()
    {
        Debug.Log("OrderController Finish");
    }
}
