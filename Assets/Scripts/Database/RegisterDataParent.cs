using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(), System.Serializable]
public class RegisterDataParent : RegisterData
{
    public List<string> TeenCPF = new List<string>();
    public List<string> TeenBirthDate = new List<string>();
    public List<string> TeenEmail = new List<string>();

    public List<string> Questions;
    public List<string> Answers;


    public void ResetParent()
    {
        TeenCPF = new List<string>();
        TeenBirthDate = new List<string>();
        TeenEmail = new List<string>();

        Answers = new List<string>();
        Questions = new List<string>();

    }


}
