using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordManager : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_InputField _cpfInput;
    [SerializeField] private TMPro.TMP_InputField _emailInput;

    [SerializeField] private GameObject _warningMessage;

    [SerializeField] private TextAsset _notRegistered;
    [SerializeField] private TextAsset _wrongEmail;
    [SerializeField] private TextAsset _emailSentMessage;

    [SerializeField] private TextAsset _emailContent;

    [SerializeField] private Button _button;

    private RegisterDataTeen _data;

    private string _cpf;
    private string _email;


    private bool _sent = false;


    private string newPassword;

    // Start is called before the first frame update
    private string GenNewPassword(int length)
    {
        var rnd = new System.Random(System.DateTime.Now.Millisecond);

        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        StringBuilder res = new StringBuilder();

        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }

        return res.ToString();

    }

    private void OnEnable()
    {
        Reset();

        _emailInput.onEndEdit.AddListener(InputEmail);
        _cpfInput.onEndEdit.AddListener(InputCPF);

    }

    public void CheckRequest()
    {
        StartCoroutine(CheckRequestRoutine());

    }

    private IEnumerator CheckRequestRoutine()
    {
        //if (_sent) yield break;

        FirestoreManager.instance.GetData<RegisterDataTeen>(_cpf);

        yield return new WaitWhile(() => FirestoreManager.instance._response == null);

        _data = FirestoreManager.instance._response as RegisterDataTeen;

        if (!_data || _data.CPF == FirestoreManager.instance._errorData.CPF)
        {
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _notRegistered.Text;
            yield break;
        }

        if (_data.Email != _email)
        {
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _wrongEmail.Text;
            yield break;
        }

        //Debug.Log("AAAAAAAAA");

        try
        {
            newPassword = GenNewPassword(6);

            //DateTime now = DateTime.Now;

            SendMail.instance.Send(_email, "Pedido de Nova Senha", (_emailContent.Text + newPassword + "\nAtenciosamente,\nGrupo de Neurociência Afetiva e Transgeracionalidade(GNAT) da PUCRS"));

            //DateTime after = DateTime.Now;

            //Debug.LogWarning((after - now).Milliseconds);
            //_sent = true;

            _data.Password = newPassword;

            FirestoreManager.instance.WriteTeenUpdate(_data);

        }
        catch (Exception e)
        {
            Debug.LogError(e);
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = "E-mail no formato incorreto";
            yield break;
        }

        _warningMessage.SetActive(true);
        _warningMessage.GetComponent<TMPro.TMP_Text>().text = _emailSentMessage.Text;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Questionnaire.QuestionnaireUI.instance.ForgotPassword_Login);


    }

    private void InputCPF(string cpf)
    {
        _cpf = cpf;
    }

    private void InputEmail(string email)
    {
        _email = email;
    }


    private void Reset()
    {
        _sent = false;
        _warningMessage.SetActive(false);

        _cpf = "";
        _email = "";
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(CheckRequest);
    }
}
