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
    [SerializeField] private GameObject SadnessMinigame;

    [SerializeField] private GameObject Anger;
    [SerializeField] private GameObject AngerMinigame;

    [SerializeField] private GameObject Fear;
    [SerializeField] private GameObject FearMinigame;

    [SerializeField] private GameObject Neutral;
    [SerializeField] private GameObject NeutralMinigame;


    public void IntroAlfredToGame()
    {

        IntroAlfred.SetActive(false);
        EmotionHuntersController.instance.StartGame();
    }

    public void SadToMinigame()
    {
        Sadness.SetActive(false);
        SadnessMinigame.SetActive(true);
    }
    public void AngerToMinigame()
    {
        Anger.SetActive(false);
        AngerMinigame.SetActive(true);
    }
    public void FearToMinigame()
    {
        Fear.SetActive(false);
        FearMinigame.SetActive(true);
    }
    public void NeutralToMinigame()
    {
        Neutral.SetActive(false);
        NeutralMinigame.SetActive(true);
    }


}
