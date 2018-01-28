using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseEmotion : MonoBehaviour {
	Transform endgameMessage;
	public void Start() {
		endgameMessage = GameObject.Find("EndgameCanvas").transform.Find("EndgameMessage");
	}
  public void close()
  {
    Transform container = transform.Find("Image");
    // container.Find("chest" + PlayerInfo.chestBeingPlayed.ToString() + "Message").gameObject.SetActive(false);
    container.gameObject.SetActive(false);
    PlayerInfo.chestBeingPlayed = -1;

		PlayerInfo.chestsFound++;
    GameObject.Find("HUD/Image/chestCounterText").GetComponent<Text>().text = PlayerInfo.chestsFound.ToString() + "/" + PlayerInfo.CHESTS_TO_WIN.ToString();

    if (PlayerInfo.chestsFound == PlayerInfo.CHESTS_TO_WIN)
    {
      endgameMessage.gameObject.SetActive(true);
      PlayerInfo.chestBeingPlayed = 99;
    }
  }
}
