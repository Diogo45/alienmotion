using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu, System.Serializable]
public class RegisterDataTeen : RegisterData
{
    public string ParentCPF;
    public string ParentEmail;

    
    //public DateTime Week = DateTime.MinValue;

    [SerializeField]
    private string _week = DateTime.MinValue.ToBinary().ToString();
    public DateTime Date
    {
        get
        {
            long temp = Convert.ToInt64(_week);
            DateTime time = DateTime.FromBinary(temp);
            return time;
        }
        set
        {
            _week = value.ToBinary().ToString();
        }

    }

    public int Week;

    public List<Answer> RMETAnswers01;
    public List<Answer> RMETAnswers02;
    public List<Answer> RMETAnswers03;

    public List<Answer> ECTAnswers01;
    public List<Answer> ECTAnswers02;
    public List<Answer> ECTAnswers03;

    public List<Answer> EDAEAnswers01;
    public List<Answer> EDAEAnswers02;
    public List<Answer> EDAEAnswers03;


    public void ResetTeen()
    {
        Reset();
        ParentCPF = "";
        ParentEmail = "";
    }

}
