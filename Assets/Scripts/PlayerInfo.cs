using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
  public static int selectedSpecies = 0; // TODO: Trocar para -1
  public static int chestsFound = 0;
  public static int CHESTS_TO_WIN = 6;
  public static int lastChestFound = -1;
  public static int chestBeingPlayed = -1;

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}
