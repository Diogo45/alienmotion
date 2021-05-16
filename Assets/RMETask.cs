using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RMETask : MonoBehaviour
{
    [SerializeField] private GameObject _beginScreen;
    [SerializeField] private GameObject _imageScreen;
    [SerializeField] private GameObject _trialCorrect;
    [SerializeField] private GameObject _trialIncorrect;
    [SerializeField] private GameObject _endScreen;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _nextTrialButton;

    [SerializeField] private Image _imageField;
    private List<Sprite> _imageList;

    [SerializeField] private List<Sprite> _imageList01;
    [SerializeField] private List<Sprite> _imageList02;
    [SerializeField] private List<Sprite> _imageList03;

    [SerializeField] private Answer[] _RMETAnswers;
    private string _answer;

    [field: SerializeField]
    public int _currentImageIndex { get; private set; }

    private string _trialAnswer = "2";

    private void Awake()
    {
        switch (EmotionHuntersController.instance.Week)
        {
            case 1:
                _imageList = _imageList01;
                break;
            case 2:
                _imageList = _imageList02;
                break;
            case 3:
                _imageList = _imageList03;
                break;
        }


        _imageField.sprite = _imageList[0];
        _RMETAnswers = new Answer[_imageList.Count];
        _answer = "";
    }

    private void OnDisable()
    {
        //Debug.Log("DISABLED ECT");
        EmotionHuntersController.instance.WriteRMETData(_RMETAnswers.ToList());
    }


    public void StartTrial()
    {
        _beginScreen.SetActive(false);
        _imageScreen.SetActive(true);
        RunImageTrial();
    }


    public void NextFromTrial()
    {

        if (_answer == _trialAnswer)
        {
            _imageScreen.SetActive(false);
            _trialCorrect.SetActive(true);

        }
        else
        {
            _imageScreen.SetActive(false);
            _trialIncorrect.SetActive(true);
        }

        _answer = "";


    }


    public void TrialIncorretTrialAgain()
    {
        _trialIncorrect.SetActive(false);
        _imageScreen.SetActive(true);
    }

    public void TrialCorrect()
    {
        _trialCorrect.SetActive(false);
        _imageScreen.SetActive(true);
        _currentImageIndex++;
        
        _nextButton.gameObject.SetActive(true);
        _nextTrialButton.gameObject.SetActive(false);

        RunImage();
    }


    public void RunImageTrial()
    {
        _imageField.sprite = _imageList[0];
    }

    public void StartTest()
    {
        //_beginScreen.SetActive(false);
        _imageScreen.SetActive(true);

        RunImage();
    }


    public void RunImage()
    {
        _imageField.sprite = _imageList[_currentImageIndex];
    }

    public void Next()
    {
        if (_answer == "")
        {
            _nextButton.interactable = false;
            return;
        }

        _RMETAnswers[_currentImageIndex] = new Answer { file = _imageField.sprite.name, emotion = _answer };

        _answer = "";

        if (_currentImageIndex + 1 < _imageList.Count)
            _currentImageIndex++;
        else
        {
            _endScreen.SetActive(true);
            return;
        }

        RunImage();


    }


    public void Answer01()
    {



        _answer = "1";
        _nextButton.interactable = true;



    }

    public void Answer02()
    {
        _answer = "2";
        _nextButton.interactable = true;

    }

    public void Answer03()
    {
        _answer = "3";
        _nextButton.interactable = true;

    }

    public void Answer04()
    {
        _answer = "4";
        _nextButton.interactable = true;

    }

}
