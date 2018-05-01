using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audio : MonoBehaviour {
  public static AudioSource component;

  void Start()
  {
    component = gameObject.GetComponent<AudioSource>();
    InitialMessage.setInitialAudio();
  }
}
