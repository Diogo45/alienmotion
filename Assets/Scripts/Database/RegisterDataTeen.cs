using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu, System.Serializable]
public class RegisterDataTeen : RegisterData
{
    public string ParentCPF;
    public string ParentEmail;

    public int Week;

    public List<Answer> RMETAnswers01;
    public List<Answer> RMETAnswers02;
    public List<Answer> RMETAnswers03;

    public List<Answer> ECTAnswers01;
    public List<Answer> ECTAnswers02;
    public List<Answer> ECTAnswers03;
     
    public void ResetTeen()
    {
        Reset();

        ParentCPF = "";
        ParentEmail = "";
    }

}
