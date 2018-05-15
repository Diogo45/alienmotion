using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MiniGameType3 : MiniGame
{
  public MiniGameImage[][] images;
  private Transform progress;

  public MiniGameType3(string name, string explanation, Transform sceneElement, string shortExplanation, Sprite faceInformation, MiniGameImage[][] images) : base(name, explanation, sceneElement, shortExplanation, faceInformation)
  {
    this.images = images;
    this.progress = GameObject.Find("MinigameCanvas").transform.Find("Image/Progress");
  }

  public override void SetupMiniGame()
  {
    progress.gameObject.SetActive(true);
    ImageSelection.selectedImages = new LinkedList<int>();
    int i = 0;
    while (i < images[this.currentChallenge].Length)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(true);
      sceneElement.Find("MiniGame/image" + i).GetComponent<Image>().sprite = images[this.currentChallenge][i].image;
      ImageSelection.SetSingleImageHighlightColor(i, new Color32(255, 255, 255, 255));
      i++;
    }
    while (i < 12)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(false);
      i++;
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

  private bool hasWrongAnswear() {
    return hasWrongAnswearAux(ImageSelection.selectedImages.First);
  }

  private bool hasWrongAnswearAux(LinkedListNode<int> n) {
    if (!images[this.currentChallenge][n.Value].isCorrect) {
      return true;
    }
    if (n.Next == null) {
      return false;
    }
    return hasWrongAnswearAux(n.Next);
  }

  private int countAllCorrectAnswears() {
    int correctCount = 0;
    for (int i = 0; i < images[this.currentChallenge].Length; i++)
    {
      if (images[this.currentChallenge][i].isCorrect) {
        correctCount++;
      }
    }
    return correctCount;
  }

  public override MiniGameResponse ValidateAnswear()
  {
    if (ImageSelection.selectedImages.Count == 0)
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Selecione pelo menos uma imagem!");
    }
    if (hasWrongAnswear())
    {
      return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Você selecionou alguma imagem errada.");
    }
    if (ImageSelection.selectedImages.Count != countAllCorrectAnswears())
    {
      return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Faltou selecionar mais algumas imagens corretas!");
    }
    return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
  }

  public override void ClearImagesColors()
  {
    for (int i = 0; i < images[this.currentChallenge].Length; i++)
      ImageSelection.SetSingleImageColor(i, new Color(255, 255, 255, 255));
  }
}