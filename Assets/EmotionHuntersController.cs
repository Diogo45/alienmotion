using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct Answer
{
    public string file;
    public string emotion;
}

public class EmotionHuntersController : Singleton<EmotionHuntersController>
{
    public int Week { get; private set; }

    [SerializeField]
    private RegisterDataTeen _teenData;
    private bool _receivedTeenData = false;

    [SerializeField] private GameObject _emotionCategorizationTask;
    [SerializeField] private GameObject _RMETask;
    [SerializeField] private GameObject _EDAETask;

    private void Awake()
    {
        
        base.Awake();

        //_emotionCategorizationTask.SetActive(false);
        //_RMETask.SetActive(false);
        //_EDAETask.SetActive(false);

        Week = 1;

        Debug.Log("WEEK IS HARDCODED TO ONE");

        // StartCoroutine(GetTeenData());
    }


    public void ToECT()
    {
        _emotionCategorizationTask.SetActive(true);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);
    }

    public void ToRMET()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(true);
        _EDAETask.SetActive(false);
    }

    public void ToEDAE()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(true);
    }


    public void OnEndAllTasks()
    {
        _teenData.Date = DateTime.Now;
        _teenData.Week += 1;
        FirestoreManager.instance.WriteTeenUpdate(_teenData);
    }

    private IEnumerator GetTeenData()
    {
        FirestoreManager.instance.GetData<RegisterDataTeen>(PlayerPrefs.GetString("TeenCPF"));
        yield return new WaitWhile(() => FirestoreManager.instance._response == null);

        if(FirestoreManager.instance._response == FirestoreManager.instance._errorData)
        {
            Debug.LogError("Teen not in DATABASE");
        }

        _teenData =  FirestoreManager.instance._response as RegisterDataTeen;
        _receivedTeenData = true;
    }

    //private IEnumerator ECT()
    //{
    //    StartCoroutine(GetTeenData());
    //    yield return new WaitWhile(() => _teenData == null);

    //    _emotionCategorizationTask.SetActive(true);

    //}

    //public void EmotionCategorizationTask()
    //{
    //    StartCoroutine(ECT());
    //}


}
