using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireManager : MonoBehaviour
{
    [System.Serializable]
    public struct QuestionnaireUI
    {
        public GameObject WelcomePage;
        public GameObject Info;
        public GameObject LoginResonsavel;
        public GameObject RegisterResponsavel;
        public GameObject LoginAdolescente;
        public GameObject RegisterAdolescente;
    }


    [field: SerializeField] public QuestionnaireUI UI { get; private set; }

    [field: SerializeField] public GameObject Title { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickTest()
    {
        Debug.Log("Click");
    }

}
