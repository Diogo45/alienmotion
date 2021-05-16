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


    public void ToIntroAlfred()
    {

        IntroAlfred.SetActive(true);
        EmotionHuntersController.instance.DisableMovement();
    }

    public void IntroAlfredToGame()
    {

        IntroAlfred.SetActive(false);
        EmotionHuntersController.instance.EnableMovement();
    }

    public void SadToMinigame()
    {
        Sadness.SetActive(false);
        SadnessMinigame.SetActive(true);
    }

    public void SadMinigameToGame()
    {
        SadnessMinigame.SetActive(false);
        EmotionHuntersController.instance.EnableMovement();
        EmotionHuntersController.instance.CompleteMiniGame();
    }

    public void SadMinigame()
    {
        Sadness.SetActive(true);
        EmotionHuntersController.instance.DisableMovement();
    }



    public void AngerToMinigame()
    {
        Anger.SetActive(false);
        AngerMinigame.SetActive(true);
    }

    public void AngerMinigameToGame()
    {
        AngerMinigame.SetActive(false);
        EmotionHuntersController.instance.EnableMovement();
        EmotionHuntersController.instance.CompleteMiniGame();

    }

    public void ToAngerMinigame()
    {
        Anger.SetActive(true);
        EmotionHuntersController.instance.DisableMovement();
    }

    public void FearToMinigame()
    {
        Fear.SetActive(false);
        FearMinigame.SetActive(true);
    }

    public void FearMinigameToGame()
    {
        FearMinigame.SetActive(false);
        EmotionHuntersController.instance.EnableMovement();
        EmotionHuntersController.instance.CompleteMiniGame();

    }

    public void ToFearMinigame()
    {
        Fear.SetActive(true);
        EmotionHuntersController.instance.DisableMovement();
    }

    public void NeutralToMinigame()
    {
        Neutral.SetActive(false);
        NeutralMinigame.SetActive(true);
    }


    public void NeutralMinigameToGame()
    {
        NeutralMinigame.SetActive(false);
        EmotionHuntersController.instance.EnableMovement();
        EmotionHuntersController.instance.CompleteMiniGame();
 
    }

    public void ToNeutralMinigame()
    {
        Neutral.SetActive(true);
        EmotionHuntersController.instance.DisableMovement();
    }

}
