using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ChestFound : MonoBehaviour
{
  Transform minigameCanvas;
  Text chestTitle;
  Text chestText;
  ScrollRect scrollView;
  private Animator chestAnim;

  void Start()
  {
    minigameCanvas = GameObject.Find("MinigameCanvas").transform.Find("Image");
    chestTitle = minigameCanvas.Find("chestTitle").GetComponent<Text>();
    Transform scrollViewTransform = minigameCanvas.Find("Scroll View");
    scrollView = scrollViewTransform.GetComponent<ScrollRect>();
    chestText = scrollViewTransform.Find("Viewport/Content/chestMessage").GetComponent<Text>();
  }

  void OnControllerColliderHit(ControllerColliderHit target)
  {
    if (target.gameObject.tag.Equals("Chest") == true)
    {
      int chestFoundNumber = target.gameObject.GetComponent<ChestInfo>().chestNumber;
      chestTitle.text = PlayerInfo.EMOTIONS[chestFoundNumber].name;
      chestText.text = PlayerInfo.EMOTIONS[chestFoundNumber].description;
      scrollView.verticalNormalizedPosition = 1f;
      PlayerInfo.chestBeingPlayed = chestFoundNumber;

      chestAnim = target.gameObject.GetComponent<Animator>();
      chestAnim.SetBool("found", true);
      target.gameObject.tag = "ChestFound";

      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time+" segundos: Baú encontrado. ("+PlayerInfo.EMOTIONS[chestFoundNumber].name+")");
      }

      StartCoroutine(ShowChestInfo());
    }
  }

  IEnumerator ShowChestInfo()
  {
    yield return new WaitForSecondsRealtime(1);
    minigameCanvas.gameObject.SetActive(true);
  }
}