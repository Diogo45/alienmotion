using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{

    public enum LoginState
    {
        None, PasswordDoesNotMatch, MissingFromDataBase, NotAuthorized, WeekLimit , Successful
    }

    [SerializeField] private RegisterData _loginContainer;
    [SerializeField] private RegisterDataTeen _teenData;
    [SerializeField] private RegisterDataParent _parentData;

    [SerializeField] private TMPro.TMP_InputField _cpfInput;
    [SerializeField] private TMPro.TMP_InputField _passwordInput;

    [SerializeField] private GameObject _warningMessage;

    [SerializeField] private TextAsset _teenNotAuthorized;
    [SerializeField] private TextAsset _passwordMismatch;
    [SerializeField] private TextAsset _notRegistered;
    [SerializeField] private TextAsset _weekLimit;



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
        _warningMessage.SetActive(false);
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

        FirestoreManager.instance.GetData<RegisterDataParent>(_teenData.ParentCPF);

        yield return new WaitWhile(() => FirestoreManager.instance._response == null);

        _parentData = FirestoreManager.instance._response as RegisterDataParent;

        if (_parentData.CPF == FirestoreManager.instance._errorData.CPF)
        {
            _loginState = LoginState.NotAuthorized;
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _teenNotAuthorized.Text;
            yield return null;
        }

        if (_teenData.CPF == FirestoreManager.instance._errorData.CPF)
        {
            //Debug.LogError("The " + _loginContainer.CPF + " does not exist on the database");
            _loginState = LoginState.MissingFromDataBase;
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _notRegistered.Text;
        }
        else
        if (_teenData.Password == _loginContainer.Password)
        {

            if (_teenData.Week == 0)
            {
                _teenData.ECTAnswers01 = new List<Answer>();
                _teenData.ECTAnswers02 = new List<Answer>();
                _teenData.ECTAnswers03 = new List<Answer>();

                _teenData.RMETAnswers01 = new List<Answer>();
                _teenData.RMETAnswers02 = new List<Answer>();
                _teenData.RMETAnswers03 = new List<Answer>();

                _teenData.Week = 1;

                FirestoreManager.instance.WriteGameTeenData(_teenData);

                PlayerPrefs.SetString("TeenCPF", _teenData.CPF);
                _loginState = LoginState.Successful;
            }
            else
            {
                if (System.DateTime.Now.Subtract(_teenData.Date).Days < 7)
                {
                    _warningMessage.SetActive(true);
                    _warningMessage.GetComponent<TMPro.TMP_Text>().text = _weekLimit.Text;
                    _loginState = LoginState.WeekLimit;
                    
                }
                else
                {
                    _loginState = LoginState.Successful;
                    PlayerPrefs.SetString("TeenCPF", _teenData.CPF);

                }
            }

        }
        else
        {
            //Debug.LogError("The passwords don't match");
            _loginState = LoginState.PasswordDoesNotMatch;
            _warningMessage.SetActive(true);
            _warningMessage.GetComponent<TMPro.TMP_Text>().text = _passwordMismatch.Text;

        }



    }



}
