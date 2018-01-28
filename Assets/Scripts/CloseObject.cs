using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObject : MonoBehaviour {
  public void close() {
    gameObject.SetActive(false);
  }
}
