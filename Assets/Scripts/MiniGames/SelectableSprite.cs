using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableSprite : MonoBehaviour
{
    public Image _image;
    private Toggle _toggle;

    [HideInInspector]
    public int Index;
    public bool Selected;
    public int Emotion;

    [field: SerializeField]
    public Color _activeColor { get; private set; }

    private void Awake()
    {
        //_image = GetComponent<Image>(); 
        _toggle = GetComponent<Toggle>();

        _toggle.onValueChanged.AddListener(OnSelect);
    }

    public void OnSelect(bool isOn)
    {
        if (isOn)
        {
            _image.color = _activeColor;
            Selected = true;

        }
        else
        {
            _image.color = Color.white;
            Selected = false;
        }
    }


}
