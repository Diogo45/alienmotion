using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct SpriteAnswer
{
    public Sprite Sprite;
    public int emotion;
}

public class DragAndDropGameManager : MonoBehaviour
{
   

    [SerializeField] private GameObject _minigameScreen;
    [SerializeField] private GameObject _rightScreen;
    [SerializeField] private GameObject _wrongOnceScreen;
    [SerializeField] private GameObject _wrongThriceScreen;
    [SerializeField] private GameObject _finalScreen;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    [SerializeField] private List<GameObject> _draggables;

    [SerializeField] private List<SpriteAnswer> _emotion01;
    [SerializeField] private List<SpriteAnswer> _emotion02;

    [SerializeField] private List<GameObject> _dropPoints;

    [SerializeField] private List<SpriteAnswer> _trial01;
    [SerializeField] private List<SpriteAnswer> _trial02;
    [SerializeField] private List<SpriteAnswer> _trial03;



    private int _currentSprite;
    private int _currentSnapPoint;
    private int _errorCount = 0;

    [SerializeField] private Sprite _defaultSprite;

    [SerializeField] private UnityEvent OnEndMiniGame;

    public int _currentTrial;
    public int[] _imgsPerTrial = { 4, 6, 8 };

    private void Awake()
    {
        for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
        {
            _draggables[i].SetActive(true);

        }

        for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
        {
            _draggables[i].GetComponent<Image>().sprite = _trial01[i].Sprite;
        }

        _previousButton.gameObject.SetActive(false);
        _nextButton.onClick.RemoveAllListeners();

        _nextButton.onClick.AddListener(Next);

        _emotion01 = new List<SpriteAnswer>();
        _emotion02 = new List<SpriteAnswer>();
    }

    public void Drop(int id)
    {
        if (_currentSprite == -1)
        {
            if (_dropPoints[id].GetComponent<Image>().sprite != _defaultSprite)
            {
                if (id < _dropPoints.Count / 2f)
                {
                    _emotion01.RemoveAll(x => x.Sprite.name == _dropPoints[id].GetComponent<Image>().sprite.name);
                }
                else
                {
                    _emotion02.RemoveAll(x => x.Sprite.name == _dropPoints[id].GetComponent<Image>().sprite.name);
                }

                GameObject draggable = _draggables.Find(x => x.GetComponent<Image>().sprite.name == _dropPoints[id].GetComponent<Image>().sprite.name);

                draggable.SetActive(true);

            }

           

            _dropPoints[id].GetComponent<Image>().sprite = _defaultSprite;
            return;
        }

        for (int i = 0; i < _dropPoints.Count; i++)
        {
            if (_dropPoints[i].GetComponent<Image>().sprite.name == _draggables[_currentSprite].GetComponent<Image>().sprite.name)
            {

                if (i < _dropPoints.Count / 2f)
                {
                    _emotion01.RemoveAll(x => x.Sprite.name == _dropPoints[i].GetComponent<Image>().sprite.name);
                }
                else
                {
                    _emotion02.RemoveAll(x => x.Sprite.name == _dropPoints[i].GetComponent<Image>().sprite.name);
                }

                _dropPoints[i].GetComponent<Image>().sprite = _defaultSprite;

                



            }

        }

        _currentSnapPoint = id;
        _dropPoints[id].GetComponent<Image>().sprite = _draggables[_currentSprite].GetComponent<Image>().sprite;

        if (id < _dropPoints.Count / 2f)
        {
            switch (_currentTrial)
            {
                case 0:
                    _emotion01.Add(new SpriteAnswer { Sprite = _trial01[_currentSprite].Sprite, emotion = _trial01[_currentSprite].emotion });
                    break;
                case 1:
                    _emotion01.Add(new SpriteAnswer { Sprite = _trial02[_currentSprite].Sprite, emotion = _trial02[_currentSprite].emotion });
                    break;
                case 2:
                    _emotion01.Add(new SpriteAnswer { Sprite = _trial03[_currentSprite].Sprite, emotion = _trial03[_currentSprite].emotion });
                    break;
            }
        }
        else
        {
            switch (_currentTrial)
            {
                case 0:
                    _emotion02.Add(new SpriteAnswer { Sprite = _trial01[_currentSprite].Sprite, emotion = _trial01[_currentSprite].emotion });
                    break;
                case 1:
                    _emotion02.Add(new SpriteAnswer { Sprite = _trial02[_currentSprite].Sprite, emotion = _trial02[_currentSprite].emotion });
                    break;
                case 2:
                    _emotion02.Add(new SpriteAnswer { Sprite = _trial03[_currentSprite].Sprite, emotion = _trial03[_currentSprite].emotion });
                    break;
            }
        }

        _draggables[_currentSprite].SetActive(false);


        _currentSprite = -1;

    }



