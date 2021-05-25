using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubquestionnaireManager : MonoBehaviour
{
    [SerializeField] private string[] _questions;
    [SerializeField] private string[] _questionsAnswers;

    [SerializeField] private RegisterDataParent _registerDataParent;

    [SerializeField] private List<InputValue> _inputList;
    
    private void Awake()
    {
        _inputList = new List<InputValue>(GetComponentsInChildren<InputValue>(true));



        for (int i = 0; i < _inputList.Count; i++)
        {
            _inputList[i].QuestionIndex = i;
            _inputList[i].Init();
        }

        _questions = new string[_inputList.Count];
        _questionsAnswers = new string[_inputList.Count];

        WriteAnswers();

    }

    public void WriteAnswers()
    {
        foreach (var item in _inputList)
        {
            _questions[item.QuestionIndex] = (item.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text);
            _questionsAnswers[item.QuestionIndex] = item.Value;
        }

        _registerDataParent.Questions = new List<string>(_questions);
        _registerDataParent.Answers = new List<string>(_questionsAnswers);


    }

    



}
