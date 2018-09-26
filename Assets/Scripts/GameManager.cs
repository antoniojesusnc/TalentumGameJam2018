using System;
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

    [SerializeField]
    float _minOrderDelayTime;
    [SerializeField]
    float _maxOrderDelayTime;

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
    [SerializeField]
    float _alphaWhenDragging;

    [Header("GameOver")]
    public GameObject _guIGameOver;

    public CanvasGroup DropAreaCanvas
    {
        get
        {
            return _dropArea.GetComponent<CanvasGroup>();
        }
    }
    float _dropAreaTopPos;

    [Header("Points")]
    [SerializeField]
    int _pointsPerTable;
    int _currentPoints;
    int _bestPoints;


    public delegate void DelegateOnUpdateLifes(int currentLifes);
    public event DelegateOnUpdateLifes OnUpdateLifes;

    public delegate void DelegateChangePoints(int currentPoints);
    public event DelegateChangePoints OnChangePoints;

    // Use this for initialization
    void Start()
    {
        _dropAreaTopPos = _dropArea.transform.position.y - 0.5f * _dropArea.sizeDelta.y * _dropArea.lossyScale.y;
        _currentLifes = _totalLifes;
    }


    public void AddPoints()
    {
        _currentPoints += GetOrderAmount() * _pointsPerTable;
        if (OnChangePoints != null)
            OnChangePoints(_currentPoints);
    }

    public void TakeLife()
    {
        --_currentLifes;
        if(_currentLifes <= 0)
        {
            SoundManager.Instance.PlayDefeat();
            FinishGame();
        }

        if (OnUpdateLifes != null)
        {
            OnUpdateLifes(_currentLifes);

            if (_currentLifes == 1)
                SoundManager.Instance.OneLifeLeft();
        }
            
    }

    private void FinishGame()
    {
        _guIGameOver.GetComponent<GUIGameOver>().GameOver();
    }

    public void GetOrderProperties(out int amount, out EDoneness doneness, out float duration)
    {
        doneness = (EDoneness)UnityEngine.Random.Range(1, 6);

        int incrementByTime = Mathf.FloorToInt( Time.timeSinceLevelLoad / _incrementOneAllAtTime);
        amount = UnityEngine.Random.Range(_minOrderAmount + incrementByTime, _maxOrderAmount + incrementByTime);
        duration = UnityEngine.Random.Range(_minOrderTime, _maxOrderTime);
    }

    public int GetOrderAmount()
    {
        return _startOrdersAmount + Mathf.FloorToInt(Time.timeSinceLevelLoad / _timeToIncreaseNumOrders);
    }

    public float GetOrderDelayTime()
    {
        return UnityEngine.Random.Range(_minOrderDelayTime, _maxOrderDelayTime);
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
        DropAreaCanvas.alpha = enable?0:_alphaWhenDragging;

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
