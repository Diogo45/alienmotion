using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResgisterParentManager : RegisterManager
{
    [SerializeField] private TMPro.TMP_InputField _teenCpfInput;

    [SerializeField] private TMPro.TMP_InputField _teenBirthDateInput;

    [SerializeField] private TMPro.TMP_InputField _teenEmailInput;

    //[SerializeField] private RegisterDataParent _registerDataParent;

    //  128             64          32      16          8           4       2      1
    // [ emailTeen  birthDayTeen  cpfTeen password confirmPassword email birthDate CPF].sum = 255

    private void Awake()
    {
        base.Awake();

        _teenCpfInput.onEndEdit.AddListener(TeenInputCPF);
        _teenBirthDateInput.onEndEdit.AddListener(TeenInputBirthDate);
        _teenEmailInput.onEndEdit.AddListener(TeenInputEmail);


    }

    private void TeenInputCPF(string arg0)
    {
        ((RegisterDataParent)_registerData).TeenCPF = arg0;
        _fieldsFilled |= 32;
    }

    private void TeenInputBirthDate(string arg0)
    {
        ((RegisterDataParent)_registerData).TeenBirthDate = arg0;
        _fieldsFilled |= 64;

    }

    private void TeenInputEmail(string arg0)
    {
        ((RegisterDataParent)_registerData).TeenEmail = arg0;
        _fieldsFilled |= 128;

    }

    private void Update()
    {
        base.Update();
    }


}
