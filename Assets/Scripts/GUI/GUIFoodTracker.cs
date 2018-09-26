using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIFoodTracker : MonoBehaviour, IBeginDragHandler , IEndDragHandler, IDragHandler
{

    public Transform _espetoParent;
    public EspetoController _espetoPrefab;
    public CanvasGroup _dropArea;

    EspetoController _movingEspeto;

    void CreateEspeto(Vector2 position)
    {
        _movingEspeto = Instantiate<EspetoController>(_espetoPrefab, _espetoParent);
        _movingEspeto.transform.position = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CreateEspeto(eventData.position);
        _dropArea.alpha = 0.2f;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _dropArea.alpha = 0;
        if (eventData.pointerEnter.name == EspetoController.KeyDropArea)
        {
            _movingEspeto.Setted();
        }
        else
        {
            Debug.Log("BadPosition");
            Destroy(_movingEspeto.gameObject);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _movingEspeto.transform.position = eventData.position + _movingEspeto.Offset;
    }

}
