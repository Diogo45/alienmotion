using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Questionnaire
{
    public class ScreenManager : MonoBehaviour
    {
 
        [field: SerializeField]
        public int _currentScreen { get; private set; }

        [field: SerializeField] public int _screenQuantity { get; private set; }

        [SerializeField]
        private TMP_Text _pageCounter;

        public UnityEvent OnEndScreens; 
        
        void Start()
        {
            _currentScreen = 0;
            // -2 for the Next and Previous buttons
            _screenQuantity = transform.childCount - 2;

            if(_pageCounter)
                _pageCounter.text = (_currentScreen + 1) + "/" + _screenQuantity;
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
                Debug.Log("VLHE");
                OnEndScreens.Invoke();
            }

            if (_pageCounter)
                _pageCounter.text = (_currentScreen + 1) + "/" + _screenQuantity;

        }


        public void Previous()
        {
            if (_currentScreen - 1 >= 0)
            {
                transform.GetChild(_currentScreen).gameObject.SetActive(false);
                transform.GetChild(--_currentScreen).gameObject.SetActive(true);
            }

            if (_pageCounter)
                _pageCounter.text = (_currentScreen + 1) + "/" + _screenQuantity;
        }


    }
}