    private bool CheckAnswer()
    {
        if (_emotion01.Count < _imgsPerTrial[_currentTrial] / 2f)
            return false;


        if (_emotion02.Count < _imgsPerTrial[_currentTrial] / 2f)
            return false;

        foreach (var item in _emotion01)
        {
            if (item.emotion != 1)
            {
                return false;
            }
        }

        foreach (var item in _emotion02)
        {
            if (item.emotion != 0)
            {
                return false;
            }
        }

        return true;
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
            {
                _draggables[i].SetActive(true);

            }

            switch (_currentTrial)
            {

                case 1:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _draggables[i].GetComponent<Image>().sprite = _trial02[i].Sprite;
                    }
                    break;
                case 2:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _draggables[i].GetComponent<Image>().sprite = _trial03[i].Sprite;
                    }
                    break;
            }

            _errorCount = 0;

        }
        else
        {
            _minigameScreen.SetActive(false);
            _finalScreen.SetActive(true);
        }

        _nextButton.onClick.RemoveAllListeners();
        _nextButton.onClick.AddListener(Next);

    }

    public void ErrorOnce()
    {
        //Debug.Log("WrongOnce");

        Reset();


        for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
        {
            _draggables[i].SetActive(true);

        }

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
            {
                _draggables[i].SetActive(true);

            }

            switch (_currentTrial)
            {

                case 1:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _draggables[i].GetComponent<Image>().sprite = _trial02[i].Sprite;
                    }
                    break;
                case 2:
                    for (int i = 0; i < _imgsPerTrial[_currentTrial]; i++)
                    {
                        _draggables[i].GetComponent<Image>().sprite = _trial03[i].Sprite;
                    }
                    break;
            }

            _errorCount = 0;

            _rightScreen.SetActive(false);

            _minigameScreen.SetActive(true);

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Next);


        }
        else
        {

            _rightScreen.SetActive(false);
            //_minigameScreen.SetActive(false);
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


    public void Next()
    {
        if (CheckAnswer())
        {
            // _minigameScreen.SetActive(false);
            _rightScreen.SetActive(true);

            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(Right);

        }
        else
        {
            _errorCount++;

            if (_errorCount >= 3)
            {
                // _minigameScreen.SetActive(false);
                _wrongThriceScreen.SetActive(true);

                _nextButton.onClick.RemoveAllListeners();
                _nextButton.onClick.AddListener(ErrorThrice);

                return;

            }
            else
            {
                // _minigameScreen.SetActive(false);
                _wrongOnceScreen.SetActive(true);

                _nextButton.onClick.RemoveAllListeners();
                _nextButton.onClick.AddListener(ErrorOnce);

                return;
            }

        }

      



    }


    private void Reset()
    {
        foreach (var item in _dropPoints)
        {
            item.GetComponent<Image>().sprite = _defaultSprite;
        }

        _emotion01 = new List<SpriteAnswer>();
        _emotion02 = new List<SpriteAnswer>();
    }

    public void Drag(int sprite)
    {
        _currentSprite = sprite;
    }



}
