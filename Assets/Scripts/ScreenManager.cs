using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Questionnaire
{
    public class ScreenManager : MonoBehaviour
    {

#if UNITY_EDITOR
        [field: SerializeField]
#endif
        public int _currentScreen { get; private set; }

        [SerializeField]
        private int _screenQuantity;

        [SerializeField]
        private GameObject NextButton, PreviousButton;

        void Start()
        {
            _currentScreen = 0;
            // -2 for the Next and Previous buttons
            _screenQuantity = transform.childCount - 2;
        }

        public void Next()
        {
            if (_currentScreen + 1 < _screenQuantity)
            {
                transform.GetChild(_currentScreen).gameObject.SetActive(false);
                transform.GetChild(++_currentScreen).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(_currentScreen).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
                QuestionnaireUI.instance.SDData_Final();
            }
        }


        public void Previous()
        {
            if (_currentScreen - 1 >= 0)
            {
                transform.GetChild(_currentScreen).gameObject.SetActive(false);
                transform.GetChild(--_currentScreen).gameObject.SetActive(true);
            }
            else
            {
                
            }
        }


    }
}
