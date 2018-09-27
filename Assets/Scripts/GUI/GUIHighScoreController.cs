using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHighScoreController : MonoBehaviour
{

    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    void Start()
    {
        OnChangePoints(GameManager.Instance.HighScore);
        GameManager.Instance.OnChangeHighScorePoints += OnChangePoints;
    }

    private void OnChangePoints(int currentPoints)
    {
        _text.text = currentPoints.ToString("000000");
    }


}
