using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValueDropdown : InputValue
{


    public override void UpdateValue(int option)
    {
        Value = _dropdown.options[_dropdown.value].text;

    }

    public override void UpdateValue(string text)
    {
        Value = text;
    }

    public override void Init()
    {
        _dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(UpdateValue);
        Value = _dropdown.options[_dropdown.value].text;
    }

    

}
