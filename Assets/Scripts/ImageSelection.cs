using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageSelection : MonoBehaviour
{
  public static LinkedList<int> selectedImages = new LinkedList<int>();
  public static int selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
  public static int selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;

  public void SelectImage(int imageNumber)
  {
    selectedImage0 = imageNumber;
    SetImageColorAndClearOthers(imageNumber, new Color32(250, 203, 193, 255));
  }
  public void SelectOrDeselectImage(int imageNumber)
  {
    EventSystem.current.SetSelectedGameObject(null);
    if (selectedImages.Contains(imageNumber))
    {
      selectedImages.Remove(imageNumber);
      SetSingleImageColor(imageNumber, new Color32(255, 255, 255, 255));
      SetSingleImageHighlightColor(imageNumber, new Color32(255, 255, 255, 255));
    }
    else
    {
      selectedImages.AddLast(imageNumber);
      SetSingleImageColor(imageNumber, new Color32(250, 203, 193, 255));
      SetSingleImageHighlightColor(imageNumber, new Color32(250, 203, 193, 255));
    }
  }

  public void SelectImageOfMultiples(string image)
  {
    int imageNumber = int.Parse(image.Substring(1, 1));
    int index = int.Parse(image.Substring(0, 1));
    SetImageOfMultiplesColor(index, imageNumber, new Color32(250, 203, 193, 255));
    switch (index)
    {
      case 0:
        selectedImage0 = imageNumber;
        break;
      case 1:
        selectedImage1 = imageNumber;
        break;
    }
  }

  public static void SetSingleImageColor(int imageNumber, Color color)
  {
    Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/image" + imageNumber).GetComponent<Button>();
    ColorBlock cb = btn.colors;
    cb.normalColor = color;
    btn.colors = cb;
  }

  public static void SetSingleImageHighlightColor(int imageNumber, Color color)
  {
    Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/image" + imageNumber).GetComponent<Button>();
    ColorBlock cb = btn.colors;
    cb.highlightedColor = color;
    btn.colors = cb;
  }

  public static void SetImageColorAndClearOthers(int imageNumber, Color color)
  {
    for (int i = 0; i < 12; i ++) {
      if (i == imageNumber)
      {
        SetSingleImageColor(i, color);
      }
      else
      {
        SetSingleImageColor(i, new Color32(255, 255, 255, 255));
      }
    }
  }

  public static void SetImageOfMultiplesColor(int index, int imageNumber, Color color)
  {
    for (int i = 0; i < 6; i++)
    {
      if (i == imageNumber) {
        Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/bodyPart" + index + "/image" + i).GetComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.normalColor = color;
        btn.colors = cb;
      }
      else
      {
        Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/bodyPart" + index + "/image" + i).GetComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.normalColor = new Color32(255, 255, 255, 255);
        btn.colors = cb;
      }
    }
  }
}
