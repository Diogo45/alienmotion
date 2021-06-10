using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataBaseState
{
    None, Ok, AlreadyExists, WrongType
}

public class FirestoreManager : Singleton<FirestoreManager>
{

    [SerializeField] private RegisterDataParent _parentData;
    [SerializeField] private RegisterDataTeen _teenData;


    [SerializeField] private SubquestionnaireManager _parentQuestions;

    public RegisterData _response { get; private set; }
    [field: SerializeField] public RegisterData _errorData { get; private set; }
    public DataBaseState _state { get; private set; }


    private string _firebaseURL = @"https://emotionhunterscontrol-default-rtdb.firebaseio.com/";


    private int _postTrials = 0;

    public void Awake()
    {
        base.Awake();
        _state = DataBaseState.None;
    }


    public void WriteTeenUpdate(RegisterDataTeen data)
    {

        if (InputUtils.IsOnlyNumbers(data.CPF) && data.CPF != "")
        {
            RestClient.Put(_firebaseURL + data.CPF.ToString() + ".json", data).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = data.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(data);

            });

            StartCoroutine(CheckPostTeen(data, WriteTeenUpdate));

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
            RestClient.Put(_firebaseURL + data.CPF.ToString() + ".json", data).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = data.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(data);

            });

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
            if (InputUtils.IsOnlyNumbers(_teenData.CPF) && _teenData.CPF != "")
            {
                RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData).Catch(response =>
                {

                    Debug.LogError(response);

                    Message msg = ScriptableObject.CreateInstance<Message>();

                    msg.Id = _teenData.CPF.ToString() + "ERROR";

                    msg.httpResponse = response.ToString();

                    msg.message = JsonUtility.ToJson(_teenData);

                });

                //StartCoroutine(CheckPostTeen(_teenData, WriteRegisterTeenData));

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
                RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData).Catch(response =>
                {

                    Debug.LogError(response);

                    Message msg = ScriptableObject.CreateInstance<Message>();

                    msg.Id = _teenData.CPF.ToString() + "ERROR";

                    msg.httpResponse = response.ToString();

                    msg.message = JsonUtility.ToJson(_teenData);

                });
            }
            else
            {
                Debug.LogError("CPF EMPTY ON WRITE");

            }



        }
    }

    public void WriteGameTeenData(RegisterDataTeen _data)
    {
        if (InputUtils.IsOnlyNumbers(_data.CPF) && _data.CPF != "")
        {
            RestClient.Put(_firebaseURL + _data.CPF.ToString() + ".json", _data).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = _data.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(_data);

            });

            StartCoroutine(CheckPostTeen(_data, WriteGameTeenData));
        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");
        }
    }

    public IEnumerator CheckPostTeen(RegisterDataTeen _data, Action<RegisterDataTeen> onDiff)
    {
        yield return new WaitForSeconds(1);

        GetData<RegisterDataTeen>(_data.CPF);

        yield return new WaitWhile(() => _response == null);

        RegisterDataTeen _databaseTeen = _response as RegisterDataTeen;

        if (_response.CPF != _errorData.CPF)
        {

            if(_data.Different(_databaseTeen))
            {
                if (_postTrials < 10)
                {
                    Debug.LogError(_postTrials + " IS DIFFERENT, TRY POST AGAIN");

                    _postTrials++;

                    onDiff.Invoke(_data);
                }
                else
                {
                    _postTrials = 0;
                    Debug.LogError("ERROR IN GET, RUN OUT OF TRIALS");
                }
            }
            else
            {
                Debug.LogError(_postTrials + " IS EQUAL, FINE");
            }
        }
        else
        {

            if(_postTrials < 10)
            {
                _postTrials++;
                Debug.LogError("ERROR IN GET, IS NOT ON DATABASE OR WRONG TYPE, TRY AGAIN");

                yield return new WaitForSeconds(1);

                StartCoroutine(CheckPostTeen(_data, onDiff));
            }
            else
            {
                _postTrials = 0;
                Debug.LogError("ERROR IN GET, RUN OUT OF TRIALS");
            }

        } 



    }


    public IEnumerator CheckRegisterParentData()
    {
        _state = DataBaseState.None;

        GetData<RegisterData>(_parentData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF != _errorData.CPF)
        {

            _state = DataBaseState.AlreadyExists;

            yield break;
        }
        else
        {
            _state = DataBaseState.Ok;

        }


    }

    public IEnumerator CheckRegisterTeenData()
    {
        _state = DataBaseState.None;

        GetData<RegisterDataTeen>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF != _errorData.CPF)
        {

            try
            {
                if (((RegisterDataTeen)_response).ParentCPF != "" && _response.Password != "")
                {
                    _state = DataBaseState.AlreadyExists;
                }
                else
                {
                    _state = DataBaseState.Ok;

                    yield break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);

                _state = DataBaseState.WrongType;

                yield break;
            }


        }

        _state = DataBaseState.Ok;


    }

    public IEnumerator WriteRegisterParentData()
    {


        _parentQuestions.WriteAnswers();

        if (InputUtils.IsOnlyNumbers(_parentData.CPF) && _parentData.CPF != "")
        {

            RestClient.Put(_firebaseURL + _parentData.CPF.ToString() + ".json", _parentData).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = _parentData.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(_parentData);

            });
        }
        else
        {
            Debug.LogError("CPF EMPTY ON WRITE");

        }

        GetData<RegisterDataTeen>(_teenData.CPF);

        yield return new WaitWhile(() => _response == null);

        if (_response.CPF == _errorData.CPF)
        {
            RestClient.Put(_firebaseURL + _teenData.CPF.ToString() + ".json", _teenData).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = _teenData.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(_teenData);

            });



        }
        else
        {

            ((RegisterDataTeen)_response).ParentCPF = _parentData.CPF;
            ((RegisterDataTeen)_response).ParentEmail = _parentData.Email;


            RestClient.Put(_firebaseURL + _response.CPF.ToString() + ".json", _response).Catch(response =>
            {

                Debug.LogError(response);

                Message msg = ScriptableObject.CreateInstance<Message>();

                msg.Id = _response.CPF.ToString() + "ERROR";

                msg.httpResponse = response.ToString();

                msg.message = JsonUtility.ToJson(_response);

            });

        }



    }

    public void GetData<T>(string cpf) where T : RegisterData
    {
        _response = null;
        //Debug.Log("Getting register data for " + cpf);

        if (cpf == "" || cpf == null)
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
