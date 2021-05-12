using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionHuntersUIController : Singleton<EmotionHuntersUIController>
{
    private void Awake()
    {
        base.Awake();
    }

    [SerializeField] private GameObject IntroAlfred;
    [SerializeField] private GameObject Sadness;
    [SerializeField] private GameObject Anger;
    [SerializeField] private GameObject Fear;
    [SerializeField] private GameObject Neutral;

    public void IntroAlfredToGame()
    {

        IntroAlfred.SetActive(false);
        EmotionHuntersController.instance.StartGame();
    }


}
