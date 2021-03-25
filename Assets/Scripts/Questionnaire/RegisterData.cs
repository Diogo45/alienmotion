using System.Collections;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

[CreateAssetMenu()]
public class RegisterData : ScriptableObject
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public string CPF;
    [SerializeField]
    public string BirthDate;
    [SerializeField]
    public string Email;

    [SerializeField]
    public string Password
    {
        get { return Password; }
        set
        {
            using (SHA256 SHA256 = SHA256.Create())
            {
                byte[] hashPassword = SHA256.ComputeHash(Encoding.UTF8.GetBytes(value));

                Debug.Log(BitConverter.ToString(hashPassword));

            }
        }
    }




    public static void PrintByteArray(byte[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log($"{array[i]:X2}");
            if ((i % 4) == 3) Debug.Log(" ");
        }

    }
}
