using System.Text.RegularExpressions;
using UnityEngine;

public class RegisterManager : MonoBehaviour
{


    [SerializeField] private TMPro.TMP_InputField _cpfInput;

    [SerializeField] private TMPro.TMP_InputField _bithDateInput;
        
    [SerializeField] private TMPro.TMP_InputField _emailInput;

    [SerializeField] private TMPro.TMP_InputField _passwordInput;
    [SerializeField] private TMPro.TMP_InputField _confirmPasswordInput;
    [SerializeField] private GameObject _passwordWarning;
    [SerializeField] private TMPro.TMP_Text _passwordWarningText;


    [SerializeField] private RegisterData _registerData;

    [SerializeField] private TextAsset _passwordMismatch;
    [SerializeField] private TextAsset _passwordRequirements;
    [SerializeField] private TextAsset _passwordMismatchAndRequirements;

    private void Awake()
    {
        _cpfInput.onEndEdit.AddListener(InputCPF);
        _bithDateInput.onEndEdit.AddListener(InputBirthDate);
        _emailInput.onEndEdit.AddListener(InputEmail);
        _passwordInput.onEndEdit.AddListener(InputPassword);
        _confirmPasswordInput.onEndEdit.AddListener(InputConfirmationPassword);    
    }


    public void InputCPF(string cpf)
    {
        _registerData.CPF = cpf;
    }

    public void InputBirthDate(string birthDate)
    {
        _registerData.BirthDate = birthDate;
    }

    public void InputEmail(string email)
    {
        _registerData.Email = email;
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
        return (Regex.IsMatch(_passwordInput.text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"));

    }

    public void InputConfirmationPassword(string password)
    {
        if (!DoesPasswordMatch())
            _passwordWarning.gameObject.SetActive(true);
        else
        {
            _registerData.Password = password;
            _passwordWarning.gameObject.SetActive(false);
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
        }

    }

}
