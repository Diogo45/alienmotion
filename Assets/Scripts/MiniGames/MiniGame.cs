using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

abstract public class MiniGame
{
  public string name;
  public string explanation;
  public string shortExplanation;
  public Transform sceneElement;
  public Sprite faceInformation;
  public int currentChallenge = 0;

  public MiniGame(string name, string explanation, Transform sceneElement, string shortExplanation, Sprite faceInformation)
  {
    this.name = name;
    this.explanation = explanation;
    this.sceneElement = sceneElement;
    this.shortExplanation = shortExplanation;
    this.faceInformation = faceInformation;
  }

  public void SetUIExplanationText()
  {
    sceneElement.Find("miniGameExplanation").gameObject.SetActive(true);
    sceneElement.Find("miniGameExplanation").GetComponent<Text>().text = this.explanation;
  }

  public void HideUIExplanation()
  {
    sceneElement.Find("miniGameExplanation").gameObject.SetActive(false);
  }

  public void ShowMiniGame()
  {
    sceneElement.Find("MiniGame").gameObject.SetActive(true);
  }

  public void SetShortExplanation()
  {
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(true);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").GetComponent<Text>().text = shortExplanation;
  }

  abstract public MiniGameResponse ValidateAnswear();
  abstract public bool HasNextChallenge();
  abstract public void NextChallenge();

  abstract public void SetupMiniGame();
  abstract public void FinishGame();
  abstract public void ClearImagesColors();
}