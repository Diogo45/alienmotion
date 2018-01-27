using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
  public static int selectedSpecies = -1; // TODO: Trocar para -1
  public static int chestsFound = 0;
  public static int CHESTS_TO_WIN = 6;

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}
