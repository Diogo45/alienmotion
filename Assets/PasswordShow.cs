using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordShow : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_InputField _textPasswordField;
    [SerializeField] private Toggle _toggle;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(TogglePassword);
    }


    public void TogglePassword(bool toggle)
    {
        if (toggle)
        {
            _textPasswordField.contentType = TMPro.TMP_InputField.ContentType.Alphanumeric;
            _textPasswordField.enabled = false;
            _textPasswordField.enabled = true;
        }
        else
        {
            _textPasswordField.contentType = TMPro.TMP_InputField.ContentType.Password;
            _textPasswordField.enabled = false;
            _textPasswordField.enabled = true;
        }
    }


}
