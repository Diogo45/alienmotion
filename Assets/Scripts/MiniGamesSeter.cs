using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamesSeter : MonoBehaviour {
	void Start () {
    PlayerInfo.SetMiniGames();
    InitialMessage.setInitialText();
    FinalMessage.setFinalText();
  }
}
