using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Answer
{
    public string file;
    public string emotion;
}

public class EmotionHuntersController : Singleton<EmotionHuntersController>
{
    public int Week { get; private set; }

    private RegisterDataTeen _teenData;
    private bool _receivedTeenData = false;

    [SerializeField] private GameObject _emotionCategorizationTask;

    private void Awake()
    {
        base.Awake();

        StartCoroutine(GetTeenData());
    }

    private IEnumerator GetTeenData()
    {
        FirestoreManager.instance.GetData<RegisterDataTeen>(PlayerPrefs.GetString("TeenCPF"));
        yield return new WaitWhile(() => FirestoreManager.instance._response == null);
        _teenData =  FirestoreManager.instance._response as RegisterDataTeen;
        _receivedTeenData = true;
    }

    private IEnumerator ECT()
    {
        StartCoroutine(GetTeenData());
        yield return new WaitWhile(() => _teenData == null);

        _emotionCategorizationTask.SetActive(true);

    }

    public void EmotionCategorizationTask()
    {
        StartCoroutine(ECT());
    }


}
