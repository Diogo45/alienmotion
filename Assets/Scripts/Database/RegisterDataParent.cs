using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(), System.Serializable]
public class RegisterDataParent : RegisterData
{
    public List<string> TeenCPF = new List<string>();
    public List<string> TeenBirthDate = new List<string>();
    public List<string> TeenEmail = new List<string>();

    public List<string> SocioeconomicAnswers;
    public List<string> SociodemographicAnswers;


    public void ResetParent()
    {
        TeenCPF = new List<string>();
        TeenBirthDate = new List<string>();
        TeenEmail = new List<string>();

        SociodemographicAnswers = new List<string>();
        SocioeconomicAnswers = new List<string>();

    }


}
