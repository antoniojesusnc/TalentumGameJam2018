using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOrdersController : MonoBehaviour {

    public List<GUIOrderControllerElement> _orders;

    int _currentOrderAmounts;

	void Start () {
        SetInitialValues();

        //InvokeRepeating("CheckIncreaseNumOrders",1,1);
	}

    private void SetInitialValues()
    {
        _currentOrderAmounts = GameManager.Instance.GetOrderAmount();

        int amount;
        EDoneness doneness;
        float duration;
        for (int i = 0; i < _orders.Count; i++)
        {
            if(i < _currentOrderAmounts)
            {
                GameManager.Instance.GetOrderProperties(out amount, out doneness, out duration);
                _orders[i].SetProperties(amount, doneness, duration);
                _orders[i].StartAnim();
            }
        }
    }

    void CheckIncreaseNumOrders()
    {
        int newOrderAmount = GameManager.Instance.GetOrderAmount();
        if(_currentOrderAmounts <= newOrderAmount)
        {
            _currentOrderAmounts = newOrderAmount;
            _orders[_currentOrderAmounts - 1].Disable = false;
        }

        if (_currentOrderAmounts == _orders.Count)
            CancelInvoke("CheckIncreaseNumOrders");
    }


}
