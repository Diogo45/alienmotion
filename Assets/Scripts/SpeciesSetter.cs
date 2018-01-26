using UnityEngine;
using System.Collections;

public class SpeciesSetter : MonoBehaviour {
  private Animator anim;
  void Start() {
    anim = GetComponent<Animator>();
    anim.SetInteger("selectedAge", PlayerInfo.selectedSpecies);
  }
}
