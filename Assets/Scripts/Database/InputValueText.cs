using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValueText : InputValue
{

    

    public override void UpdateValue(int option)
    {
        Value = _inputField.text;

    }

    public override void UpdateValue(string text)
    {
        Value = text;
    }

    public override void Init()
    {
        _inputField = GetComponentInChildren<TMPro.TMP_InputField>();
        _inputField.onValueChanged.AddListener(UpdateValue);
        Value = _inputField.text;
    }

 

}
