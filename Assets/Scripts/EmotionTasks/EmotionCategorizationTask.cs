using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EmotionCategorizationTask : MonoBehaviour
{
    

    [SerializeField] private ToggleGroup toggleGroup;
    //For some reason unity's toggleGroup.SetAllOff does not work
    //And the list of toggles in the group is always empty on debugging in visual studio
    [SerializeField] private Toggle[] _toggleList;

    [SerializeField] private GameObject _imageShow;
    [SerializeField] private GameObject _crossShow;
    [SerializeField] private GameObject _emotionChoiceShow;

    [SerializeField] private GameObject _beginScreen;
    [SerializeField] private GameObject _expScreen;
    [SerializeField] private GameObject _trialCompleteScreen;
    [SerializeField] private GameObject _halfPointScreen;
    [SerializeField] private GameObject _endScreen;

    [SerializeField] private Button _nextButton;

    private float _showImageForSeconds = 0.2f;
    private float _showCrossForSeconds = 1f;

    [SerializeField] private Image _imageField;
    private List<Sprite> _emotionList;

    [SerializeField] private List<Sprite> _emotionList01;
    [SerializeField] private List<Sprite> _emotionList02;
    [SerializeField] private List<Sprite> _emotionList03;

    [SerializeField] private Answer[] _emotionAnswers;
    private string _emotionAnswer;

    [field: SerializeField]
    public int _currentImageIndex { get; private set; }

    private bool _halfPoint = false;


    private void Awake()
    {
        switch (EmotionHuntersController.instance.Week)
        {
            case 1:
                _emotionList = _emotionList01;
                break;
            case 2:
                _emotionList = _emotionList02;
                break;
            case 3:
                _emotionList = _emotionList03;
                break;
        }


        _emotionList = _emotionList.OrderBy(a => Guid.NewGuid()).ToList();
        _imageField.sprite = _emotionList[0];
        _emotionAnswers = new Answer[_emotionList.Count];
        _emotionAnswer = "";
    }

    


    public void NextTrial()
    {
        if (_emotionAnswer == "")
        {
            _nextButton.interactable = false;
            return;
        }

        _emotionChoiceShow.SetActive(false);

        _trialCompleteScreen.SetActive(true);

        _nextButton.onClick.RemoveAllListeners();
        _nextButton.onClick.AddListener(Next);

    }

    public void StartTest()
    {
        _trialCompleteScreen.SetActive(false);
        StartCoroutine(RunImage());
    }

    public void StartTrial()
    {
        _expScreen.SetActive(false);
        StartCoroutine(RunImage());
    }

    public void RestartTest()
    {
        _halfPointScreen.SetActive(false);
        StartCoroutine(RunImage());
    }



    public void Next()
    {
        if (_currentImageIndex >= _emotionList.Count / 2 && !_halfPoint)
        {
            _showImageForSeconds = 1f;
            _halfPointScreen.SetActive(true);
            _halfPoint = true;
            return;
        }


        if (_emotionAnswer == "")
        {
            _nextButton.interactable = false;
            return;
        }
        else
        {
            _nextButton.interactable = true;
        }

        _emotionAnswers[_currentImageIndex] = new Answer { file = _emotionList[_currentImageIndex].name, emotion = _emotionAnswer };

        if (_currentImageIndex + 1 < _emotionList.Count)
            _currentImageIndex++;
        else
        {
            _endScreen.SetActive(true);
            return;
        }

        _imageField.sprite = _emotionList[_currentImageIndex];

        foreach (var item in _toggleList)
        {
            item.isOn = false;
        }


        _emotionChoiceShow.SetActive(false);
        StartCoroutine(RunImage());
    }

    private IEnumerator RunImage()
    {
        _crossShow.SetActive(true);
        yield return new WaitForSeconds(_showCrossForSeconds);
        _crossShow.SetActive(false);
        _imageShow.SetActive(true);
        yield return new WaitForSeconds(_showImageForSeconds);
        _imageShow.SetActive(false);
        _emotionChoiceShow.SetActive(true);
    }





    public void BegginToExp()
    {
        _beginScreen.SetActive(false);
        _expScreen.SetActive(true);
    }

    public void Anger(bool toggle)
    {
        if (toggle)
        {
            _emotionAnswer = "Raiva";
            _nextButton.interactable = true;
        }
        else
            _emotionAnswer = "";
    }

    public void Fear(bool toggle)
    {

        if (toggle)
        {

            _emotionAnswer = "Medo";
            _nextButton.interactable = true;

        }
        else
            _emotionAnswer = "";
    }
    public void Saddness(bool toggle)
    {


        if (toggle)
        {
            _emotionAnswer = "Tristeza";
            _nextButton.interactable = true;
        }
        else
            _emotionAnswer = "";
    }
    public void Neutral(bool toggle)
    {

        if (toggle)
        {
            _emotionAnswer = "Neutro";
            _nextButton.interactable = true;

        }
        else
            _emotionAnswer = "";
    }

}
