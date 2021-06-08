using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePasswordManager : Singleton<ChangePasswordManager>
{

    public enum ChangePassState
    {
        None, PasswordDoesNotMatch, Successful
    }

    [SerializeField] private RegisterData _loginContainer;
    [SerializeField] private RegisterDataTeen _teenData;
    [SerializeField] private RegisterDataTeen _newTeenData;

    [SerializeField] private TMPro.TMP_InputField _passwordInput;
    [SerializeField] private TMPro.TMP_InputField _newPasswordInput;

    [SerializeField] private GameObject _warningMessage;


    [SerializeField] private TextAsset _passwordMismatch;
    [SerializeField] private TextAsset _passwordChanged;

    public ChangePassState _loginState { get; private set; }


    private void Awake()
    {
        base.Awake();
        _loginState = ChangePassState.None;
        _loginContainer = ScriptableObject.CreateInstance<RegisterData>();
        _newTeenData = ScriptableObject.CreateInstance<RegisterDataTeen>();

        _newPasswordInput.onEndEdit.AddListener(InputNewPassword);
        _passwordInput.onEndEdit.AddListener(InputPassword);
    }

    private void Reset()
    {

        _loginState = ChangePassState.None;
        _warningMessage.SetActive(false);
    }

    public void InputPassword(string password)
    {
        _loginContainer.Password = password;
    }

    public void InputNewPassword(string password)
    {
        _newTeenData.Password = password;
    }
    
    public void Change()
    {
        StartCoroutine(ChangePassword());
    }

    public IEnumerator ChangePassword()
    {
        Reset();

        _loginContainer.CPF = PlayerPrefs.GetString("TeenCPF");

        FirestoreManager.instance.GetData<RegisterDataTeen>(_loginContainer.CPF);

        yield return new WaitWhile(() => FirestoreManager.instance._response == null);

        _teenData = FirestoreManager.instance._response as RegisterDataTeen;

        //Debug.Log(_teenData.CPF);

        if (!_teenData || _teenData.CPF == FirestoreManager.instance._errorData.CPF)
        {
            Debug.LogError("CHANGE PASSWORD -> The " + _loginContainer.CPF + " does not exist on the database");
            //_loginState = LoginState.MissingFromDataBase;
            //_warningMessage.SetActive(true);
            //_warningMessage.GetComponent<TMPro.TMP_Text>().text = _notRegistered.Text;
            yield break;

        }

        if (_teenData.Password == _loginContainer.Password)
        {

            _loginState = ChangePassState.Successful;

            _newTeenData.CPF = _teenData.CPF;
            _newTeenData.BirthDate = _teenData.BirthDate;
            //_newTeenData.Date = _teenData.Date;
            _newTeenData.ECTAnswers01 = _teenData.ECTAnswers01;
            _newTeenData.ECTAnswers02 = _teenData.ECTAnswers02;
            _newTeenData.ECTAnswers03 = _teenData.ECTAnswers03;
            _newTeenData.EDAEAnswers01 = _teenData.EDAEAnswers01;
            _newTeenData.EDAEAnswers02 = _teenData.EDAEAnswers02;
            _newTeenData.EDAEAnswers03 = _teenData.EDAEAnswers03;
            _newTeenData.Email = _teenData.Email;
            _newTeenData.ParentCPF = _teenData.ParentCPF;
            _newTeenData.ParentEmail = _teenData.ParentEmail;
            _newTeenData.RMETAnswers01 = _teenData.RMETAnswers01;
            _newTeenData.RMETAnswers02 = _teenData.RMETAnswers02;
            _newTeenData.RMETAnswers03 = _teenData.RMETAnswers03;
            _newTeenData.Week = _teenData.Week;
            _newTeenData._week = _teenData._week;

            _passwordInput.text = "";
            _newPasswordInput.text = "";

            FirestoreManager.instance.WriteTeenUpdate(_newTeenData);
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _passwordChanged.Text;
        }
        else
        {
            //Debug.LogError("CHANGE PASSWORD -> The " + _newTeenData.Password + " " + _teenData.Password);
            _loginState = ChangePassState.PasswordDoesNotMatch;
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _passwordMismatch.Text;

        }
    }


}
