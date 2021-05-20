using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Update()
    {
        _text.text = EmotionHuntersController.instance._miniGamesCompleted + "/4";
    }



}
