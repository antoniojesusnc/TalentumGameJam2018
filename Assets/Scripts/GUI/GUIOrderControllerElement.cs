using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIOrderControllerElement : MonoBehaviour
{
    [Header("Control vars")]
    public int _amount;
    public EDoneness _doneness;
    public float _duration;

    [Header("GUI")]
    [SerializeField]
    TMPro.TextMeshProUGUI _amountText;
    [SerializeField]
    Image _image;
    [SerializeField]
    Image _bar;

    float _timeStamp;
    bool _starting;
    bool _finish;
    public bool Disable { get; set; }

    private void Awake()
    {
        
    }

    public void SetProperties(int amount, EDoneness doneness, float duration)
    {
        _amount = amount;
        _doneness = doneness;
        _duration = duration;
        _timeStamp = _duration;

        UpdateGUI();
    }

    internal void StartAnim()
    {
        Disable = false;
    }

    public void AddEspeto(EspetoController espeto)
    {
        if (_amount <= 0)
            return;

        if (espeto.Doneness == _doneness)
        {
            --_amount;
            if (_amount <= 0)
            {
                FinishEspeto();
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

    private void Update()
    {
        if (Disable || _starting || _finish)
            return;

        _timeStamp -= Time.deltaTime;
        UpdateGUIBar();
    }

    private void UpdateGUIBar()
    {
        _bar.fillAmount = Mathf.Lerp(0, 1, _timeStamp / _duration);
        if(_timeStamp <= 0)
        {
            FinishTime();
        }
    }

    private void UpdateGUI()
    {
        _amountText.text = _amount.ToString();
        _image.color = GameManager.Instance.GetImage(_doneness);

        UpdateGUIBar();
    }


    void FinishTime()
    {
        _finish = true;

        GameManager.Instance.TakeLife();
    }

    void FinishEspeto()
    {
        _finish = true;
        Debug.Log("OrderController FinishEspeto");
    }
}
