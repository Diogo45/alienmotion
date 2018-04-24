using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MiniGameImage
{
  public Sprite image;
  public bool isCorrect;
  public string wrongMessage;

  public MiniGameImage(Sprite image, bool isCorrect, string wrongMessage)
  {
    this.image = image;
    this.isCorrect = isCorrect;
    this.wrongMessage = wrongMessage;
  }
}
