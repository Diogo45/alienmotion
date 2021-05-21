using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EDAETask : MonoBehaviour
{
    [SerializeField] private GameObject _beginScreen;
    [SerializeField] private GameObject _questionsScreen;
    [SerializeField] private GameObject _endScreen;


    [SerializeField] private List<string> _questions;
    [SerializeField] private List<GameObject> _questionUI;
    private TMPro.TMP_Dropdown[] _questionDropdown;
    private TMPro.TMP_Text[] _questionText;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _toEndScreenButton;
    [SerializeField] private TMPro.TMP_Text _pageUI;

    [SerializeField] private Answer[] _answers;



    private int _pageIndex = 0;
    [SerializeField] private float _pageCount;

    private int _questionIndex;

    private void Awake()
    {


        _pageCount = _questions.Count / _questionUI.Count;
        _questionDropdown = new TMPro.TMP_Dropdown[_questionUI.Count];
        _questionText = new TMPro.TMP_Text[_questionUI.Count];
        _answers = new Answer[_questions.Count];

        for (int i = 0; i < _answers.Length; i++)
        {
            _answers[i] = new Answer { file = _questions[i], emotion = "0" };
        }


        for (int i = 0; i < _questionUI.Count; i++)
        {
            _questionDropdown[i] = _questionUI[i].GetComponentInChildren<TMPro.TMP_Dropdown>();
            _questionText[i] = _questionUI[i].GetComponentInChildren<TMPro.TMP_Text>();
        }

        for (int i = 0; i < _questionText.Length; i++)
        {
            if (_questionIndex + i > _questions.Count)
            {
                _questionUI[i].SetActive(false);
                continue;

            }

            _questionText[i].text = _questions[_questionIndex + i];
        }


        _pageUI.text = "1/" + Mathf.FloorToInt(_pageCount);
    }

    private void OnDisable()
    {
        //Debug.Log("DISABLED ECT");
        EmotionHuntersController.instance.WriteEADEData(_answers.ToList());
    }


    public void Next()
    {

        Reset();
        
        if (_pageIndex + 1 < _pageCount)
        {
            _pageIndex += 1;
            _questionIndex += 6;
        }
        else
        {
            _pageIndex += 1;
            _questionIndex += 6;
            _nextButton.gameObject.SetActive(false);
            _toEndScreenButton.gameObject.SetActive(true);

        }

        for (int i = 0; i < _questionText.Length; i++)
        {
            // Debug.Log(_questionIndex);
            if (_questionIndex + i > _questions.Count - 1)
            {
                _questionUI[i].SetActive(false);
                continue;

            }

            _questionText[i].text = _questions[_questionIndex + i];
        }


        _pageUI.text = (_pageIndex + 1) + "/" + (Mathf.FloorToInt(_pageCount) + 1);
    }

    private void Reset()
    {

        foreach (var item in _questionDropdown)
        {
            item.value = 0;
        }
        
    }

    public void Begin()
    {
        _beginScreen.SetActive(false);
        _questionsScreen.SetActive(true);
    }

    public void Dropdown01(int value)
    {
        _answers[_questionIndex] = new Answer
        {
            file = _questionText[_questionIndex].text,
            emotion = _questionDropdown[_questionIndex].options[_questionDropdown[_questionIndex].value].text
        };

    }

    public void Dropdown02(int value)
    {
        _answers[_questionIndex + 1] = new Answer
        {
            file = _questionText[_questionIndex + 1].text,
            emotion = _questionDropdown[_questionIndex + 1].options[_questionDropdown[_questionIndex + 1].value].text
        };

    }

    public void Dropdown03(int value)
    {
        _answers[_questionIndex + 2] = new Answer
        {
            file = _questionText[_questionIndex + 2].text,
            emotion = _questionDropdown[_questionIndex + 2].options[_questionDropdown[_questionIndex + 2].value].text
        };

    }

    public void Dropdown04(int value)
    {
        _answers[_questionIndex + 3] = new Answer
        {
            file = _questionText[_questionIndex + 3].text,
            emotion = _questionDropdown[_questionIndex + 3].options[_questionDropdown[_questionIndex + 3].value].text
        };

    }

    public void Dropdown05(int value)
    {
        _answers[_questionIndex + 4] = new Answer
        {
            file = _questionText[_questionIndex + 4].text,
            emotion = _questionDropdown[_questionIndex + 4].options[_questionDropdown[_questionIndex + 4].value].text
        };

    }
    public void Dropdown06(int value)
    {
        _answers[_questionIndex + 5] = new Answer
        {
            file = _questionText[_questionIndex + 5].text,
            emotion = _questionDropdown[_questionIndex + 5].options[_questionDropdown[_questionIndex + 5].value].text
        };

    }


}
