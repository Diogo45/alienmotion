using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerInfo : MonoBehaviour
{
  public static int selectedSpecies = 0; // TODO: Trocar para -1
  public static int chestsFound = 0;
  public static int CHESTS_TO_WIN = 6;
  public static int lastChestFound = -1;
  public static int chestBeingPlayed = 99; // -1: Nenhum diálogo na tela / 99: História / 0 ~ 6: Baú
  public static int current_step_game = -1;
  public const int STEP_NOT_PLAYING = -1;
  public const int STEP_LEARNING_EMOTION = 0;
  public const int STEP_LEARNING_MINIGAME = 1;
  public const int STEP_PLAYING_MINIGAME = 2;
  public const int STEP_FINISHED_MINIGAME = 3;
  public const int STEP_RECEIVING_POSITIVE_FEEDBACK = 4;
  public const int STEP_RECEIVING_NEGATIVE_FEEDBACK = 5;
  public const int STEP_LEARNING_FACE = 6;
  public const int WRONG_ANSWEAR = -2;
  public const int NOT_SELECTED_ANSWEAR = -1;
  public const int CORRECT_ANSWEAR = 1;
  public static string[] initialTexts;
  public static AudioClip[] initialAudios;
  public static AudioClip[] finalAudios;
  public static string[] finalTexts;
  public static string[] CHESTS_TITLE = new string[6]{
    "Alegria",
    "Tristeza",
    "Medo",
    "Nojo",
    "Raiva",
    "Surpresa",
  };

  public static string[] CHESTS_TEXT;
  public static AudioClip[] AUDIO_DESCRIPTION;

  public static MiniGame gameType0Alegria;
  public static MiniGame gameType0Raiva;
  public static MiniGame gameType1Nojo;
  public static MiniGame gameType1Surpresa;
  public static MiniGame gameType2Medo;
  public static MiniGame gameType2Tristeza;
  public static Emotion[] EMOTIONS;

  public static void SetMiniGames()
  {
    if (selectedSpecies == 0)
    {
      initialTexts = new string[]{
        "Olá! Este é o Alfred. Ele vem de um planeta muito distante chamado Ogle-TR e foi escolhido, entre muitos, para passar algumas horas aqui no Planeta Terra para virar um mestre das emoções! Se ele conseguir alcançar este objetivo, ele poderá retornar ao seu lar e ensinar para seus amigos como eles podem se relacionar melhor.",
        "No entanto, o caminho para virar mestre das emoções é muito difícil para um etzinho como Alfred e ele precisará de sua ajuda! Vamos começar!",
        "Você sabe porque sentimos as emoções? E porque choramos? Você já estranhou a cara que um amigo fez depois de algo que você disse? Você já ficou vermelho quando foi falar algo constrangedor? Isso já aconteceu, não é?\n\nPois então, alguns cientistas já pesquisam isso há muitos anos!\n\nE é sobre isso que queremos falar hoje, sobre as emoções e as expressões faciais delas.",
        "Alegria, medo, tristeza, raiva, nojo e surpresa. Essas são as emoções básicas ou primárias. Isso porque existem muuuuitas outras emoções que chamamos de secundárias, que são basicamente uma mistura das emoções primárias. Por exemplo, quando sentimos inveja, que é uma emoção secundária, é como se sentíssemos raiva e tristeza ao mesmo tempo.",
        "Uma forma fácil de saber se uma emoção é primária ou secundária é pensando nas expressões faciais, ou seja, na cara que as pessoas fazem quando sentem alguma dessas emoções.",
        "Vamos pensar:\n\nNão é mais fácil saber quando alguém sente tristeza do que quando alguém sente saudade? Por exemplo, quando você perde algo que você gosta muuuuuito, que cara você faz? É fácil de fazer, não é? Isso porque a tristeza é uma emoção primária que pode ser facilmente vista no rosto.",
        "Além disso, outra coisa importante de sabermos é que todas as emoções que sentimos tem um por quê. E a partir de agora vamos falar sobre isso.",
        "As emoções primárias tem uma função importante na nossa vida e tiveram um papel muito relevante para a evolução da nossa espécie. Vamos aprender um pouco mais sobre cada uma delas e virar mestre das emoções?",
        "Estamos na Fazenda das Emoções. Nesta fazenda, 6 baús escondem segredos e premiações sobre cada uma das seis emoções básicas. Vença as tarefas escondidas e acumule pontos até se tornar o verdadeiro mestre das emoções.\n\nQual será o primeiro baú?"
      };
      initialAudios = new AudioClip[]{
        Resources.Load<AudioClip>("Audio/Intro/1AC"),
        Resources.Load<AudioClip>("Audio/Intro/2AC"),
        Resources.Load<AudioClip>("Audio/Intro/1C"),
        Resources.Load<AudioClip>("Audio/Intro/2C"),
        Resources.Load<AudioClip>("Audio/Intro/3C"),
        Resources.Load<AudioClip>("Audio/Intro/4C"),
        Resources.Load<AudioClip>("Audio/Intro/5C"),
        Resources.Load<AudioClip>("Audio/Intro/4AC"),
        Resources.Load<AudioClip>("Audio/Intro/5AC"),
      };
      finalTexts = new string[1]{
        "Agora que já passamos por todas as emoções básicas e aprendemos sobre elas, você ganhou o título de Mestre das Emoções e auxiliou Alfred a entender tudo sobre as emoções! Obrigado pela ajuda e espero que o treinamento tenha contribuído para o seu crescimento assim como contribuiu para o Alfred ser um etzinho melhor!"
      };
      finalAudios = new AudioClip[]{
        Resources.Load<AudioClip>("Audio/Ending/6AC"),
      };
      CHESTS_TEXT = new string[6]{
        "<b>Alegria</b> é a emoção que mais gostamos de sentir. Ela aparece quando sentimos prazer em algo. Estamos sempre na busca de sentir alegria. Ela faz com que sintamos que nossa vida vale a pena e, através do sorriso, podemos mostrar aos outros quando estamos felizes. Sentimos muuuuita alegria, por exemplo, quando brincamos com os nossos melhores amigos.",
        "A <b>tristeza</b> é a emoção que menos gostamos de sentir. Apesar disso, essa é uma emoção tão importante quanto todas as outras. Desde que somos bebês ela nos ajuda a mostrar quando não estamos bem, através do choro. Quando choramos, fica fácil das outras pessoas perceberem que precisamos de ajuda. Você já deve ter sentido tristeza quando perdeu alguma coisa que gostava muito.",
        "O <b>medo</b> é a emoção que mais sentimos no corpo. Quando sentimos medo, os nossos pensamentos quase que param e ficamos num estado chamado de luta ou fuga.Essa emoção é muito importante porque é ela que nos impede, por exemplo,  de atravessar a rua quando vemos um carro vindo muito rápido. Também é ela que nos faz correr de um cachorro raivoso pra ele não nos alcançar.",
        "O <b>nojo</b> existe pra nos afastar de coisas que podem nos deixar doentes. É ele que faz com que a gente não coma comida estragada ou tape o nariz quando sentimos um cheiro muuuito ruim. A gente costuma ficar longe de coisas nojentas, não é mesmo?",
        "A <b>raiva</b> aparece quando sentimos que alguém foi injusto com a gente. Por exemplo, quando nos chamam de algum apelido que não gostamos. Na hora podemos brigar ou então ficar quietos, mas depois ficamos um tempãaao pensando no que devíamos ter respondido.",
        "A <b>surpresa</b> é parecida com o medo e é bem comum que a gente confunda essas duas emoções. Para não confundir, lembre-se: sentimos surpresa quando acontecem coisas que não estamos esperando. Por exemplo, quando você ganha um presente que você não imaginava que ganharia."
      };
      AUDIO_DESCRIPTION = new AudioClip[6]{
        Resources.Load<AudioClip>("Audio/Alegria/C"),
        Resources.Load<AudioClip>("Audio/Tristeza/C"),
        Resources.Load<AudioClip>("Audio/Medo/C"),
        Resources.Load<AudioClip>("Audio/Nojo/C"),
        Resources.Load<AudioClip>("Audio/Raiva/C"),
        Resources.Load<AudioClip>("Audio/Surpresa/C"),
      };
      gameType0Alegria = new MiniGameType3(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de alegria.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame3"),
        "Selecione apenas as expressões faciais de alegria.",
        Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[4]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_2_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_0_surpresa"), false, "Esta expressão é de surpresa.\n")
          },
          new MiniGameImage[8] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_0_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_1_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_2_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_3_neurtro"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_4_raiva"), false, "Esta expressão é de raiva.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_5_nojo"), false, "Esta expressão é de nojo.\n")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_3_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_5_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_6_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_8_raiva"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_9_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_10_nojo"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_3_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_8_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_7_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/3/errado_9_neutra"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_8_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_3_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_9_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_7_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/4/errado_2_surpresa"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_8_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_9_neutro"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_3_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/5/errado_7_neutro"), false, ""),
          },
        }
      );
      gameType0Raiva = new MiniGameType3(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de raiva.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame3"),
        "Selecione apenas as expressões faciais de raiva.",
        Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[4]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/errado_1_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/errado_2_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/correto_1"), true, "")
          },
          new MiniGameImage[9] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_2_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_5_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_4_tristeza"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_8_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_10_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_6_tristeza"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_8_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/3/errado_5_nojo"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_8_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/4/errado_3_nojo"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_8_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/errado_9_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/5/certo_1"), true, ""),
          },
        }
      );
      gameType1Nojo = new MiniGameType1(
        "Montando a Face",
        "A seguir você encontrará imagens de olhos e bocas de diferentes emoções. Selecione as duas partes que representam nojo.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de nojo.",
        Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/faceInfo"),
        new MiniGameImage[][][]{
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte0/errado_2"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/0/parte1/certo_0"), true, "")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte0/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte0/certo_0"), true, "")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/1/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte0/errado_4"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/2/parte1/errado_4"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/3/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente."),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/4/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte0/certo_0"), true, ""),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/crianca/5/parte1/certo_0"), true, ""),
            }
          },
        }
      );
      gameType1Surpresa = new MiniGameType1(
        "Montando a Face",
        "A seguir você encontrará imagens de olhos e bocas de diferentes emoções. Selecione as duas partes que representam surpresa.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de surpresa.",
        Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/faceInfo"),
        new MiniGameImage[][][]{
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte0/certo_0"), true, "")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/0/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte0/errado_2"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/1/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte0/errado_4"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/2/parte1/errado_4"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente."),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/3/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/4/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/crianca/5/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
            }
          },
        }
      );
      gameType2Medo = new MiniGameType2(
        "Separe as Emoções",
        "A seguir você encontrará várias imagens. Arraste as imagens para o bloco conforme sua classificação.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame2"),
        "Arraste as emoções para as caixas corretas.",
        Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[6]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/0/certo_2"), true, "")
          },
          new MiniGameImage[8] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/1/errado_3"), false, "")
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/2/certo_4"), true, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/3/errado_1"), false, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/4/errado_0"), false, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/crianca/5/certo_1"), true, ""),
          },
        },
        new string[] { "Neutras", "Neutras", "Neutras", "Neutras", "Neutras", "Neutras" }
      );
      gameType2Tristeza = new MiniGameType2(
        "Separe as Emoções",
        "A seguir você encontrará várias imagens. Arraste as imagens para o bloco conforme sua classificação.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame2"),
        "Arraste as emoções para as caixas corretas.",
        Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[6]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/0/certo_2"), true, ""),
          },
          new MiniGameImage[8] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/1/errado_2"), false, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/2/certo_4"), true, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/3/errado_0"), false, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/4/certo_2"), true, ""),
          },
          new MiniGameImage[10] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/crianca/5/errado_4"), false, ""),
          },
        },
        new string[] { "Neutras", "Neutras", "Neutras", "Neutras", "Neutras", "Neutras" }
      );
    }
    else if (selectedSpecies == 1)
    {
      initialTexts = new string[]{
        "Olá! Este é o Alfred. Ele vem de um planeta muito distante chamado Ogle-TR e foi escolhido, entre muitos, para passar algumas horas aqui no Planeta Terra para virar um mestre das emoções! Se ele conseguir alcançar este objetivo, ele poderá retornar ao seu lar e ensinar para seus amigos como eles podem se relacionar melhor.",
        "No entanto, o caminho para virar mestre das emoções é muito difícil para um etzinho como Alfred e ele precisará de sua ajuda! Vamos começar!",
        "Você já se perguntou alguma vez o motivo pelo qual temos emoções? E porque choramos? Você já se pegou repensando algo que ia dizer por causa da expressão facial que um amigo fez pra você? E aquele momento em que você foi falar algo em público e ficou com o rosto vermelho? Isso já aconteceu, não é mesmo?",
        "Pois então, cientistas como Darwin já pesquisam isso há muitos anos!\n\nE é sobre isso que queremos falar hoje, sobre as emoções e as expressões faciais delas.",
        "Alegria, medo, tristeza, raiva, nojo e surpresa. Já ouviu falar? Essas são as emoções consideradas básicas ou primárias. Isso porque existem outras centenas de emoções que chamamos de secundárias, como saudade, ansiedade, tranquilidade, inveja, ciúme, e tantas outras. Mas porque secundárias? Secundárias a que? Àquelas primeiras! As básicas. Isso quer dizer que quando sentimos inveja, por exemplo, existe outra emoção por trás disso. Talvez uma raiva misturada com tristeza.",
        "Ou então quando dizemos que estamos ansiosos: isso significa que na verdade o que estamos sentindo primariamente é medo. Medo de algo que pode acontecer em um futuro breve. Entendeu?",
        "Uma forma fácil de saber se uma emoção é primária ou secundária é pensando nas expressões faciais. Quando pensamos em raiva, por exemplo, é fácil imaginar alguém expressando essa emoção através do rosto, não é? Mas quando pensamos em fazer cara de tranquilidade, por exemplo, já não é tão fácil assim, não é mesmo? Isso porque raiva é uma emoção primária e tranquilidade é secundária, como já vimos anteriormente.",
        "As emoções primárias tem uma função importante na nossa vida e tiveram um papel muito relevante para a evolução da nossa espécie. Vamos aprender um pouco mais sobre cada uma delas e virar mestre das emoções?",
        "Estamos na Fazenda das Emoções. Nesta fazenda, 6 baús escondem segredos e premiações sobre cada uma das seis emoções básicas. Vença as tarefas escondidas e acumule pontos até se tornar o verdadeiro mestre das emoções.\n\nQual será o primeiro baú?"
      };
      initialAudios = new AudioClip[]{
        Resources.Load<AudioClip>("Audio/Intro/1AC"),
        Resources.Load<AudioClip>("Audio/Intro/2AC"),
        Resources.Load<AudioClip>("Audio/Intro/1A"),
        Resources.Load<AudioClip>("Audio/Intro/2A"),
        Resources.Load<AudioClip>("Audio/Intro/3A"),
        Resources.Load<AudioClip>("Audio/Intro/4A"),
        Resources.Load<AudioClip>("Audio/Intro/5A"),
        Resources.Load<AudioClip>("Audio/Intro/4AC"),
        Resources.Load<AudioClip>("Audio/Intro/5AC"),
      };
      finalTexts = new string[1]{
        "Pronto! Agora já passamos por todas as emoções básicas e aprendemos sobre elas. Você ganhou o título de Mestre das Emoções e auxiliou Alfred a entender tudo sobre as emoções! Obrigado pela ajuda e espero que o treinamento tenha contribuído para o seu crescimento assim como contribuiu para o Alfred ser um etzinho melhor! Esperamos que você se sinta mais apto a reconhecer e entender as emoções e expressões faciais no seu contexto do dia-a-dia."
      };
      finalAudios = new AudioClip[]{
        Resources.Load<AudioClip>("Audio/Ending/6AC"),
      };
      CHESTS_TEXT = new string[6]{
        "<b>Alegria</b> é a emoção que mais gostamos de sentir. Ela aparece quando sentimos prazer em algo; e por isso, acaba guiando nossas escolhas e decisões. Ela faz com que sintamos que nossa vida vale a pena e, através do sorriso, podemos transmitir socialmente nosso contentamento. Além disso, a alegria nos ajuda na criação de laços afetivos desde que somos bebês; pois quando sorrimos recebemos mais atenção e afeto de quem está a nossa volta.",
        "A <b>tristeza</b> é a emoção que mais evitamos sentir, afinal, ninguém gosta de ficar triste. Porem, essa é uma emoção tão importante quanto todas as outras. Desde que somos bebês ela nos ajuda na comunicação dos nossos desconfortos, através do choro. Depois que aprendemos a falar, ela continua nos ajudando a aprender com nossos erros e a pensarmos de forma mais profunda e criativa. É a tristeza que nos move a introspecção, a pensarmos melhor sobre os caminhos que a vida nos oferece e a nos colocarmos no lugar do outro. Além disso, o choro, manifestação da tristeza, também facilita a busca por apoio, já que quando choramos aumentamos a probabilidade de chamar a atenção de alguém que possa nos ajudar. Sendo assim, podemos dizer que quando evitamos sentir tristeza a qualquer custo, também nos afastamos de nós mesmos, somos menos autênticos e podemos até nos distanciarmos de quem amamos.",
        "O <b>medo</b> é a emoção que mais sentimos de forma física. Quando o nosso sistema dessa emoção se ativa, os nossos pensamentos quase que se bloqueiam e utilizamos o sistema de luta ou suga. O nosso corpo se prepara pra reagir antes mesmo de pensarmos o que seria mais correto. Viram como nosso corpo é inteligente? É por isso que muitas vezes podemos fazer coisas estúpidas quando estamos com medo. Porque nos preparamos pra lutar, pra fugir ou pra congelar. Nesse sentido, podemos dizer que quando estamos com medo nós sentimos, agimos e só depois pensamos no que aconteceu.",
        "O <b>nojo</b> existe pra nos prevenir do risco de contaminação. É ele que nos impede de comer alimentos que poderiam ser tóxicos ou coisas que nos fariam mal. Ele nos protege de bactérias e de doenças e começa a se desenvolver exatamente quando a criança está se preparando pra caminhar; por volta dos dois anos. Não faz todo sentido? Isso porque até ali o bebê era exclusivamente dependente do fornecimento de alimento e cuidados maternos. É por isso que bebês pequenos parecem não sentir nojo de colocar nada na boca. Mas falando nisso, você sabia que existem dois tipos de nojo?\n\nIsso mesmo. Além deste nojo que estávamos falando, existe também o nojo moral; que é aquele nojo que sentimos de um político corrupto ou de um pedófilo, por exemplo, ou de alguém que consideramos que fez algo muito errado. E sabe o que é mais interessante? Que a nossa reação frente aos dois tipos de nojo é a mesma, a de evitação. Queremos nos afastar daquele objeto que nos desperta essa emoção e nossa expressão facial também é a mesma quando estamos falando de algo muito nojento ou de uma pessoa que julgamos da mesma forma. Isso porque o sistema ativado é o mesmo. ",
        "O papel evolutivo da <b>raiva</b> está relacionado a situações nas quais temos que avaliar o custo-benefício de algo. Quando nos sentimos de alguma forma prejudicados, por exemplo, e queremos mostrar que não devemos ser colocados naquela posição novamente. É isso que acontece naquelas momentos que passamos por uma situação desconfortável e depois ficamos um tempão pensando no que devíamos ter respondido pra aquela pessoa que nos desrespeitou de alguma forma.",
        "A <b>surpresa</b> tem bastante a ver com o medo. Inclusive, quando somos crianças, essas são as emoções mais difíceis de se distinguir. É por isso que muitas vezes uma criança pode chorar de susto, mesmo que seja uma surpresa boa, como uma festa de aniversário. Quando sentimos surpresa é como se o nosso corpo se preparasse pra sentir medo, mas o nosso pensamento nos diz que nada de ruim vai acontecer. É como um medo bom. É por isso também, por ser um sistema muito rápido a ser ativado, que muitas vezes não conseguimos fingir gostar de algo que não gostamos quando somos pegos de surpresa.",
      };
      AUDIO_DESCRIPTION = new AudioClip[6]{
        Resources.Load<AudioClip>("Audio/Alegria/A"),
        Resources.Load<AudioClip>("Audio/Tristeza/A"),
        Resources.Load<AudioClip>("Audio/Medo/A"),
        Resources.Load<AudioClip>("Audio/Nojo/A"),
        Resources.Load<AudioClip>("Audio/Raiva/A"),
        Resources.Load<AudioClip>("Audio/Surpresa/A"),
      };
      gameType0Alegria = new MiniGameType3(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de alegria.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame3"),
        "Selecione apenas as expressões faciais de alegria.",
        Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[8]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/carreto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_6"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_9"), false, "Esta expressão é de raiva.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_11"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_12"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_18"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_20"), false, "Esta expressão é neutra.\n")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_19"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_25"), false, "Esta expressão é de raiva.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_27"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_21"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_29"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_23"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_24"), false, "Esta expressão é de raiva.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_17"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_26"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_28"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/correto_0"), true, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_2"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_13"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_14"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_10"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_0"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_1"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_7"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_5"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_8"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_15"), false, "Esta expressão é de raiva.\n")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_8_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_7_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_8_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/errado_4_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/3/correto_1"), true, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_8_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_7_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_3_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/4/errado_9_neutra"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_4_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_1_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_0_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_6_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_5_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_2_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_7_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/errado_3_surpresa"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/5/neutro_8_neutra"), false, "")
          }
        }
      );
      gameType0Raiva = new MiniGameType3(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de raiva.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame3"),
        "Selecione apenas as expressões faciais de raiva.",
        Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[8]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_0_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_5_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_4_nojo"), false, "Esta expressão é de nojo.\n")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_7_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_4_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_9_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_8_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_5_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_6_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_0_medo"), false, "Esta expressão é de medo.\n")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_6_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_8_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_5_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_9_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_7_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_0_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_4_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/correto_1"), true, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_6_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_8_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_9_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/3/errado_10_neutra"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/4/errado_8_tristeza"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_0_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_4_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_1_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_3_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_9_neutra"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_7_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_5_nojo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_6_tristeza"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_2_medo"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/5/errado_8_tristeza"), false, "")
          }
        }
      );
      gameType1Nojo = new MiniGameType1(
        "Montando a Face",
        "A seguir você encontrará imagens de olhos e bocas de diferentes emoções. Selecione as duas partes que representam nojo.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de nojo.",
        Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/faceInfo"),
        new MiniGameImage[][][]{
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte0/errado_2"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/0/parte1/certo_0"), true, "")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte0/certo_0"), true, "")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/1/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte0/errado_4"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/2/parte1/errado_4"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/3/parte1/certo_0"), true, "")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/4/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente.")
            },
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte0/errado_2_raiva"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/errado_0_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/errado_3_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/errado_1_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/errado_4_medo"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Nojo/adulto/5/parte1/certo_0"), true, "")
            },
          }
        }
      );
      gameType1Surpresa = new MiniGameType1(
        "Montando a Face",
        "A seguir você encontrará imagens de olhos e bocas de diferentes emoções. Selecione as duas partes que representam surpresa.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de surpresa.",
        Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/faceInfo"),
        new MiniGameImage[][][]{
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte0/certo_0"), true, "")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/0/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte0/errado_2"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[4]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/1/parte1/errado_2"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte0/errado_4"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/errado_0"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/errado_1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/errado_2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/errado_3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/2/parte1/errado_4"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/errado_2_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte0/certo_0"), true, ""),
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/3/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/errado_2_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/errado_2_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/errado_0_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/4/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGameImage[][]{
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/errado_0_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/errado_5_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/errado_3_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/errado_1_alegria"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte0/certo_0"), true, "")
            },
            new MiniGameImage[6]{
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/certo_0"), true, ""),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/errado_2_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/errado_1_raiva"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/errado_3_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/errado_4_tristeza"), false, "Que pena, você errou! Tente novamente."),
              new MiniGameImage(Resources.Load<Sprite>("MiniGame1Images/Surpresa/adulto/5/parte1/errado_5_tristeza"), false, "Que pena, você errou! Tente novamente.")
            }
          }
        }
      );
      gameType2Medo = new MiniGameType2(
        "Separe as Emoções",
        "A seguir você encontrará várias imagens. Arraste as imagens para o bloco conforme sua classificação.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame2"),
        "Arraste as emoções para as caixas corretas.",
        Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[8]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/0/correto_2"), true, "")
          },
          new MiniGameImage[8] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/correto_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/1/errado_3"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/errado_5"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/2/correto_5"), true, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/3/errado_5"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_5"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_6"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_6"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/4/certo_3"), true, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_5"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Medo/adulto/5/certo_2"), true, ""),
          }
        },
        new string[]{"Neutras", "Neutras", "Neutras", "Neutras", "Neutras", "Neutras" }
      );
      gameType2Tristeza = new MiniGameType2(
        "Separe as Emoções",
        "A seguir você encontrará várias imagens. Arraste as imagens para o bloco conforme sua classificação.",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame2"),
        "Arraste as emoções para as caixas corretas.",
        Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/faceInfo"),
        new MiniGameImage[][]{
          new MiniGameImage[8]{
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/0/correto_2"), true, "")
          },
          new MiniGameImage[8] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/correto_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/1/errado_2"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/correto_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/2/errado_5"), false, "")
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_5"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/3/errado_4"), false, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_5"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/4/certo_1"), true, ""),
          },
          new MiniGameImage[12] {
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_0"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_1"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_0"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_3"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_4"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_1"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_2"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_5"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_3"), false, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_4"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/certo_2"), true, ""),
            new MiniGameImage(Resources.Load<Sprite>("MiniGame2Images/Tristeza/adulto/5/errado_5"), false, ""),
          },
        },
        new string[] { "Neutras", "Neutras", "Neutras", "Neutras", "Neutras", "Neutras" }
      );
    }
    EMOTIONS = new Emotion[6]{
      new Emotion(CHESTS_TITLE[0], CHESTS_TEXT[0], gameType0Alegria, AUDIO_DESCRIPTION[0]),
      new Emotion(CHESTS_TITLE[1], CHESTS_TEXT[1], gameType2Tristeza, AUDIO_DESCRIPTION[1]),
      new Emotion(CHESTS_TITLE[2], CHESTS_TEXT[2], gameType2Medo, AUDIO_DESCRIPTION[2]),
      new Emotion(CHESTS_TITLE[3], CHESTS_TEXT[3], gameType1Nojo, AUDIO_DESCRIPTION[3]),
      new Emotion(CHESTS_TITLE[4], CHESTS_TEXT[4], gameType0Raiva, AUDIO_DESCRIPTION[4]),
      new Emotion(CHESTS_TITLE[5], CHESTS_TEXT[5], gameType1Surpresa, AUDIO_DESCRIPTION[5]),
    };
  }

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}

