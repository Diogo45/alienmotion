using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct Answer
{
    public string file;
    public string emotion;
}

public class EmotionHuntersController : Singleton<EmotionHuntersController>
{

    [SerializeField] private GameObject _player;
    private PlayerMovement _playerMovement;
    private CharacterController _playerCharacterController;

    [SerializeField] private RegisterDataTeen _teenData;

    [SerializeField] private GameObject _emotionCategorizationTask;
    [SerializeField] private GameObject _RMETask;
    [SerializeField] private GameObject _EDAETask;

    [SerializeField] private GameObject _endScreen;


    [SerializeField] private GameObject _miniGameIntervention;


    [SerializeField] private AudioSource _voiceOverAudio;
    [SerializeField] private AudioSource _musicAudio;

    public int Week { get; private set; }
    private bool _receivedTeenData = false;


    private void Awake()
    {

        base.Awake();

        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerCharacterController = _player.GetComponent<CharacterController>();

        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);

        Debug.Log("WEEK IS HARDCODED TO ONE");
        Week = 1;

        if (Week == 1)
        {
            ToECT();
            _playerMovement.enabled = false;
            _playerCharacterController.enabled = false;
        }
        else if (Week == 2)
        {
            // Alfred Mini Games

        }
        else if (Week == 3)
        {

        }





        // StartCoroutine(GetTeenData());
    }


    public void End()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);
        _endScreen.SetActive(true);
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

        if (FirestoreManager.instance._response == FirestoreManager.instance._errorData)
        {
            Debug.LogError("Teen not in DATABASE");
        }

        _teenData = FirestoreManager.instance._response as RegisterDataTeen;
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
