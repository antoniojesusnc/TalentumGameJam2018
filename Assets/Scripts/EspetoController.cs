using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EspetoController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public const int MaxSort = 1080;
    public const string KeyDropArea = "DropArea";

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
    GameManager _gameManager;
    EDoneness _newDoneness;

    [Header("Fish Status")]
    [SerializeField]
    float _donenessIncrement;
    [SerializeField]
    float _totalIncrement;



    void Awake()
    {
        _gameManager = GameManager.Instance;

        _beingCreated = true;
        GameManager.Instance.SetAllEspetoRayCast(false);
    }

    void Start()
    {

    }

    public void Setted()
    {
        _beingCreated = false;
        GameManager.Instance.SetAllEspetoRayCast(true);
        SetSpeedIncrement();

        GetComponentInChildren<Canvas>().sortingOrder = MaxSort - (int)transform.position.y;
    }

    void SetSpeedIncrement()
    {
        _donenessIncrement = GameManager.Instance.GetIncrementValue(this);
    }

    bool FinishPlaceInOrder(GUIOrderControllerElement orderController)
    {
        if (orderController.AddEspeto(this))
        {
            _finished = true;
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }

    }

    void FinishThrowToTrash(GUITrashController trash)
    {
        _finished = true;
        trash.AddEspeto(this);
        Destroy(gameObject);

        SFXManager.Instance.PlayCookWasted();
    }

    void FinishWasted()
    {
        Debug.Log("Wasted");
        Destroy(gameObject);
    }

    void Update()
    {
        if (_beingCreated || _finished)
            return;

        _totalIncrement += Time.deltaTime * _donenessIncrement;

        _newDoneness = _gameManager.GetDoneness(_totalIncrement);
        if (_newDoneness != Doneness)
        {
            UpdateDoneness(_newDoneness);
        }
    }

    private void UpdateDoneness(EDoneness newDoneness)
    {
        _doneness = newDoneness;

        SetColor(_gameManager.GetImage(_doneness));

        SFXManager.Instance.PlayCookPlus();

        if (_doneness == EDoneness.Size)
        {
            FinishWasted();
            SFXManager.Instance.PlayCookWasted();
        }
    }

    void SetColor(Color color)
    {
        var allImages = GetComponentsInChildren<Image>();
        for (int i = 0; i < allImages.Length; ++i)
        {
            if (allImages[i].name != "Stick")
            {
                allImages[i].color = color;
            }
        }
    }

    //////////////////
    /// Drag Listeners Increment
    //////////////////
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.Instance.SetAllEspetoRayCast(false);

        _originalPosition = transform.position;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _offset;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GUIOrderControllerElement orderController = null;
        if (eventData.pointerEnter != null)
            orderController = eventData.pointerEnter.GetComponent<GUIOrderControllerElement>();

        GUITrashController trashController = null;
        if (eventData.pointerEnter != null && orderController == null)
            trashController = eventData.pointerEnter.GetComponent<GUITrashController>();

        if (eventData.pointerEnter != null &&
            (eventData.pointerEnter.name == KeyDropArea ||
            trashController != null ||
             orderController != null))
        {
            if (orderController != null)
            {
                if (!FinishPlaceInOrder(orderController))
                    transform.position = _originalPosition;
            }
            else if (trashController != null)
            {
                FinishThrowToTrash(trashController);
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
