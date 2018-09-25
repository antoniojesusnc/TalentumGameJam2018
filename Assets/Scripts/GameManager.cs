using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Doneness Values")]
    [SerializeField]
    List<DonenessInfo> _donenessValues;


    [Header("DistanceRates")]
    [SerializeField]
    float _closeValue;
    float _farValue;


    // Use this for initialization
    void Start () {
		
	}


    public static Sprite GetImage(float donenessValue)
    {
        return GetImage(Mathf.RoundToInt(donenessValue));
    }

    public static Sprite GetImage(EDoneness doneness)
    {
        return null;
    }
}

public class DonenessInfo
{
    public EDoneness doneness;
    public Sprite sprite;
}
