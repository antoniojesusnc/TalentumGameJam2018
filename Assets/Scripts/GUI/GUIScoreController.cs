using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScoreController : MonoBehaviour
{

    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    void Start()
    {
        OnChangePoints(5);
        GameManager.Instance.OnChangePoints += OnChangePoints;
    }

    private void OnChangePoints(int currentPoints)
    {
        _text.text = currentPoints.ToString("000000");
    }


}
