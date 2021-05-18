using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateFormater : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputField;

    private string lastText = "";
    int prevLength;
    private void Awake()
    {
        
        _inputField = GetComponent<TMPro.TMP_InputField>();
        //_inputField.text = "DD/MM/AAAA";
        _inputField.onValueChanged.AddListener(OnValueChanged);
        _inputField.onValueChanged.AddListener(OnMoveCaret);
    }

    private char Format(string text, int index, char addedChar)
    {
      

        if (char.IsNumber(addedChar))
        {
            Debug.Log("AAAAAAAA");

            if (index == 2 || index == 5)
            {
                _inputField.selectionAnchorPosition = index + 1;
                _inputField.selectionFocusPosition = index + 2;
            }
            else
            {
                _inputField.selectionAnchorPosition = _inputField.caretPosition;
                _inputField.selectionFocusPosition = _inputField.caretPosition + 1;
            }

            return addedChar;

        }

        return text[index];
    }

    public void OnMoveCaret(string str)
    {
        
    }

    public void OnValueChanged(string str)
    {
        print("String:" + str);

        //_inputField.DeactivateInputField();

        if (str.Length > 0)
        {
            _inputField.onValueChanged.RemoveAllListeners();
            if (!char.IsDigit(str[str.Length - 1]) && str[str.Length - 1] != '/')
            { // Remove Letters
                _inputField.text = str.Remove(str.Length - 1);
                //_inputField.caretPosition = _inputField.text.Length;
                _inputField.MoveTextEnd(false);


            }
            else if (str.Length == 2 || str.Length == 5)
            {
                if (str.Length < prevLength)
                { // Delete
                    _inputField.text = str.Remove(str.Length - 1);
                    //_inputField.caretPosition = _inputField.text.Length;
                    _inputField.MoveTextEnd(false);


                }
                else
                { // Add
                    _inputField.text = str + "/";
                    //_inputField.caretPosition = _inputField.text.Length;
                    _inputField.MoveTextEnd(false);


                }
            }
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }
        //_inputField.enabled = false;
        //_inputField.enabled = true;

        //_inputField.DeactivateInputField();
        //_inputField.ActivateInputField();
        //_inputField.Select();


        prevLength = _inputField.text.Length;
    }




    //public void Format(string text)
    //{
    //    if (text.Length < 1)
    //        return;


    //    string dd, mm, yyyy;

    //    text = InputUtils.OnlyNumbers(text);


    //    try
    //    {
    //        dd = text.Substring(0, 2);
    //    }
    //    catch (Exception e)
    //    {
    //        dd = text.Substring(0, 1);
    //        _inputField.SetTextWithoutNotify(dd);
    //        return;
    //    }

    //    try
    //    {
    //        mm = text.Substring(1, 2);
    //    }
    //    catch (Exception e)
    //    {
    //        mm = text.Substring(1, 1);
    //        _inputField.SetTextWithoutNotify(dd + "/" + mm);
    //        return;

    //    }

    //    try
    //    {
    //        yyyy = text.Substring(3, text.Length - 4);
    //    }
    //    catch (Exception e)
    //    {
    //        _inputField.SetTextWithoutNotify(dd + "/" + mm);
    //        return;
    //    }

    //    _inputField.SetTextWithoutNotify(dd + "/" + mm + "/" + yyyy);

    //}



}
