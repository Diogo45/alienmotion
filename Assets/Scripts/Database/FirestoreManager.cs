using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreManager : Singleton<FirestoreManager>
{

    [SerializeField] private RegisterDataParent _parentData;
    [SerializeField] private RegisterDataTeen _teenData;


    [SerializeField] private SubquestionnaireManager _parentQuestions;

    public RegisterData _response { get; private set; }
    [field: SerializeField] public RegisterData _errorData { get; private set; }


    private string _firebaseURL = @"https://emotionhunters-29694-default-rtdb.firebaseio.com/";

    public void Awake()
    {
        base.Awake();
    }

    
    public void WriteTeenUpdate(RegisterDataTeen data)
    {
        RestClient.Put(_firebaseURL + data.CPF.ToString() + ".json", data);
    }

    public IEnumerator WriteRegisterTeenData()
    {
        //Debug.Log(_teenData.Password);

        GetData<RegisterDataTeen>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);

        }
        else
        {
            _teenData.ParentCPF = ((RegisterDataTeen)_response).ParentCPF;
            _teenData.ParentEmail = ((RegisterDataTeen)_response).ParentEmail;
            RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);

        }
    }

    public void WriteGameTeenData(RegisterDataTeen _data)
    {
        RestClient.Put(_firebaseURL + _data.CPF.ToString() + ".json", _data);
    }


    public IEnumerator WriteRegisterParentData()
    {

        _parentQuestions.WriteAnswers();

        RestClient.Put(_firebaseURL + _parentData.CPF.ToString() + ".json", _parentData);

        GetData<RegisterData>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);

        }
        else
        {
            _teenData.Password = _response.Password;
            RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);

        }



    }

    public void GetData<T>(string cpf) where T : RegisterData
    {
        _response = null;
        //Debug.Log("Getting register data for " + cpf);

        if (cpf == "")
        {
            _response = _errorData;
            return;
        }

        //Debug.Log("2Getting register data for " + cpf);

        RestClient.Get(_firebaseURL + cpf + ".json").Then(response =>
        {
            //Debug.Log(response.Text);
            try
            {
                T resp = ScriptableObject.CreateInstance<T>();
                JsonUtility.FromJsonOverwrite(response.Text, resp);
                _response = resp;
                //Debug.Log(_response);
            }
            catch (Exception e)
            {
                _response = _errorData;
            }

        });
    }

    //public void GetLoginData(RegisterData _loginData)
    //{
    //    Debug.Log("Getting register data for " + _loginData.CPF);
    //    RestClient.Get(_firebaseURL + _loginData.CPF + ".json").Then(response =>
    //    {
    //        Debug.Log(response.Text);
    //        try
    //        {
    //            var resp = ScriptableObject.CreateInstance<RegisterDataParent>();
    //            JsonUtility.FromJsonOverwrite(response.Text, resp);
    //            _responseParent = resp;
    //            Debug.Log(_responseParent.CPF);
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogError(e);
    //        }
            
    //    });



    //}


}
