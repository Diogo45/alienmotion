using UnityEngine;
using System.Collections;

public class ChestFound : MonoBehaviour
{
  Transform minigameCanvas;
  private Animator chestAnim;

  void Start()
  {
    minigameCanvas = GameObject.Find("MinigameCanvas").transform.Find("Image");
  }

  void OnCollisionEnter(Collision target)
  {
    if (target.gameObject.tag.Equals("Chest") == true)
    {
      int chestFoundNumber = target.gameObject.GetComponent<ChestInfo>().chestNumber;
      minigameCanvas.gameObject.SetActive(true);
      minigameCanvas.Find("chest" + chestFoundNumber.ToString() + "Message").gameObject.SetActive(true);
      PlayerInfo.chestBeingPlayed = chestFoundNumber;

      chestAnim = target.gameObject.GetComponent<Animator>();
      chestAnim.SetBool("found", true);
      target.gameObject.tag = "ChestFound";
    }
  }
}