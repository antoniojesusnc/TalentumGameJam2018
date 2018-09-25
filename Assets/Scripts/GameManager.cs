using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

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

    // Use this for initialization
    void Start()
    {
        _dropAreaTopPos = _dropArea.transform.position.y - 0.5f * _dropArea.sizeDelta.y * _dropArea.lossyScale.y;
    }


    public float GetIncrementValue(EspetoController espeto)
    {
        float currentDistance = _dropAreaTopPos - espeto.transform.position.y;
        float maxDistance = -_dropArea.sizeDelta.y;

        return Mathf.Lerp(_farValue, _closeValue, currentDistance / maxDistance);
    }

    public EDoneness GetDoneness(float donenessValue)
    {
        return (EDoneness)Mathf.RoundToInt(donenessValue);
    }

    public Sprite GetImage(float donenessValue)
    {
        return GetImage(GetDoneness(donenessValue));
    }

    internal void SetAllEspetoRayCast(bool enable)
    {
        var list = GameObject.FindObjectsOfType<EspetoController>();
        for (int i = list.Length - 1; i >= 0; --i)
        {
            list[i].GetComponent<GraphicRaycaster>().enabled = enable;
        }
    }

    public Sprite GetImage(EDoneness doneness)
    {
        for (int i = _donenessValues.Count - 1; i >= 0; --i)
        {
            if (_donenessValues[i].doneness == doneness)
            {
                return _donenessValues[i].sprite;
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
    public Sprite sprite;
}
