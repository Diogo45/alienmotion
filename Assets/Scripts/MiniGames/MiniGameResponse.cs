using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MiniGameResponse
{
  public int code;
  public string message;

  public MiniGameResponse(int code, string message)
  {
    this.code = code;
    this.message = message;
  }
}
