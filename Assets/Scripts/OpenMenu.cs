using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
  Transform windowCloseGame;
  public void Start()
  {
    windowCloseGame = GameObject.Find("MenuCanvas").transform.Find("Window");
  }
  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (windowCloseGame.gameObject.activeInHierarchy)
      {
        windowCloseGame.gameObject.SetActive(false);
      }
      else
      {
        windowCloseGame.gameObject.SetActive(true);
      }
    }
  }
}
