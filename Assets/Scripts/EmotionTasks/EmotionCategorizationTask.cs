using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionCategorizationTask : MonoBehaviour
{


    [SerializeField] private GameObject _imageShow;
    [SerializeField] private GameObject _crossShow;
    [SerializeField] private GameObject _emotionChoiceShow;

    private float _showImageForSeconds = 0.2f;
    private float _showCrossForSeconds = 1f;

    [SerializeField] private Image _imageField;
    [SerializeField] private List<Image> _emotionList;
    private string[] _emotionAnswers;
    private string _emotionAnswer;

    public int _currentImageIndex { get; private set; }

    private void Awake()
    {
        _emotionAnswers = new string[_emotionList.Count];
                
    }




    public void Next()
    {
        
        _emotionAnswers[_currentImageIndex] = _emotionAnswer;

        if (_currentImageIndex < _emotionList.Count)
            _currentImageIndex++;
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


}
