﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu, System.Serializable]
public class RegisterDataTeen : RegisterData
{
    public string ParentCPF;
    public string ParentEmail;

    public void ResetTeen()
    {
        Reset();

        ParentCPF = "";
        ParentEmail = "";
    }

}
