using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseEmotion : MonoBehaviour {
	Transform endgameMessage;
  Transform chestMessage;
  Transform miniGameExplanation;
	public void Start() {
		endgameMessage = GameObject.Find("EndgameCanvas").transform.Find("EndgameMessage");
    chestMessage = GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/chestMessage");
  }

  public void showMiniGameExplanation()
  {
    chestMessage.gameObject.SetActive(false);
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.SetUIExplanationText(PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name);
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.gameObject.SetActive(true);
    PlayerInfo.current_step_game = PlayerInfo.STEP_LEARNING_MINIGAME;
  }

  public void playMiniGame()
  {
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.HideUIExplanation();
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.SetupMiniGame();
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.ShowMiniGame();
    PlayerInfo.current_step_game = PlayerInfo.STEP_PLAYING_MINIGAME;
  }

  public void selectEmotion()
  {
    if (PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.ValidateAnswear() == PlayerInfo.CORRECT_ANSWEAR) {
      bool hasNextChallenge = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.HasNextChallenge();
      if (hasNextChallenge)
      {
        PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.NextChallenge();
      }
      else
      {
        PlayerInfo.current_step_game = PlayerInfo.STEP_FINISHED_MINIGAME;
        ok_click();
      }
    }
  }

  public void close()
  {
    Transform container = transform.Find("Image");
    container.gameObject.SetActive(false);
    PlayerInfo.chestBeingPlayed = -1;

		PlayerInfo.chestsFound++;
    GameObject.Find("HUD/Image/chestCounterText").GetComponent<Text>().text = PlayerInfo.chestsFound.ToString() + "/" + PlayerInfo.CHESTS_TO_WIN.ToString();

    PlayerInfo.current_step_game = PlayerInfo.STEP_NOT_PLAYING;

    if (PlayerInfo.chestsFound == PlayerInfo.CHESTS_TO_WIN)
    {
      endgameMessage.gameObject.SetActive(true);
      PlayerInfo.chestBeingPlayed = 99;
    }
  }

  public void ok_click()
  {
    switch (PlayerInfo.current_step_game)
    {
      case PlayerInfo.STEP_NOT_PLAYING:
        showMiniGameExplanation();
        break;
      case PlayerInfo.STEP_LEARNING_MINIGAME:
        playMiniGame();
        break;
      case PlayerInfo.STEP_PLAYING_MINIGAME:
        selectEmotion();
        break;
      case PlayerInfo.STEP_FINISHED_MINIGAME:
        close();
        break;
      default:
        close();
        break;
    }
  }
}
