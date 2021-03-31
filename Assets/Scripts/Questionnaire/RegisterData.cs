using System.Collections;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

[CreateAssetMenu()]
public class RegisterData : ScriptableObject
{
    public string CPF;
    public string BirthDate;
    public string Email;

    [SerializeField]
    private string _password;

    public string Password
    {
        get { return Password; }
        set
        {
            using (SHA256 SHA256 = SHA256.Create())
            {
                byte[] hashPassword = SHA256.ComputeHash(Encoding.UTF8.GetBytes(value));

                _password = BitConverter.ToString(hashPassword);

                //Debug.Log(Password);

                SHA256.Dispose();
            }
        }
    }




    
}
