using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RMETask : MonoBehaviour
{
    [SerializeField] private GameObject _beginScreen;
    [SerializeField] private GameObject _expScreen;
    [SerializeField] private GameObject _trialCompleteScreen;
    [SerializeField] private GameObject _halfPointScreen;
    [SerializeField] private GameObject _endScreen;

    [SerializeField] private Button _nextButton;

    [SerializeField] private Image _imageField;
    private List<Sprite> _imageList;

    [SerializeField] private List<Sprite> _imageList01;
    [SerializeField] private List<Sprite> _imageList02;
    [SerializeField] private List<Sprite> _imageList03;

    [SerializeField] private Answer[] _RMETAnswers;
    private string _answer;

    [field: SerializeField]
    public int _currentImageIndex { get; private set; }


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

    public void StartTrial()
    {
        _expScreen.SetActive(false);
        RunImage();
    }

    public void RunImage()
    {
        _imageField.sprite = _imageList[_currentImageIndex];
        _currentImageIndex++;
    }

    public void Next()
    {
        if(_answer == "")
        {
            _nextButton.interactable = false;
            return;
        }

        _RMETAnswers[_currentImageIndex] = new Answer { file = _imageField.sprite.name, emotion = _answer };


    }


    public void Answer01()
    {
        _answer = "1";
        _nextButton.interactable = true;

    }

    public void Answer02()
    {
        _answer = "2";
        _RMETAnswers[_currentImageIndex] = new Answer { file = _imageField.sprite.name, emotion = _answer };
        _nextButton.interactable = true;

    }

    public void Answer03()
    {
        _answer = "3";
        _RMETAnswers[_currentImageIndex] = new Answer { file = _imageField.sprite.name, emotion = _answer };
        _nextButton.interactable = true;

    }

    public void Answer04()
    {
        _answer = "4";
        _RMETAnswers[_currentImageIndex] = new Answer { file = _imageField.sprite.name, emotion = _answer };
        _nextButton.interactable = true;

    }

}
