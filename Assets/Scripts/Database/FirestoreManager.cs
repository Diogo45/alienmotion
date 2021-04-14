using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreManager : Singleton<FirestoreManager>
{

    [SerializeField] private RegisterDataParent _parentData;
    [SerializeField] private RegisterData _teenData;

    //[SerializeField] private RegisterData _loginData;

    public RegisterDataParent _responseParent { get; private set; }
    public RegisterData _responseTeen { get; private set; }


    private string _firebaseURL = @"https://emotionhunters-29694-default-rtdb.firebaseio.com/";

    public void Awake()
    {
        base.Awake();
    }

    public void WriteRegisterData()
    {
        RestClient.Put(_firebaseURL + _parentData.CPF + ".json", _parentData);
    }

    public void GetLoginData(RegisterData _loginData)
    {
        Debug.Log("Getting register data for " + _loginData.CPF);
        RestClient.Get(_firebaseURL + _loginData.CPF + ".json").Then(response =>
        {
            Debug.Log(response.Text);
            try
            {
                var resp = ScriptableObject.CreateInstance<RegisterDataParent>();
                JsonUtility.FromJsonOverwrite(response.Text, resp);
                _responseParent = resp;
                Debug.Log(_responseParent.CPF);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
        });



    }


}
