using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
  public static int selectedSpecies = 0; // TODO: Trocar para -1

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}
