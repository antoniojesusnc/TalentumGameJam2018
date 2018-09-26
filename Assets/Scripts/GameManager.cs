﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Orders")]
    [SerializeField]
    int _minOrderAmount;
    [SerializeField]
    int _maxOrderAmount;
    [SerializeField]
    float _minOrderTime;
    [SerializeField]
    float _maxOrderTime;
    [SerializeField]
    int _incrementOneAllAtTime;
    [SerializeField]
    int _timeToIncreaseNumOrders;
    [SerializeField]
    int _startOrdersAmount;

    [Header("Lifes")]
    [SerializeField]
    int _totalLifes;

    int _currentLifes;
    public int Lifes
    {
        get
        {
            return _currentLifes;
        }
    }

    [Header("Doneness Values")]
    [SerializeField]
    List<DonenessInfo> _donenessValues;


    [Header("Distance Rates")]
    [SerializeField]
    float _closeValue;
    [SerializeField]
    float _farValue;
    [SerializeField]
    RectTransform _dropArea;
    float _dropAreaTopPos;


    public delegate void DelegateOnUpdateLifes(int currentLifes);
    public event DelegateOnUpdateLifes OnUpdateLifes;

    // Use this for initialization
    void Start()
    {
        _dropAreaTopPos = _dropArea.transform.position.y - 0.5f * _dropArea.sizeDelta.y * _dropArea.lossyScale.y;
        _currentLifes = _totalLifes;
    }

    public void TakeLife()
    {
        --_currentLifes;
        if(_currentLifes <= 0)
        {
            FinishGame();
        }

        if (OnUpdateLifes != null)
            OnUpdateLifes(_currentLifes);
    }

    private void FinishGame()
    {
        Debug.Log("GameFinished");
    }

    public void GetOrderProperties(out int amount, out EDoneness doneness, out float duration)
    {
        doneness = (EDoneness)UnityEngine.Random.Range(1, 4);

        int incrementByTime = Mathf.FloorToInt( Time.timeSinceLevelLoad / _incrementOneAllAtTime);
        amount = UnityEngine.Random.Range(_minOrderAmount + incrementByTime, _maxOrderAmount + incrementByTime);
        duration = UnityEngine.Random.Range(_minOrderTime, _maxOrderTime);
    }

    public int GetOrderAmount()
    {
        return _startOrdersAmount + Mathf.FloorToInt(Time.timeSinceLevelLoad / _timeToIncreaseNumOrders);
    }

    public float GetIncrementValue(EspetoController espeto)
    {
        float currentDistance = _dropAreaTopPos - espeto.transform.position.y;
        float maxDistance = -_dropArea.sizeDelta.y;

        return Mathf.Lerp(_closeValue, _farValue, currentDistance / maxDistance);
    }

    public EDoneness GetDoneness(float donenessValue)
    {
        return (EDoneness)Mathf.RoundToInt(donenessValue);
    }


    public void SetAllEspetoRayCast(bool enable)
    {
        var list = GameObject.FindObjectsOfType<EspetoController>();
        for (int i = list.Length - 1; i >= 0; --i)
        {
            list[i].GetComponent<GraphicRaycaster>().enabled = enable;
        }
    }

    public Color GetImage(float donenessValue)
    {
        return GetImage(GetDoneness(donenessValue));
    }

    public Color GetImage(EDoneness doneness)
    {
        for (int i = _donenessValues.Count - 1; i >= 0; --i)
        {
            if (_donenessValues[i].doneness == doneness)
            {
                return _donenessValues[i].color;
            }
        }
        Debug.LogWarning("No sprite found");
        return GetImage(EDoneness.WASTED);
    }
}

[System.Serializable]
public class DonenessInfo
{
    public EDoneness doneness;
    public Color color;
}
