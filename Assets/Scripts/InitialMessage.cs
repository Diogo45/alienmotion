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

  public static void setInitialAudio()
  {
    Audio.component.clip = PlayerInfo.initialAudios[0];
    Audio.component.Play();
  }

  public void okClick()
  {
    GameObject.Find("NewGameCanvas/Image/Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
    Audio.component.Stop();
    if (initialTextStep + 1 == PlayerInfo.initialTexts.Length)
    {
      gameObject.SetActive(false);
      PlayerInfo.chestBeingPlayed = -1;
    }
    else
    {
      initialTextStep++;
      Audio.component.clip = PlayerInfo.initialAudios[initialTextStep];
      Audio.component.Play();
      initialTextField.GetComponent<Text>().text = PlayerInfo.initialTexts[initialTextStep];
    }
  }
}
