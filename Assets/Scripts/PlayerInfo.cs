using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
  public static int selectedSpecies = 0;

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}
