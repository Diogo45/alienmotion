using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalMessage : MonoBehaviour
{
  public static int finalTextStep = 0;
  public static Transform finalTextField;
  public static Transform finalImageField;

  public static void setFinalText()
  {
    finalTextField = GameObject.Find("EndgameCanvas").transform.Find("EndgameMessage/Scroll View/Viewport/Content/Text").transform;
    finalTextField.GetComponent<Text>().text = PlayerInfo.finalTexts[0];
    finalImageField = GameObject.Find("EndgameCanvas").transform.Find("EndgameMessage/Scroll View/Viewport/Content/Image").transform;
    finalImageField.GetComponent<Image>().sprite = PlayerInfo.finalImages[0];
  }

  public static void setFinalAudio()
  {
    Audio.component.clip = PlayerInfo.finalAudios[0];
    Audio.component.Play();
  }

  public void okButton()
  {
    GameObject.Find("EndgameMessage/Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
    Audio.component.Stop();
    if (finalTextStep + 1 == PlayerInfo.finalTexts.Length)
    {
      Application.Quit();
    }
    else
    {
      finalTextStep++;
      Audio.component.clip = PlayerInfo.finalAudios[finalTextStep];
      Audio.component.Play();
      finalTextField.GetComponent<Text>().text = PlayerInfo.finalTexts[finalTextStep];
      finalImageField.GetComponent<Image>().sprite = PlayerInfo.finalImages[finalTextStep];
    }
  }
}
