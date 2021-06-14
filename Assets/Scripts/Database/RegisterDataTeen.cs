using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu, System.Serializable]
public class RegisterDataTeen : RegisterData
{
    public string ParentCPF;
    public string ParentEmail;


    //public DateTime Week = DateTime.MinValue;

    [SerializeField]
    public string _week;

    public string _coleta01;
    public string _coleta02;
    public string _coleta03;

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
            switch (Week)
            {
                case 1:
                    _coleta01 = value.ToString();
                    break;
                case 2:
                    _coleta02 = value.ToString();
                    break;
                case 3:
                    _coleta03 = value.ToString();
                    break;

            }


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


    public bool Different(RegisterDataTeen compare)
    {

        if (CPF != compare.CPF)
        {
            Debug.LogError(CPF + " " + compare.CPF);
            return true;
        }

        if (BirthDate != compare.BirthDate)
        {
            Debug.LogError(BirthDate + " " + compare.BirthDate);

            return true;
        }

        if (Email != compare.Email)
        {
            Debug.LogError(Email + " " + compare.Email);

            return true;
        }

        if (_password != compare._password)
        {
            Debug.LogError(_password + " " + compare._password);

            return true;
        }

        if (ParentCPF != compare.ParentCPF)
        {
            Debug.LogError(ParentCPF + " " + compare.ParentCPF);

            return true;
        }

        if (ParentEmail != compare.ParentEmail)
        {
            Debug.LogError(ParentEmail + " " + compare.ParentEmail);

            return true;
        }

        if (_week != compare._week)
        {
            Debug.LogError(_week + " " + compare._week);

            return true;
        }

        if (_coleta01 != compare._coleta01)
        {
            Debug.LogError(_coleta01 + " " + compare._coleta01);

            return true;
        }

        if (_coleta02 != compare._coleta02)
        {
            Debug.LogError(_coleta02 + " " + compare._coleta02);

            return true;
        }

        if (_coleta03 != compare._coleta03)
        {
            Debug.LogError(_coleta03 + " " + compare._coleta03);

            return true;
        }

        if (Week != compare.Week)
        {
            Debug.LogError(Week + " " + compare.Week);

            return true;
        }

        if (RMETAnswers01.Except(compare.RMETAnswers01).ToList().Count > 0)
        {
            Debug.LogError(RMETAnswers01.Except(compare.RMETAnswers01).ToList()[0].file);
            return true;

        }
        if (RMETAnswers02.Except(compare.RMETAnswers02).ToList().Count > 0)
        {
            Debug.LogError(RMETAnswers02.Except(compare.RMETAnswers02).ToList()[0].file);
            return true;

        }
        if (RMETAnswers03.Except(compare.RMETAnswers03).ToList().Count > 0)
        {
            Debug.LogError(RMETAnswers03.Except(compare.RMETAnswers03).ToList()[0].file);
            return true;
        }

        if (ECTAnswers01.Except(compare.ECTAnswers01).ToList().Count > 0)
        {
            Debug.LogError(ECTAnswers01.Except(compare.ECTAnswers01).ToList()[0].file);

            return true;
        }

        if (ECTAnswers02.Except(compare.ECTAnswers02).ToList().Count > 0)
        {
            Debug.LogError(ECTAnswers02.Except(compare.ECTAnswers02).ToList()[0].file);

            return true;
        }

        if (ECTAnswers03.Except(compare.ECTAnswers03).ToList().Count > 0)
        {
            Debug.LogError(ECTAnswers03.Except(compare.ECTAnswers03).ToList()[0].file);

            return true;
        }

        if (EDAEAnswers01.Except(compare.EDAEAnswers01).ToList().Count > 0)
        {
            Debug.LogError(EDAEAnswers01.Except(compare.EDAEAnswers01).ToList()[0].file);


            return true;
        }

        if (EDAEAnswers02.Except(compare.EDAEAnswers02).ToList().Count > 0)
        {

            Debug.LogError(EDAEAnswers02.Except(compare.EDAEAnswers02).ToList()[0].file);

            return true;
        }

        if (EDAEAnswers03.Except(compare.EDAEAnswers03).ToList().Count > 0)
        {
            Debug.LogError(EDAEAnswers03.Except(compare.EDAEAnswers03).ToList()[0].file);

            return true;
        }

        return false;
    }

    public void ResetTeen()
    {
        Reset();
        ParentCPF = null;
        ParentEmail = null;
    }

}
