using UnityEngine;
using System.Collections;

public class SpeciesSetter : MonoBehaviour {
  void Start() {
    anim = GetComponent<Animator>();
    anim.SetInteger("selectedSpecies", PlayerInfo.selectedSpecies);
  }
}
