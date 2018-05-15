using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MiniGameType1 : MiniGame
{
  public MiniGameImage[][][] images;
  private Transform progress;

  public MiniGameType1(string name, string explanation, Transform sceneElement, string shortExplanation, Sprite faceInformation, MiniGameImage[][][] images) : base(name, explanation, sceneElement, shortExplanation, faceInformation)
  {
    this.images = images;
    this.progress = GameObject.Find("MinigameCanvas").transform.Find("Image/Progress");
  }

  public override void SetupMiniGame()
  {
    progress.gameObject.SetActive(true);
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    int i = 0;

    for (int j = 0; j < 2; j++)
    {
      while (i < images[this.currentChallenge][j].Length)
      {
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).gameObject.SetActive(true);
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).GetComponent<Image>().sprite = images[this.currentChallenge][j][i].image;
        i++;
      }
      while (i < 6)
      {
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).gameObject.SetActive(false);
        i++;
      }
      i = 0;
    }

    int k = 0;
    while (k <= this.currentChallenge)
    {
      progress.Find("p" + k).GetComponent<Image>().sprite = PlayerInfo.miniGameDoneImg;
      k++;
    }
    while (k < images.Length)
    {
      progress.Find("p" + k).GetComponent<Image>().sprite = PlayerInfo.miniGameToDoImg;
      k++;
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
    if ((ImageSelection.selectedImage0 == PlayerInfo.NOT_SELECTED_ANSWEAR) || (ImageSelection.selectedImage1 == PlayerInfo.NOT_SELECTED_ANSWEAR))
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Selecione uma de cada categoria!");
    }
    else if (images[this.currentChallenge][0][ImageSelection.selectedImage0].isCorrect && images[this.currentChallenge][1][ImageSelection.selectedImage1].isCorrect)
    {
      return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
    }
    return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Tente novamente.");
  }

  public override void ClearImagesColors()
  {
    for (int i = 0; i < 2; i++)
      for (int j = 0; j < images[this.currentChallenge][i].Length; j++)
        ImageSelection.SetImageOfMultiplesColor(i, j, new Color(255, 255, 255, 255));
  }
}