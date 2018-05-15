using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MiniGameType0: MiniGame
{
  public MiniGameImage[][] images;
  private Transform progress;

  public MiniGameType0(string name, string explanation, Transform sceneElement, string shortExplanation, Sprite faceInformation, MiniGameImage[][] images) : base(name, explanation, sceneElement, shortExplanation, faceInformation)
  {
    this.images = images;
    this.progress = GameObject.Find("MinigameCanvas").transform.Find("Image/Progress");
  }

  public override void SetupMiniGame()
  {
    progress.gameObject.SetActive(true);
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    int i = 0;
    while (i < images[this.currentChallenge].Length)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(true);
      sceneElement.Find("MiniGame/image" + i).GetComponent<Image>().sprite = images[this.currentChallenge][i].image;
      i++;
    }
    while (i < 12)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(false);
      i++;
    }

    int j = 0;
    while (j <= this.currentChallenge)
    {
      progress.Find("p" + j).GetComponent<Image>().sprite = PlayerInfo.miniGameDoneImg;
      j++;
    }
    while (j < images.Length)
    {
      progress.Find("p" + j).GetComponent<Image>().sprite = PlayerInfo.miniGameToDoImg;
      j++;
    }
  }

  public override bool HasNextChallenge()
  {
    return this.currentChallenge < (images.Length - 1);
  }

  public override void NextChallenge()
  {
    this.currentChallenge++;
    SetupMiniGame();
  }

  public override void FinishGame()
  {
    progress.gameObject.SetActive(false);
    sceneElement.Find("MiniGame").gameObject.SetActive(false);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(false);
    this.currentChallenge = 0;
  }

  public override MiniGameResponse ValidateAnswear()
  {
    if (ImageSelection.selectedImage0 == PlayerInfo.NOT_SELECTED_ANSWEAR)
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Selecione uma imagem!");
    }
    else if (images[this.currentChallenge][ImageSelection.selectedImage0].isCorrect)
    {
      return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
    }
    return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, images[this.currentChallenge][ImageSelection.selectedImage0].wrongMessage);
  }

  public override void ClearImagesColors()
  {
    for (int i = 0; i < images[this.currentChallenge].Length; i++)
      ImageSelection.SetSingleImageColor(i, new Color(255, 255, 255, 255));
  }
}