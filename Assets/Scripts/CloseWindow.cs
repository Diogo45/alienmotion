using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
  Transform windowCloseGame;
  public void Start()
  {
    windowCloseGame = GameObject.Find("MenuCanvas").transform.Find("Window");
  }
  public void close()
  {
    windowCloseGame.gameObject.SetActive(false);
  }
}
