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

        if (InputUtils.IsOnlyNumbers(data.CPF) && data.CPF != "")
        {
            RestClient.Put(_firebaseURL + data.CPF.ToString() + ".json", data);

        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");
        }

    }

    public void WriteUpdate(RegisterData data)
    {

        if (InputUtils.IsOnlyNumbers(data.CPF) && data.CPF != "")
        {
            RestClient.Put(_firebaseURL + data.CPF.ToString() + ".json", data);

        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");
        }

    }

    public IEnumerator WriteRegisterTeenData()
    {
        //Debug.Log(_teenData.Password);

       


        GetData<RegisterDataTeen>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            if(InputUtils.IsOnlyNumbers(_teenData.CPF) && _teenData.CPF != "")
            {
                RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);
            }
            else
            {
                Debug.LogError("CPF EMPTY ON WRITE");

            }


        }
        else
        {
            _teenData.ParentCPF = ((RegisterDataTeen)_response).ParentCPF;
            _teenData.ParentEmail = ((RegisterDataTeen)_response).ParentEmail;

            if (InputUtils.IsOnlyNumbers(_teenData.CPF) && _teenData.CPF != "")
            {
                RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData);
            }
            else
            {
                Debug.LogError("CPF EMPTY ON WRITE");

            }

            

        }
    }

    public void WriteGameTeenData(RegisterDataTeen _data)
    {
        if(InputUtils.IsOnlyNumbers(_data.CPF) && _data.CPF != "")
        {
            RestClient.Put(_firebaseURL + _data.CPF.ToString() + ".json", _data);

        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");
        }
    }


    public IEnumerator WriteRegisterParentData()
    {

        _parentQuestions.WriteAnswers();

        if (InputUtils.IsOnlyNumbers(_parentData.CPF) && _parentData.CPF != "")
        {

            RestClient.Put(_firebaseURL + _parentData.CPF.ToString() + ".json", _parentData);
        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");

        }


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

    


}
