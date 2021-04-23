using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputValue : MonoBehaviour
{
    public string Value { get; protected set; }
    [field:SerializeField]
    public int QuestionIndex { get; set; }

    [SerializeField] public TMPro.TMP_InputField _inputField { get; protected set; }
    [SerializeField] public TMPro.TMP_Dropdown _dropdown { get; protected set; }

    public abstract void Init();

    public abstract void UpdateValue(int option);
    public abstract void UpdateValue(string text);

}
