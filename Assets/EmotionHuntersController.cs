using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject _miniGameEndScreen;

    [SerializeField] private GameObject _endScreen01;
    [SerializeField] private GameObject _endScreen02;
    [SerializeField] private GameObject _endScreen03;

    [SerializeField] private GameObject _miniGameIntervention;

    [SerializeField] private AudioSource _musicAudio;

    public int _miniGamesCompleted = 0;

    public int Week { get; private set; }
    private bool _receivedTeenData = false;

    public bool IsIntervention { get; private set; } = false;

    private void Awake()
    {

        base.Awake();

        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerCharacterController = _player.GetComponent<CharacterController>();

        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);

        //Debug.Log("WEEK IS HARDCODED TO ONE");

        StartCoroutine(GetTeenData());


        StartCoroutine(SetWeek());

        
    }


    public void CompleteMiniGame()
    {
        _miniGamesCompleted++;
        Debug.Log(_miniGamesCompleted);
        if (_miniGamesCompleted >= 4)
        {
            ToEndMiniGames();
            DisableMovement();
        }

    }


    public void EnableMovement()
    {
        _playerMovement.enabled = true;
        _playerCharacterController.enabled = true;
    }

    public void DisableMovement()
    {
        _playerMovement.enabled = false;
        _playerCharacterController.enabled = false;
    }

    public void End()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);

        if (Week == 1)
            _endScreen01.SetActive(true);
        else if (Week == 2)
            _endScreen02.SetActive(true);
        else if (Week == 3)
            _endScreen03.SetActive(true);
    }


    public void ToEndMiniGames()
    {
        _miniGameEndScreen.SetActive(true);
    }

    public void MiniGamesEndToTask()
    {
        IsIntervention = true;
        _miniGameEndScreen.SetActive(false);
        ToECT();

    }

    public void ToECT()
    {
        _emotionCategorizationTask.SetActive(true);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(false);
    }

    public void WriteECTData(List<Answer> answers)
    {
        if (Week == 1)
        {
            _teenData.ECTAnswers01 = answers;
        }
        else if (Week == 2)
        {
            _teenData.ECTAnswers02 = answers;
        }
        else
        {
            _teenData.ECTAnswers03 = answers;

        }

        FirestoreManager.instance.WriteGameTeenData(_teenData);
    }



    public void ToRMET()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(true);
        _EDAETask.SetActive(false);
    }

    public void WriteRMETData(List<Answer> answers)
    {
        if (Week == 1)
        {
            _teenData.RMETAnswers01 = answers;
        }
        else if (Week == 2)
        {
            _teenData.RMETAnswers02 = answers;
        }
        else
        {
            _teenData.RMETAnswers03 = answers;

        }

        FirestoreManager.instance.WriteGameTeenData(_teenData);
    }

    public void ToEDAE()
    {
        _emotionCategorizationTask.SetActive(false);
        _RMETask.SetActive(false);
        _EDAETask.SetActive(true);
    }

    public void WriteEADEData(List<Answer> answers)
    {
        if (Week == 1)
        {
            _teenData.EDAEAnswers01 = answers;
        }
        else if (Week == 2)
        {
            _teenData.EDAEAnswers02 = answers;
        }
        else
        {
            _teenData.EDAEAnswers03 = answers;

        }

        FirestoreManager.instance.WriteGameTeenData(_teenData);
    }


    public void OnEndAllTasks()
    {
        _teenData.Date = DateTime.Now;
        _teenData.Week += 1;
        FirestoreManager.instance.WriteTeenUpdate(_teenData);
        SceneManager.LoadScene(0);
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

    private IEnumerator SetWeek()
    {
        yield return new WaitWhile(() => _receivedTeenData == false);

        Week = _teenData.Week;

        if (Week == 1)
        {
            IsIntervention = true;
            ToECT();
            _playerMovement.enabled = false;
            _playerCharacterController.enabled = false;
        }
        else if (Week == 2)
        {
            // Alfred Mini Games
            EmotionHuntersUIController.instance.ToIntroAlfred();

        }
        else if (Week == 3)
        {
            IsIntervention = true;

            ToECT();
            _playerMovement.enabled = false;
            _playerCharacterController.enabled = false;
        }



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
