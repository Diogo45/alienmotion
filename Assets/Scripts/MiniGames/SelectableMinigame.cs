using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _minigameScreen;
    [SerializeField] private GameObject _rightScreen;
    [SerializeField] private GameObject _wrongOnceScreen;
    [SerializeField] private GameObject _wrongThriceScreen;
    [SerializeField] private GameObject _finalScreen;
    [SerializeField] private TMPro.TMP_Text _descriptionText;

    [SerializeField] private Button _nextButton;

    [SerializeField] private List<SelectableSprite> _selectables;

    [SerializeField] private List<SpriteAnswer> _trial01;
    [SerializeField] private List<SpriteAnswer> _trial02;
    [SerializeField] private List<SpriteAnswer> _trial03;

    private int _errorCount = 0;
    [SerializeField] private UnityEvent OnEndMiniGame;
    private int _currentTrial = 0;
    private int[] _imgsPerTrial = { 4, 6, 6 };
    [SerializeField]
    private string emotion;

    private void Start()
    {
        for (int i = 0; i < _selectables.Count; i++)
        {
            _selectables[i].Index = i;
           
        }

        for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
        {
            _selectables[i]._image.sprite = _trial01[i].Sprite;
            _selectables[i].Emotion = _trial01[i].emotion;
        }

        _nextButton.onClick.RemoveAllListeners();

        _nextButton.onClick.AddListener(Next);

    }

    public bool CheckAnswer()
    {
        var selected = Array.FindAll(_selectables.ToArray(), x => x.Selected == true);

        Debug.Log(_currentTrial);

        if(selected.Length < _imgsPerTrial[_currentTrial] / 2)
        {
            return false;
        }


        foreach (var item in _selectables)
        {
            if (item.Selected)
            {
                if (item.Emotion != 1)
                    return false;
            }
        }

        return true;
    }

    public void Next()
    {
        if (CheckAnswer())
        {
            _rightScreen.SetActive(true);

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Right);

        }
        else
        {
            _errorCount++;

            if (_errorCount >= 3)
            {
                _wrongThriceScreen.SetActive(true);

                _nextButton.onClick.RemoveAllListeners();
                _nextButton.onClick.AddListener(ErrorThrice);

                return;

            }
            else
            {
                _wrongOnceScreen.SetActive(true);

                _nextButton.onClick.RemoveAllListeners();
                _nextButton.onClick.AddListener(ErrorOnce);

                return;
            }

        }

        

    }

    public void ErrorThrice()
    {

        Reset();


        _wrongThriceScreen.SetActive(false);

        _minigameScreen.SetActive(true);
        _currentTrial++;

        if (_currentTrial < 3)
        {
            for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                _selectables[i].gameObject.SetActive(true);

            switch (_currentTrial)
            {
                case 1:
                    _descriptionText.text = "Selecione apenas três as expressões faciais de neutralidade.";

                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _selectables[i]._image.sprite = _trial02[i].Sprite;
                        _selectables[i].Emotion = _trial02[i].emotion;
                    }
                    break;
                case 2:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _selectables[i]._image.sprite = _trial03[i].Sprite;
                        _selectables[i].Emotion = _trial03[i].emotion;
                    }
                    break;
            }

            _errorCount = 0;


            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Next);

        }
        else
        {
            _minigameScreen.SetActive(false);
            _finalScreen.SetActive(true);

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Final);

        }


    }

    public void ErrorOnce()
    {
        Reset();

        _wrongOnceScreen.SetActive(false);

        _minigameScreen.SetActive(true);

        _nextButton.onClick.RemoveAllListeners();
        _nextButton.onClick.AddListener(Next);
    }

    public void Right()
    {

        Reset();

        _currentTrial++;

        if (_currentTrial < 3)
        {
            for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                _selectables[i].gameObject.SetActive(true);

            switch (_currentTrial)
            {
                case 1:
                    _descriptionText.text = "Selecione apenas as expressões faciais de " + emotion;
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _selectables[i]._image.sprite = _trial02[i].Sprite;
                        _selectables[i].Emotion = _trial02[i].emotion;
                    }
                    break;
                case 2:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _selectables[i]._image.sprite = _trial03[i].Sprite;
                        _selectables[i].Emotion = _trial03[i].emotion;
                    }
                    break;
            }

            _errorCount = 0;

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Next);


            _rightScreen.SetActive(false);

            _minigameScreen.SetActive(true);

        }
        else
        {
            _rightScreen.SetActive(false);
            _finalScreen.SetActive(true);

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Final);
        }

     


    }

    public void Final()
    {
        _finalScreen.SetActive(false);
        OnEndMiniGame.Invoke();
    }

    public void Reset()
    {
        foreach (var item in _selectables)
        {
            item.Selected = false;
            item._image.color = Color.white;
        }
    }


}
