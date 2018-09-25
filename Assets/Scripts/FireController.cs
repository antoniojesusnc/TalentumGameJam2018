using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    public float _minScale;
    public float _maxScale;

    [Header("Behavior")]
    [SerializeField]
    float _minTimeToChange;
    [SerializeField]
    float _maxTimeToChange;
    [SerializeField]
    float _minTimeChanging;
    [SerializeField]
    float _maxTimeChanging;


    Vector2 _originalSize;

    void Start () {

        _originalSize = GetComponent<RectTransform>().sizeDelta;
        float scaleRate = Random.Range(_minScale, _maxScale);
        GetComponent<RectTransform>().sizeDelta = _originalSize * scaleRate;

        GetComponent<Animator>().Play(
            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash
            , -1, Random.Range(0.0f, 1.0f));

        float nextChange = Random.Range(_minTimeToChange, _maxTimeToChange);
        Invoke("ChangeFireSize", nextChange);

    }
    
    void ChangeFireSize()
    {
        float scaleRate = Random.Range(_minScale, _maxScale);
        Vector2 nextSize = _originalSize * scaleRate;
        float animTime = Random.Range(_minTimeChanging, _maxTimeChanging);
        LeanTween.size(GetComponent<RectTransform>(), nextSize, animTime);

        float nextChange = Random.Range(_minTimeToChange, _maxTimeToChange);
        Invoke("ChangeFireSize", nextChange + animTime);
    }
}
