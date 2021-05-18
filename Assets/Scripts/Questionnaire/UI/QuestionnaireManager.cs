using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Questionnaire
{
    public class QuestionnaireManager : Singleton<QuestionnaireManager>
    {
       

        [System.Serializable]
        public struct QuestionnaireObj
        {
            public GameObject Title;
            public GameObject WelcomePage;
            public GameObject Info;
            public GameObject Login;
            public GameObject Register;
            public GameObject RegisterParent;
            public GameObject TCLE;
            public GameObject TALE;
            public GameObject SDData;
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

        [field: SerializeField] public GameObject Game { get; private set; }

        private QState _state;

        [field: SerializeField] public QuestionnaireObj UI { get; private set; }



        public void Awake()
        {
            base.Awake();
        }

    }
}