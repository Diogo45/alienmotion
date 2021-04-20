using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubquestionnaireManager : MonoBehaviour
{

    [SerializeField] private QuestionContainer _questions;

    private List<TMPro.TMP_Dropdown> _dropdownList;

    private List<GameObject> _currentQuestions;


    public void Awake()
    {
        
    }


}
