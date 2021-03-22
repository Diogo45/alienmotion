using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Questionnaire
{
    public class QuestionnaireManager : MonoBehaviour
    {
        [System.Serializable]
        public struct QuestionnaireObj
        {
            public GameObject WelcomePage;
            public GameObject Info;
            public GameObject Register;
            public GameObject LoginParent;
            public GameObject RegisterParent;
            public GameObject TCLE;
            public GameObject SDData;
            public GameObject LoginTeen;
            public GameObject RegisterTeen;
            public GameObject RegisterFinalScreen;
        }

        public struct QuestionnaireTextAssets
        {
            public TextAsset Welcome;
            public TextAsset Info;
            public TextAsset RegisterParentFinal;
            public TextAsset RegisterTeenFinal;
        }

        [Flags]
        private enum QState
        {
            None = 0,
            Parent = 2, Teen = 4,
            Register = 16, Login = 32,
            Finalized = 64, TCLE = 128, TALE = 256, DataCollection = 512,

            RegisterParent = Parent & Register,
            LoginParent = Parent & Login,

            RegisterTeen = Teen & Register,
            LoginTeen = Teen & Login,
        }


        public static QuestionnaireManager instance;

        [SerializeField] private GameObject Title;

        private QState _state;

        [field: SerializeField] public QuestionnaireObj UI { get; private set; }


        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {

        }

    }
}