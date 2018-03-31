using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialMessage : MonoBehaviour
{
  public static int initialTextStep = 0;
  public static Transform initialTextField;

  public static void setInitialText()
  {
    initialTextField = GameObject.Find("NewGameCanvas/Image/Scroll View/Viewport/Content/Text").transform;
    initialTextField.GetComponent<Text>().text = PlayerInfo.initialTexts[0];
  }

  public void okClick()
  {
    if (initialTextStep + 1 == PlayerInfo.initialTexts.Length)
    {
      gameObject.SetActive(false);
      PlayerInfo.chestBeingPlayed = -1;
    }
    else
    {
      initialTextStep++;
      initialTextField.GetComponent<Text>().text = PlayerInfo.initialTexts[initialTextStep];
    }
  }
}
