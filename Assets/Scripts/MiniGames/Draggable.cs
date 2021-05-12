using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 _startPosition;

    public UnityEvent<int> OnSpriteDrag;

    [SerializeField] public GridLayoutGroup _layout;

    [SerializeField]
    private int _id;

    void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        _layout.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = _startPosition;
        GetComponent<Image>().raycastTarget = true;
        _layout.enabled = true;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Sprite sprite = GetComponent<Image>().sprite;
        GetComponent<Image>().raycastTarget = false;
        OnSpriteDrag.Invoke(_id);
    }
}
