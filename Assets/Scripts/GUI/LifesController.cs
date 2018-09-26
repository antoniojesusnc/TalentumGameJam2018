using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesController : MonoBehaviour {

    [SerializeField]
    List<GameObject> _lifes;

    void Start () {
        GameManager.Instance.OnUpdateLifes += OnUpdateLifes;
        OnUpdateLifes(GameManager.Instance.Lifes);
    }

    private void OnUpdateLifes(int currentLifes)
    {
        for (int i = 0; i < _lifes.Count; ++i)
        {
            _lifes[i].SetActive(i < currentLifes);
        }
    }

    public void TakeLife()
    {

    }
}
