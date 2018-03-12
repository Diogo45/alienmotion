using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSelection : MonoBehaviour
{
  public static int selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
  public static int selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;
  public static int selectedImage2 = PlayerInfo.NOT_SELECTED_ANSWEAR;

  public void SelectImage(int imageNumber)
  {
    selectedImage0 = imageNumber;
    SetImageColor(imageNumber, new Color32(250, 203, 193, 255));
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
      case 2:
        selectedImage2 = imageNumber;
        break;
    }
  }

  public static void SetImageColor(int imageNumber, Color color)
  {
    for (int i = 0; i < 12; i ++) {
      if (i == imageNumber)
      {
        Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/image" + i).GetComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.normalColor = color;
        btn.colors = cb;
      }
      else
      {
        Button btn = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.Find("MiniGame/image" + i).GetComponent<Button>();
        ColorBlock cb = btn.colors;
        cb.normalColor = new Color32(255, 255, 255, 255);
        btn.colors = cb;
      }
    }
  }

  public static void SetImageOfMultiplesColor(int index, int imageNumber, Color color)
  {
    for (int i = 0; i < 4; i++)
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
