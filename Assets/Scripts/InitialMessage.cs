using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialMessage : MonoBehaviour
{
  public static int initialTextStep = 0;
  public static Transform initialTextField;

  void Start()
  {
    initialTextField = GameObject.Find("NewGameCanvas/Image/Scroll View/Viewport/Content/Text").transform;
  }

  public static void setInitialText()
  {
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
