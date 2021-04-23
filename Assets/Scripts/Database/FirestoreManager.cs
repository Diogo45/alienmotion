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

    
    public IEnumerator WriteRegisterTeenData()
    {

        GetData<RegisterDataTeen>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            RestClient.Put(_firebaseURL + _teenData.CPF + ".json", _teenData);

        }
        else
        {
            _teenData.ParentCPF = ((RegisterDataTeen)_response).ParentCPF;
            _teenData.ParentEmail = ((RegisterDataTeen)_response).ParentEmail;
            RestClient.Put(_firebaseURL + _teenData.CPF + ".json", _teenData);

        }


    }

    public IEnumerator WriteRegisterParentData()
    {

        _parentQuestions.WriteAnswers();

        RestClient.Put(_firebaseURL + _parentData.CPF + ".json", _parentData);

        GetData<RegisterData>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            RestClient.Put(_firebaseURL + _teenData.CPF + ".json", _teenData);

        }
        else
        {
            _teenData.Password = _response.Password;
            RestClient.Put(_firebaseURL + _teenData.CPF + ".json", _teenData);

        }



    }

    public void GetData<T>(string cpf) where T : RegisterData
    {
        _response = null;

        Debug.Log("Getting register data for " + cpf);
        RestClient.Get(_firebaseURL + cpf + ".json").Then(response =>
        {

            //Debug.Log(response.Text);
            try
            {
                T resp = ScriptableObject.CreateInstance<T>();
                JsonUtility.FromJsonOverwrite(response.Text, resp);
                _response = resp;
                Debug.Log(_response.CPF);
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
