using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CloseEmotion : MonoBehaviour {
	Transform endgameMessage;
  Transform chestMessage;
  Transform resultMessage;
  Transform shortExplanation;
  Transform mainScrollView;
  Transform resultScreen;

  public void Start() {
		endgameMessage = GameObject.Find("EndgameCanvas").transform.Find("EndgameMessage");
    chestMessage = GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/chestMessage");
    shortExplanation = GameObject.Find("MinigameCanvas").transform.Find("Image/shortExplanation");
    mainScrollView = GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View");
    resultScreen = GameObject.Find("MinigameCanvas").transform.Find("Image/ResultScreen");
    resultMessage = GameObject.Find("MinigameCanvas").transform.Find("Image/ResultScreen/resultMessage");
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
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.SetShortExplanation(PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name);
    PlayerInfo.current_step_game = PlayerInfo.STEP_PLAYING_MINIGAME;

    string path = "historico.txt";
    using (var tw = new StreamWriter(path, true))
    {
      tw.WriteLine(Time.time + " segundos: Começou minigame. (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
      tw.WriteLine(Time.time + " segundos: Começou desafio número 1. (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
    }
  }

  public void proceedGame()
  {
    bool hasNextChallenge = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.HasNextChallenge();
    mainScrollView.gameObject.SetActive(true);
    shortExplanation.gameObject.SetActive(true);
    resultScreen.gameObject.SetActive(false);

    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage2 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    if (hasNextChallenge)
    {
      // ir para próxima etapa
      PlayerInfo.current_step_game = PlayerInfo.STEP_PLAYING_MINIGAME;
      PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.NextChallenge();
      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time + " segundos: Começou desafio número " + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.currentChallenge+1+". (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
      }
    }
    else
    {
      // encerrar game
      chestMessage.gameObject.SetActive(true);
      PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.sceneElement.gameObject.SetActive(false);
      PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.FinishGame();
      PlayerInfo.current_step_game = PlayerInfo.STEP_FINISHED_MINIGAME;
      resultMessage.GetComponent<Text>().text = "";
      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time + " segundos: Finalizou o minigame. (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
      }
      ok_click();
    }
  }

  public void backToGame()
  {
    // voltar para jogo
    mainScrollView.gameObject.SetActive(true);
    shortExplanation.gameObject.SetActive(true);
    resultScreen.gameObject.SetActive(false);
    PlayerInfo.current_step_game = PlayerInfo.STEP_PLAYING_MINIGAME;
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage2 = PlayerInfo.NOT_SELECTED_ANSWEAR;
  }

  public void selectEmotion()
  {
    PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.ClearImagesColors();
    int responseCode = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.ValidateAnswear().code;
    string responseMessage = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.ValidateAnswear().message;

    mainScrollView.gameObject.SetActive(false);
    shortExplanation.gameObject.SetActive(false);
    resultScreen.gameObject.SetActive(true);
    if (responseCode == PlayerInfo.CORRECT_ANSWEAR)
    {
      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time + " segundos: Acertou a resposta. (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
      }
      PlayerInfo.current_step_game = PlayerInfo.STEP_RECEIVING_POSITIVE_FEEDBACK;
      bool hasNextChallenge = PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].game.HasNextChallenge();
      if (hasNextChallenge)
      {
        // responseMessage + próxima etapa
        resultMessage.GetComponent<Text>().text = responseMessage + "\n\nClique em OK para ir para a próxima etapa.";
      }
      else
      {
        // responseMessage + final de minigame
        resultMessage.GetComponent<Text>().text = responseMessage + "\n\nVocê finalizou todas as etapas deste mini-jogo!\nClique em OK para continuar.";
      }
    }
    else
    {
      PlayerInfo.current_step_game = PlayerInfo.STEP_RECEIVING_NEGATIVE_FEEDBACK;
      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time + " segundos: Errou a resposta. (" + PlayerInfo.EMOTIONS[PlayerInfo.chestBeingPlayed].name + ")");
      }
      // mensagem de falha (não selecionou ou selecionou errado)
      if (responseCode == PlayerInfo.WRONG_ANSWEAR)
      {
        // "Que pena! Você errou!" + responseMessage
        resultMessage.GetComponent<Text>().text = "Que pena! Você errou!\n" + responseMessage + "\n\nClique em OK para tentar novamente.";
      }
      else
      {
        // responseMessage
        resultMessage.GetComponent<Text>().text = responseMessage;
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
      string path = "historico.txt";
      using (var tw = new StreamWriter(path, true))
      {
        tw.WriteLine(Time.time + " segundos: Finalizou todos os minigames\nFim de jogo.");
      }
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
      case PlayerInfo.STEP_RECEIVING_POSITIVE_FEEDBACK:
        proceedGame();
        break;
      case PlayerInfo.STEP_RECEIVING_NEGATIVE_FEEDBACK:
        backToGame();
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
