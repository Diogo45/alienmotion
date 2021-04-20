using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{

    public enum LoginState
    {
        None, PasswordDoesNotMatch, MissingFromDataBase, Successful
    }

    [SerializeField] private RegisterData _loginContainer;
    [SerializeField] private RegisterDataTeen _teenData;

    [SerializeField] private TMPro.TMP_InputField _cpfInput;
    [SerializeField] private TMPro.TMP_InputField _passwordInput;


    protected byte _fieldsFilled = 0;
    [SerializeField] private byte _fieldsFilledTotal;

    public LoginState _loginState { get; private set; }
    

    private void Awake()
    {
        base.Awake();
        _loginState = LoginState.None;
        _cpfInput.onEndEdit.AddListener(InputCPF);
        _passwordInput.onEndEdit.AddListener(InputPassword);
    }

    private void Reset()
    {
        _loginState = LoginState.None;
    }

    public void InputCPF(string cpf)
    {
        _loginContainer.CPF = cpf;
        _fieldsFilled |= 1;
    }

    public void InputPassword(string password)
    {
        _loginContainer.Password = password;
        _fieldsFilled |= 2;
    }

    

    public IEnumerator Login()
    {
        Reset();

        FirestoreManager.instance.GetData<RegisterDataTeen>(_loginContainer.CPF);

        yield return new WaitWhile(() => FirestoreManager.instance._response == null);

        _teenData = FirestoreManager.instance._response as RegisterDataTeen;

        if (_teenData.CPF == FirestoreManager.instance._errorData.CPF)
        {
            //Debug.LogError("The " + _loginContainer.CPF + " does not exist on the database");
            _loginState = LoginState.MissingFromDataBase;
        }

        if (_teenData.Password == _loginContainer.Password)
        {
            _loginState = LoginState.Successful;
        }
        else
        {
            //Debug.LogError("The passwords don't match");
            _loginState = LoginState.PasswordDoesNotMatch;

        }



    }



}
