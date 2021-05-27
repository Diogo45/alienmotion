using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResgisterParentManager : RegisterManager
{
    [SerializeField] private TMPro.TMP_InputField _teenCpfInput;

    [SerializeField] private TMPro.TMP_InputField _teenBirthDateInput;

    [SerializeField] private TMPro.TMP_InputField _teenEmailInput;

    [SerializeField] private RegisterDataTeen _teenData;

    private int _teenListIndex = 0;

    //[SerializeField] private RegisterDataParent _registerDataParent;

    //  128             64          32      16          8           4       2      1
    // [ emailTeen  birthDayTeen  cpfTeen password confirmPassword email birthDate CPF].sum = 255

    private void Awake()
    {
        base.Awake();
        _teenData.ResetTeen();
        ((RegisterDataParent)_registerData).ResetParent();
        _teenCpfInput.onEndEdit.AddListener(TeenInputCPF);
        _teenBirthDateInput.onEndEdit.AddListener(TeenInputBirthDate);
        _teenEmailInput.onEndEdit.AddListener(TeenInputEmail);

    }

    public override void InputEmail(string email)
    {
        if (email == _teenData.Email)
        {
            _emailInput.text = "";
            _emailInput.placeholder.GetComponent<TMPro.TMP_Text>().text = "Os e-mails do adolescente e do responsável não podem ser iguais!";
            _emailInput.placeholder.GetComponent<TMPro.TMP_Text>().fontSize = 30;
            return;
        }



        base.InputEmail(email);
    }


    private void TeenInputCPF(string cpf)
    {
        if (InputUtils.IsOnlyNumbers(cpf))
        {
            ((RegisterDataParent)_registerData).TeenCPF.Add(cpf);

            _teenData.ParentCPF = ((RegisterDataParent)_registerData).CPF;
            _teenData.CPF = ((RegisterDataParent)_registerData).TeenCPF[_teenListIndex];

            _fieldsFilled |= 32;
        }
        else
        {
            cpf = InputUtils.OnlyNumbers(cpf);

            if (cpf != "")
            {
                ((RegisterDataParent)_registerData).TeenCPF.Add(cpf);

                _teenData.ParentCPF = ((RegisterDataParent)_registerData).CPF;
                _teenData.CPF = ((RegisterDataParent)_registerData).TeenCPF[_teenListIndex];

                _fieldsFilled |= 32;
            }
            else
            {
                Debug.LogError("Bleh");
            }

        }


    }

    private void TeenInputBirthDate(string arg0)
    {
        ((RegisterDataParent)_registerData).TeenBirthDate.Add(arg0);
        _teenData.BirthDate = ((RegisterDataParent)_registerData).TeenBirthDate[_teenListIndex];
        _fieldsFilled |= 64;

    }

    private void TeenInputEmail(string arg0)
    {
        if (arg0 == _registerData.Email)
        {
            _teenEmailInput.text = "";
            _teenEmailInput.placeholder.GetComponent<TMPro.TMP_Text>().text = "Os e-mails do adolescente e do responsável não podem ser iguais!";
            _teenEmailInput.placeholder.GetComponent<TMPro.TMP_Text>().fontSize = 30;
            return;
        }

        ((RegisterDataParent)_registerData).TeenEmail.Add(arg0);
        //_teenData.Email = ((RegisterDataParent)_registerData).TeenEmail[_teenListIndex];

        _teenData.ParentEmail = ((RegisterDataParent)_registerData).Email;

        _fieldsFilled |= 128;

    }

    private void Update()
    {
        base.Update();
    }


}
