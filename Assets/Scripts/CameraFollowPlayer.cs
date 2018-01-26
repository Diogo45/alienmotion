using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
  void Update () {
    GameObject playerCreature = GameObject.Find("Player");
    if (playerCreature)
    {
        Vector3 newCameraPosition = new Vector3(playerCreature.transform.position.x, gameObject.transform.position.y, playerCreature.transform.position.z - 60);
        gameObject.transform.position = newCameraPosition;
    }
  }
}
