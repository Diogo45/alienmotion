using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Questionnaire
{
    public class ScreenManager : MonoBehaviour
    {

        [field: SerializeField]
        public int _currentScreen { get; private set; }

        [SerializeField]
        private int _screenQuantity;

        [SerializeField]
        private TMP_Text _pageCounter;

        void Start()
        {
            _currentScreen = 0;
            // -2 for the Next and Previous buttons
            _screenQuantity = transform.childCount - 2;
            _pageCounter.text = _currentScreen + "/" + _screenQuantity;
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

            _pageCounter.text = (_currentScreen + 1) + "/" + _screenQuantity;

        }


        public void Previous()
        {
            if (_currentScreen - 1 >= 0)
            {
                transform.GetChild(_currentScreen).gameObject.SetActive(false);
                transform.GetChild(--_currentScreen).gameObject.SetActive(true);
            }

            _pageCounter.text = (_currentScreen + 1) + "/" + _screenQuantity;
        }


    }
}
