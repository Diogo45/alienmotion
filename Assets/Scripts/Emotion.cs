public class Emotion
{
  public string name;
  public string description;
  public MiniGame game;

  public Emotion(string name, string description, MiniGame game)
  {
    this.name = name;
    this.description = description;
    this.game = game;
  }
}