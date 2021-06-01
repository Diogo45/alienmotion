using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{


    [SerializeField] protected TMPro.TMP_InputField _cpfInput;

    [SerializeField] protected TMPro.TMP_InputField _bithDateInput;
        
    [SerializeField] protected TMPro.TMP_InputField _emailInput;

    [SerializeField] protected TMPro.TMP_InputField _passwordInput;
    [SerializeField] protected TMPro.TMP_InputField _confirmPasswordInput;
    [SerializeField] protected GameObject _passwordWarning;
    [SerializeField] protected TMPro.TMP_Text _passwordWarningText;


    [SerializeField] protected RegisterData _registerData;

    [SerializeField] private TextAsset _passwordMismatch;
    [SerializeField] private TextAsset _passwordRequirements;
    [SerializeField] private TextAsset _passwordMismatchAndRequirements;


    [SerializeField] private Button _nextButton;

    //  128 64 32   16          8           4       2      1
    // [ 0  0  0 password confirmPassword email birthDate CPF].sum = 31
    protected byte _fieldsFilled = 0;
    [SerializeField]
    private byte _fieldsFilledTotal;

    protected void Awake()
    {
        _registerData.Reset();
        _cpfInput.onEndEdit.AddListener(InputCPF);
        _bithDateInput.onEndEdit.AddListener(InputBirthDate);
        _emailInput.onEndEdit.AddListener(InputEmail);
        _passwordInput.onEndEdit.AddListener(InputPassword);
        _confirmPasswordInput.onEndEdit.AddListener(InputConfirmationPassword);    
    }

    private void OnEnable()
    {
        ((RegisterDataTeen)_registerData).ResetTeen();

        _cpfInput.SetTextWithoutNotify("");
        _bithDateInput.SetTextWithoutNotify("");
        _emailInput.SetTextWithoutNotify("");
        _passwordInput.SetTextWithoutNotify("");
        _confirmPasswordInput.SetTextWithoutNotify("");
        _fieldsFilled = 0;
    }

    protected void Update()
    {
        if(_fieldsFilled == _fieldsFilledTotal) 
        {
            _nextButton.interactable = true;
        }
        else
        {
            _nextButton.interactable = false;
        }
    }


    public void InputCPF(string cpf)
    {

        
        if (InputUtils.IsOnlyNumbers(cpf))
        {
            _registerData.CPF = cpf;
            _fieldsFilled |= 1;
        }
        else
        {
            cpf = InputUtils.OnlyNumbers(cpf);

            if (cpf != "")
            {
                _registerData.CPF = cpf;
                _fieldsFilled |= 1;
            }
            else
            {
                Debug.LogError("CPF empty");
            }

        }
        
    }

    public void InputBirthDate(string birthDate)
    {
        _registerData.BirthDate = birthDate;
        _fieldsFilled |= 2;

    }

    public virtual void InputEmail(string email)
    {
        _registerData.Email = email;
        _fieldsFilled |= 4;

    }

    private bool DoesPasswordMatch ()
    {
        if (_passwordInput.text != _confirmPasswordInput.text)
        {
            return false;
        }

        return true;

    }

    private bool DoesPasswordMeetRequirements()
    {
        //(?=.*[a-z])(?=.*[A-Z])
        return (Regex.IsMatch(_passwordInput.text, @"^(?=.*\d)[a-zA-Z\d]{6,}$"));

    }

    public void InputConfirmationPassword(string password)
    {
        bool mismatch, requirements;

        if (!DoesPasswordMatch())
            mismatch = true;
        else
            mismatch = false;

        if (!DoesPasswordMeetRequirements())
            requirements = true;
        else
            requirements = false;


        if (mismatch || requirements)
        {
            _passwordWarning.SetActive(true);
            if (mismatch && requirements)
            {
                _passwordWarningText.text = _passwordMismatchAndRequirements.Text;

            }
            else if (mismatch)
            {
                _passwordWarningText.text = _passwordMismatch.Text;
            }
            else if (requirements)
            {
                _passwordWarningText.text = _passwordRequirements.Text;
            }
        }
        else
        {
            _passwordWarning.SetActive(false);
            _registerData.Password = password;
            _fieldsFilled |= 24;
        }

    }

    public void InputPassword(string password)
    {
        bool mismatch, requirements;

        if (!DoesPasswordMatch())
            mismatch = true;
        else
            mismatch = false;

        if (!DoesPasswordMeetRequirements())
            requirements = true;
        else
            requirements = false;


        if(mismatch || requirements)
        {
            _passwordWarning.SetActive(true);
            if (mismatch && requirements)
            {
                _passwordWarningText.text = _passwordMismatchAndRequirements.Text;

            }
            else if(mismatch)
            {
                _passwordWarningText.text = _passwordMismatch.Text;
            }
            else if (requirements)
            {
                _passwordWarningText.text = _passwordRequirements.Text;
            }
        }
        else
        {
            _passwordWarning.SetActive(false);
            _registerData.Password = password;

        }

    }

}
