using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class PlayerMovement : MonoBehaviour {
  private SpriteRenderer sprite;
  private Animator anim;
  private AudioSource audioSrc;

  static void WriteToFile(string filePath, string textToWrite)
  {
    using (StreamWriter outputFile = new StreamWriter(filePath, true))
    {
      outputFile.WriteLine(textToWrite);
    }
  }

  void Start()
  {
    sprite = gameObject.GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
    audioSrc = gameObject.GetComponent<AudioSource>();
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

    if (Input.GetKey(KeyCode.M))
    {
      audioSrc.Stop();
    }

    if (Input.GetKey(KeyCode.P))
    {
      audioSrc.Play();
    }

    if (Input.GetKey(KeyCode.L))
    {
      WriteToFile("historico.txt", "test");
    }

    if ((!Input.GetKey(KeyCode.W)) & (!Input.GetKey(KeyCode.A)) & (!Input.GetKey(KeyCode.S)) & (!Input.GetKey(KeyCode.D)))
    {
      anim.SetBool("walking", false);
    }
  }
}