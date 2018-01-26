using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
  private SpriteRenderer sprite;
  private Animator anim;

  void Start()
  {
    sprite = gameObject.GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.W))
    {
      anim.SetBool("walking", true);
      Vector3 position = this.transform.position;
      position.z = (float)(position.z + 0.1);
      this.transform.position = position;
    }
    if (Input.GetKey(KeyCode.A))
    {
      anim.SetBool("walking", true);
      Vector3 position = this.transform.position;
      position.x = (float)(position.x - 0.1);
      this.transform.position = position;
      sprite.flipX = true;
    }
    if (Input.GetKey(KeyCode.S))
    {
      anim.SetBool("walking", true);
      Vector3 position = this.transform.position;
      position.z = (float)(position.z - 0.1);
      this.transform.position = position;
    }
    if (Input.GetKey(KeyCode.D))
    {
      anim.SetBool("walking", true);
      Vector3 position = this.transform.position;
      position.x = (float)(position.x + 0.1);
      this.transform.position = position;
      sprite.flipX = false;
    }

    if ((!Input.GetKey(KeyCode.W)) & (!Input.GetKey(KeyCode.A)) & (!Input.GetKey(KeyCode.S)) & (!Input.GetKey(KeyCode.D)))
    {
      anim.SetBool("walking", false);
    }
  }
}