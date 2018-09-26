using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOrdersController : MonoBehaviour
{

    public List<GUIOrderControllerElement> _orders;

    int _currentOrderAmounts;

    void Start()
    {
        for (int i = 0; i < _orders.Count; ++i)
        {
            _orders[i].SetGUIOrdersController(this);
        }

        SetInitialValues();

        InvokeRepeating("CheckIncreaseNumOrders",1,1);
    }

    private void SetInitialValues()
    {
        _currentOrderAmounts = GameManager.Instance.GetOrderAmount();


        for (int i = 0; i < _orders.Count; i++)
        {
            if (i < _currentOrderAmounts)
            {
                StartOrder(i);
            }
        }
    }


    IEnumerator DelayStartOrderCo(int index)
    {
        float delayTime = GameManager.Instance.GetOrderDelayTime();
        yield return new WaitForSeconds(delayTime);
        StartOrder(index);
    }
    void StartOrder(int index)
    {
        int amount;
        EDoneness doneness;
        float duration;
        GameManager.Instance.GetOrderProperties(out amount, out doneness, out duration);
        _orders[index].SetProperties(amount, doneness, duration);
        _orders[index].StartAnim();
    }

    void CheckIncreaseNumOrders()
    {
        int newOrderAmount = GameManager.Instance.GetOrderAmount();
        if (_currentOrderAmounts < newOrderAmount)
        {
            _currentOrderAmounts = newOrderAmount;
            _orders[_currentOrderAmounts - 1].Disable = false;
        }

        if (_currentOrderAmounts == _orders.Count)
            CancelInvoke("CheckIncreaseNumOrders");
    }

    public void FinishOrder(GUIOrderControllerElement gUIOrderControllerElement)
    {
        int index = -1;
        for (int i = 0; index < 0 && i < _orders.Count; ++i)
        {
            if (_orders[i].GetInstanceID() == gUIOrderControllerElement.GetInstanceID())
                index = i;
        }

        StartCoroutine(DelayStartOrderCo(index));
    }
}
