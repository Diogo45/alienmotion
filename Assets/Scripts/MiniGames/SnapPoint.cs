using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnapPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public UnityEvent<int> OnDropSprite;

    [SerializeField]
    private int _id;

    private bool _mouseOver = false;


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("MOUSE FALSE");

        _mouseOver = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("MOUSE OVER");
        _mouseOver = true;
    }

    private void Update()
    {

        if (!_mouseOver) return;


        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("1");

            RectTransform invPanel = transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
            {
                OnDropSprite.Invoke(_id);
            }
        }

        
    }
}