using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EspetoController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("GUI Offset")]
    [SerializeField]
    Vector2 _offset;
    public Vector2 Offset
    {
        get
        {
            return _offset;
        }
    }

    [SerializeField]
    EDoneness _doneness;
    public EDoneness Doneness
    {
        get
        {
            return _doneness;
        }
    }

    bool _beingCreated;
    bool _finished;

    Vector3 _originalPosition;


    [Header("Fish Status")]
    [SerializeField]
    float _donenessIncrement;

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
        GetComponent<GraphicRaycaster>().enabled = false;
        _originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _offset;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OrderController orderController = null;
        if (eventData.pointerEnter != null)
            orderController = eventData.pointerEnter.GetComponent<OrderController>();

        if (eventData.pointerEnter.name == "Boat" ||
             orderController != null)
        {
            if (orderController != null)
            {
                PlaceInOrder(orderController);
            }
        }
        else
        {
            Debug.Log("Espeto BadPosition");
            transform.position = _originalPosition;
        }
        Setted();
    }


}
