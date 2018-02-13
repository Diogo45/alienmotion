using UnityEngine;
using System.Collections;

public class ImageSelection : MonoBehaviour
{
  public static int selectedImage = PlayerInfo.NOT_SELECTED_ANSWEAR;

  public void SelectImage(int imageNumber)
  {
    selectedImage = imageNumber;
  }
}
