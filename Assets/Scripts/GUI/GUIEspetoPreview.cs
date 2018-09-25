using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIEspetoPreview : MonoBehaviour, IBeginDragHandler , IEndDragHandler, IDragHandler
{

    public Transform _espetoParent;
    public EspetoController _espetoPrefab;

    EspetoController _movingEspeto;

    void CreateEspeto(Vector2 position)
    {
        _movingEspeto = Instantiate<EspetoController>(_espetoPrefab, _espetoParent);
        _movingEspeto.transform.position = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CreateEspeto(eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter.name == "Boat")
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
        _movingEspeto.transform.position = eventData.position;
    }

}
