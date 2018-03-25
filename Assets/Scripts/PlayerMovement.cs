using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
  private SpriteRenderer sprite;
  private Animator anim;
  private CharacterController controller;

  void Start()
  {
    sprite = gameObject.GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
    controller = GetComponent<CharacterController>();
  }

  void Update()
  {
    if (PlayerInfo.chestBeingPlayed == -1)
    {
      var x = Input.GetAxis("Horizontal") * Time.deltaTime * 30.0f;
      var z = Input.GetAxis("Vertical") * Time.deltaTime * 30.0f;

      if (x != 0 || z != 0)
      {
        if (x < 0)
        {
          sprite.flipX = true;
        }
        else
        {
          sprite.flipX = false;
        }
        anim.SetBool("walking", true);
        controller.Move(new Vector3(x, 0, z));
      }
      else
      {
        anim.SetBool("walking", false);
      }
    }
    else
    {
      anim.SetBool("walking", false);
    }
  }
}