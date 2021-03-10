using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireManager : MonoBehaviour
{
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTest()
    {
        Debug.Log("Click");
    }

}
