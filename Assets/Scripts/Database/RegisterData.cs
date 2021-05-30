using System.Collections;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

[CreateAssetMenu(), System.Serializable]
public class RegisterData : ScriptableObject
{
    public string CPF;
    public string BirthDate;
    public string Email;

    [SerializeField]
    protected string _password;

    public string Password
    {
        get { return _password; }
        set
        {
            using (SHA256 SHA256 = SHA256.Create())
            {
               //Debug.Log(value);

                byte[] hashPassword = SHA256.ComputeHash(Encoding.UTF8.GetBytes(value));

                _password = BitConverter.ToString(hashPassword);

                //Debug.Log(_password);

                SHA256.Dispose();
            }
        }
    }

    public void Reset()
    {
        CPF = "";
        BirthDate = "";
        Email = "";

        _password = "";
    }


    
}
