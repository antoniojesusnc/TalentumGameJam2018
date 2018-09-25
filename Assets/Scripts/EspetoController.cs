using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EspetoController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public EDoneness Doneness { get; private set; }

    bool _beingCreated;
    bool _finished;

    Vector3 _originalPosition;

    void Awake()
    {
        _beingCreated = true;
        GetComponent<GraphicRaycaster>().enabled = false;
    }

    void Start()
    {

    }

    public void Setted()
    {
        _beingCreated = false;
        GetComponent<GraphicRaycaster>().enabled = true;
        SetSpeedIncrement();
    }

    void SetSpeedIncrement()
    {

    }

    void PlaceInOrder(OrderController orderController)
    {
        orderController.AddFish(this);
        Destroy(gameObject);
    }

    //////////////////
    /// Drag Listeners Increment
    //////////////////
    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OrderController orderController = eventData.pointerEnter.GetComponent<OrderController>();
        if (eventData.pointerEnter.name == "Boat" ||
             orderController != null)
        {
            if(orderController != null)
            {
                PlaceInOrder(orderController);
            }
            Setted();
        }
        else
        {
            Debug.Log("Espeto BadPosition");
            transform.position = _originalPosition;

        }
    }

   
}
