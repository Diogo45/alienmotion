﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResgisterParentManager : RegisterManager
{
    [SerializeField] private TMPro.TMP_InputField _teenCpfInput;

    [SerializeField] private TMPro.TMP_InputField _teenBirthDateInput;

    [SerializeField] private TMPro.TMP_InputField _teenEmailInput;

    [SerializeField] private RegisterData _registerDataTeen;


    private void Awake()
    {
        base.Awake();

        _teenCpfInput.onEndEdit.AddListener(TeenInputCPF);
        _teenBirthDateInput.onEndEdit.AddListener(TeenInputBirthDate);
        _teenEmailInput.onEndEdit.AddListener(TeenInputEmail);


    }

    private void TeenInputCPF(string arg0)
    {
        _registerDataTeen.CPF = arg0;
    }

    private void TeenInputBirthDate(string arg0)
    {
        _registerDataTeen.BirthDate = arg0;
    }

    private void TeenInputEmail(string arg0)
    {
        _registerDataTeen.Email = arg0;
    }

    private void Update()
    {
        base.Update();
    }


}
