using UnityEngine;

public class Emotion
{
  public string name;
  public string description;
  public MiniGame game;
  public AudioClip audioDescription;

  public Emotion(string name, string description, MiniGame game, AudioClip audioDescription)
  {
    this.name = name;
    this.description = description;
    this.game = game;
    this.audioDescription = audioDescription;
  }
}