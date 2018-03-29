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
  public const int WRONG_ANSWEAR = -2;
  public const int NOT_SELECTED_ANSWEAR = -1;
  public const int CORRECT_ANSWEAR = 1;
  public static string[] initialTexts;
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

  public static MiniGame gameType0Alegria;
  public static MiniGame gameType0Raiva;
  public static MiniGame game1;
  public static MiniGame game2;
  public static Emotion[] EMOTIONS;

  public static void SetMiniGames()
  {
    if (selectedSpecies == 0)
    {
      initialTexts = new string[4]{
        "Olá! Este é o Alfred. Ele vem de um planeta muito distante chamado Ogle-TR e foi escolhido, entre muitos, para passar algumas horas aqui no Planeta Terra para virar um mestre das emoções! Se ele conseguir alcançar este objetivo, ele poderá retornar ao seu lar e ensinar para seus amigos como eles podem se relacionar melhor.\nNo entanto, o caminho para virar mestre das emoções é muito difícil para um etzinho como Alfred. Ele precisará da sua ajuda.",
        "Você sabe porque sentimos as emoções? E porque choramos? Você já estranhou a cara que um amigo fez depois de algo que você disse? Você já ficou vermelho quando foi falar algo constrangedor? Isso já aconteceu, não é?\nPois então, alguns cientistas já pesquisam isso há muitos anos!\nE é sobre isso que queremos falar hoje, sobre as emoções e as expressões faciais delas.",
        "Alegria, medo, tristeza, raiva, nojo e surpresa. Já ouviu falar? Essas são as emoções básicas ou primárias. Isso porque existem muuuuitas outras emoções que chamamos de secundárias, como saudade, ansiedade, tranquilidade, inveja, ciúme, e várias outras. Mas porque chamamos secundárias? Secundárias a que? Àquelas primeiras! As básicas. Isso quer dizer que quando sentimos inveja, por exemplo, existe outra emoção por trás disso. Talvez uma raiva misturada com tristeza. Faz sentido, não é?!\nQuando dizemos então que estamos ansiosos: isso significa que na verdade o que sentimos por trás disso é medo. Medo de algo ruim que pode acontecer no futuro. Entendeu?\nUma forma fácil de saber se uma emoção é primária ou secundária é pensando nas expressões faciais, ou seja, na cara que as pessoas fazem quando sentem a tal emoção.\nVamos pensar:\nQuando pensamos em raiva, por exemplo, é fácil imaginar alguém expressando essa emoção através do rosto, não é? Mas quando pensamos em fazer cara de tranquilidade já não é tão fácil assim, não é? Isso porque raiva é uma emoção primária e tranquilidade é secundária, como acabamos de ver.\nAlém disso, outra coisa importante de sabermos é que todas as emoções que sentimos tem um por quê. E a partir de agora vamos falar sobre isso.\nAs emoções primárias tem uma função importante na nossa vida e tiveram um papel muito relevante para a evolução da nossa espécie. Vamos aprender um pouco mais sobre cada uma delas e virar mestre das emoções? ",
        "Estamos na Fazenda das Emoções. Nesta fazenda, 6 baús escondem segredos e premiações sobre cada uma das seis emoções básicas. Vença as tarefas escondidas e acumule pontos até se tornar o verdadeiro mestre das emoções.\n\nQual será o primeiro baú?"
      };
      finalTexts = new string[1]{
        "Agora que já passamos por todas as emoções básicas e aprendemos sobre elas, você ganhou o título de Mestre das Emoções e auxiliou Alfred a entender tudo sobre as emoções! Obrigado pela ajuda e espero que o treinamento tenha contribuído para o seu crescimento assim como contribuiu para o Alfred ser um etzinho melhor!"
      };
      CHESTS_TEXT = new string[6]{
        "<b>Alegria</b> é a emoção que mais gostamos de sentir. Ela aparece quando sentimos prazer em algo; e por isso, acaba guiando nossas escolhas e decisões. Estamos sempre na busca de sentir alegria. Ela faz com que sintamos que nossa vida vale a pena e, através do sorriso, podemos transmitir aos outros quando estamos contentes. Além disso, a alegria nos ajuda a criar laços afetivos desde que somos bebês; pois quando sorrimos recebemos mais atenção e afeto de quem está a nossa volta.",
        "A <b>tristeza</b> é a emoção mais difícil de sentir. Ninguém gosta de ficar triste, não é mesmo? Apesar disso, esta é uma emoção tão importante quanto todas as outras. Desde que somos bebês ela nos ajuda a mostrar quando não estamos bem, através do choro. Depois que aprendemos a falar, ela continua nos ajudando a aprender com nossos erros, a pensarmos de forma mais profunda e criativa e a nos colocarmos no lugar do outro.  Além disso, o choro, que é uma manifestação da tristeza, também ajuda a chamar a atenção de alguém que possa nos dar apoio.",
        "O <b>medo</b> é a emoção que mais sentimos no corpo. Quando sentimos essa emoção, os nossos pensamentos quase que param e ficamos naquele estado chamado de luta ou fuga. O nosso corpo se prepara pra reagir antes mesmo de pensarmos o que seria mais correto. Viram como nosso corpo é inteligente? É por isso que muitas vezes podemos fazer coisas estúpidas quando estamos com medo. Porque nos preparamos pra lutar, pra fugir ou pra congelar. Essa emoção é muito importante porque é ela que nos impede de atravessar a rua, por exemplo, quando vemos um carro vindo muito rápido. Também é ela que nos faz correr de um cachorro raivoso pra ele não nos alcançar.",
        "O <b>nojo</b> existe pra nos prevenir do risco de contaminação. É ele que nos impede de comer alimentos que poderiam nos fazer mal ou de tocar em coisas que poderiam nos passar doenças. Resumidamente, ele nos protege de bactérias e de doenças e começa a aparecer quando estamos nos preparando pra engatinhar, ainda na infância. Não faz sentido? Porque até engatinharmos é só a nossa mãe que cuida o que a gente come ou no que podemos tocar. Mas falando nisso, sabia que existem dois tipos de nojo?\n\nIsso mesmo. Além deste nojo que estávamos falando, existe também o nojo moral; que é aquele nojo que sentimos quando alguém faz algo muito errado, como roubar ou mal-tratar os animais. E sabe o que é mais interessante? Que a nossa reação aos dois tipos de nojo é a mesma. Sempre queremos nos afastar do que nos dá nojo.",
        "O papel da <b>raiva</b> aparece naquelas situações que temos que avaliar se algo vale a pena. Quando nos sentimos prejudicados ou injustiçados, por exemplo, e queremos mostrar que não devemos mais passar por isso. Como quando alguém faz chacota com a nossa cara em público. Podemos reagir na hora e brigar ou então ficar quietos, mas se isso acontece normalmente ficamos um tempãaao pensando no que devíamos ter respondido. Tanto o que nos faz discutir com alguém quanto o que nos move a ficar remoendo uma situação é a raiva.",
        "A <b>surpresa</b> tem bastante a ver com o medo. Inclusive, as vezes podemos confundir essas emoções. Por exemplo, podemos acabar chorando de susto, mesmo que seja uma surpresa boa, como quando gritam “Surpresa!” em uma festa de aniversário. Quando sentimos surpresa é como se o nosso corpo se preparasse pra sentir medo, mas o nosso pensamento nos diz que nada de ruim vai acontecer. É como um medo bom. É por isso também, por tudo acontecer tão rápido dentro da gente, que muitas vezes não conseguimos fingir gostar daquela meia sem graça que uma tia nos dá de natal. A nossa cara entrega que não gostamos do presente."
      };
      gameType0Alegria = new MiniGameType0(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de ",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame0"),
        "Selecione apenas as expressões faciais de ",
        new MiniGame0Image[][]{
          new MiniGame0Image[4]{
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_00"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_01"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_02"), false, "Esta expressão é de raiva.\n")
          },
          new MiniGame0Image[8] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_10"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/correto_10"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_11"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_12"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_13"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/correto_11"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_14"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_15"), false, "Esta expressão é de medo.\n")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_20"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_21"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_25"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_22"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_20"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_23"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_24"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_21"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_28"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_26"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_27"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_22"), true, "")
          }
        }
      );
      gameType0Raiva = new MiniGameType0(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de ",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame0"),
        "Selecione apenas as expressões faciais de ",
        new MiniGame0Image[][]{
          new MiniGame0Image[4]{
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/errado_0_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/errado_1_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/errado_2_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/0/correto_0"), true, "")
          },
          new MiniGame0Image[8] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_0_alegreia"), false, "Esta expressão é de alegria.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/correto_1"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_3_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_4_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/1/errado_5_tristeza"), false, "Esta expressão é de tristeza.\n")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_0_alegria"), false, "Esta expressão é de alegria.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/correto_ 0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_4_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_3_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_6_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_5_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_7_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/correto_ 1"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/errado_8_alegria"), false, "Esta expressão é de alegria.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/crianca/2/correto_ 2"), true, "")
          }
        }
      );
      game1 = new MiniGameType1(
        "Montando a Face",
        "TODO",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de ",
        new MiniGame1Image[][][]{
          new MiniGame1Image[][]{
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGame1Image[][]{
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGame1Image[][]{
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            }
          }
        }
      );
      game2 = new MiniGameType2(
        "Selecione a Emoção",
        "TODO",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame2"),
        "Arraste as emoções para as caixas corretas. Emoções neutras e de ",
        new MiniGame2Image[][]{
          new MiniGame2Image[4]{
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_00"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/correto_0"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_01"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/0/errado_02"), false, "")
          },
          new MiniGame2Image[8] {
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_10"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/correto_10"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_11"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_12"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_13"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/correto_11"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_14"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/1/errado_15"), false, "")
          },
          new MiniGame2Image[12] {
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_20"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_21"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_25"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_22"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_20"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_23"), true, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_24"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_21"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_28"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_26"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/errado_27"), false, ""),
            new MiniGame2Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/crianca/2/correto_22"), false, "")
          }
        }
      );
    }
    else if (selectedSpecies == 1)
    {
      initialTexts = new string[4]{
        "Olá! Este é o Alfred. Ele vem de um planeta muito distante chamado Ogle-TR e foi escolhido, entre muitos, para passar algumas horas aqui no Planeta Terra para virar um mestre das emoções! Se ele conseguir alcançar este objetivo, ele poderá retornar ao seu lar e ensinar para seus amigos como eles podem se relacionar melhor.\nNo entanto, o caminho para virar mestre das emoções é muito difícil para um etzinho como Alfred. Ele precisará da sua ajuda.",
        "Você já se perguntou alguma vez o motivo pelo qual temos emoções? E porque choramos? Você já se pegou repensando algo que ia dizer por causa da expressão facial que um amigo fez pra você? E aquele momento em que você foi falar algo em público e ficou com o rosto vermelho? Isso já aconteceu, não é mesmo?\nPois então, cientistas como Darwin já pesquisam isso há muitos anos!\nE é sobre isso que queremos falar hoje, sobre as emoções e as expressões faciais delas.",
        "Alegria, medo, tristeza, raiva, nojo e surpresa. Já ouviu falar? Essas são as emoções consideradas básicas ou primárias. Isso porque existem outras centenas de emoções que chamamos de secundárias, como saudade, ansiedade, tranquilidade, inveja, ciúme, e tantas outras. Mas porque secundárias? Secundárias a que? Àquelas primeiras! As básicas. Isso quer dizer que quando sentimos inveja, por exemplo, existe outra emoção por trás disso. Talvez uma raiva misturada com tristeza. Ou então quando dizemos que estamos ansiosos: isso significa que na verdade o que estamos sentindo primariamente é medo. Medo de algo que pode acontecer em um futuro breve. Entendeu?\nUma forma fácil de saber se uma emoção é primária ou secundária é pensando nas expressões faciais. Quando pensamos em raiva, por exemplo, é fácil imaginar alguém expressando essa emoção através do rosto, não é? Mas quando pensamos em fazer cara de tranquilidade, por exemplo, já não é tão fácil assim, não é mesmo? Isso porque raiva é uma emoção primária e tranquilidade é secundária, como já vimos anteriormente.\nAs emoções primárias tem uma função importante na nossa vida e tiveram um papel muito relevante para a evolução da nossa espécie. Vamos aprender um pouco mais sobre cada uma delas e virar mestre das emoções?",
        "Estamos na Fazenda das Emoções. Nesta fazenda, 6 baús escondem segredos e premiações sobre cada uma das seis emoções básicas. Vença as tarefas escondidas e acumule pontos até se tornar o verdadeiro mestre das emoções.\n\nQual será o primeiro baú?"
      };
      finalTexts = new string[1]{
        "Pronto! Agora já passamos por todas as emoções básicas e aprendemos sobre elas. Você ganhou o título de Mestre das Emoções e auxiliou Alfred a entender tudo sobre as emoções! Obrigado pela ajuda e espero que o treinamento tenha contribuído para o seu crescimento assim como contribuiu para o Alfred ser um etzinho melhor! Esperamos que você se sinta mais apto a reconhecer e entender as emoções e expressões faciais no seu contexto do dia-a-dia."
      };
      CHESTS_TEXT = new string[6]{
        "<b>Alegria</b> é a emoção que mais gostamos de sentir. Ela aparece quando sentimos prazer em algo; e por isso, acaba guiando nossas escolhas e decisões. Ela faz com que sintamos que nossa vida vale a pena e, através do sorriso, podemos transmitir socialmente nosso contentamento. Além disso, a alegria nos ajuda na criação de laços afetivos desde que somos bebês; pois quando sorrimos recebemos mais atenção e afeto de quem está a nossa volta.",
        "A <b>tristeza</b> é a emoção que mais evitamos sentir, afinal, ninguém gosta de ficar triste. Porem, essa é uma emoção tão importante quanto todas as outras. Desde que somos bebês ela nos ajuda na comunicação dos nossos desconfortos, através do choro. Depois que aprendemos a falar, ela continua nos ajudando a aprender com nossos erros e a pensarmos de forma mais profunda e criativa. É a tristeza que nos move a introspecção, a pensarmos melhor sobre os caminhos que a vida nos oferece e a nos colocarmos no lugar do outro. Além disso, o choro, manifestação da tristeza, também facilita a busca por apoio, já que quando choramos aumentamos a probabilidade de chamar a atenção de alguém que possa nos ajudar. Sendo assim, podemos dizer que quando evitamos sentir tristeza a qualquer custo, também nos afastamos de nós mesmos, somos menos autênticos e podemos até nos distanciarmos de quem amamos.",
        "O <b>medo</b> é a emoção que mais sentimos de forma física. Quando o nosso sistema dessa emoção se ativa, os nossos pensamentos quase que se bloqueiam e utilizamos o sistema de luta ou suga. O nosso corpo se prepara pra reagir antes mesmo de pensarmos o que seria mais correto. Viram como nosso corpo é inteligente? É por isso que muitas vezes podemos fazer coisas estúpidas quando estamos com medo. Porque nos preparamos pra lutar, pra fugir ou pra congelar. Nesse sentido, podemos dizer que quando estamos com medo nós sentimos, agimos e só depois pensamos no que aconteceu.",
        "O <b>nojo</b> existe pra nos prevenir do risco de contaminação. É ele que nos impede de comer alimentos que poderiam ser tóxicos ou coisas que nos fariam mal. Ele nos protege de bactérias e de doenças e começa a se desenvolver exatamente quando a criança está se preparando pra caminhar; por volta dos dois anos. Não faz todo sentido? Isso porque até ali o bebê era exclusivamente dependente do fornecimento de alimento e cuidados maternos. É por isso que bebês pequenos parecem não sentir nojo de colocar nada na boca. Mas falando nisso, você sabia que existem dois tipos de nojo?\n\nIsso mesmo. Além deste nojo que estávamos falando, existe também o nojo moral; que é aquele nojo que sentimos de um político corrupto ou de um pedófilo, por exemplo, ou de alguém que consideramos que fez algo muito errado. E sabe o que é mais interessante? Que a nossa reação frente aos dois tipos de nojo é a mesma, a de evitação. Queremos nos afastar daquele objeto que nos desperta essa emoção e nossa expressão facial também é a mesma quando estamos falando de algo muito nojento ou de uma pessoa que julgamos da mesma forma. Isso porque o sistema ativado é o mesmo. ",
        "O papel evolutivo da <b>raiva</b> está relacionado a situações nas quais temos que avaliar o custo-benefício de algo. Quando nos sentimos de alguma forma prejudicados, por exemplo, e queremos mostrar que não devemos ser colocados naquela posição novamente. É isso que acontece naquelas momentos que passamos por uma situação desconfortável e depois ficamos um tempão pensando no que devíamos ter respondido pra aquela pessoa que nos desrespeitou de alguma forma.",
        "A <b>surpresa</b> tem bastante a ver com o medo. Inclusive, quando somos crianças, essas são as emoções mais difíceis de se distinguir. É por isso que muitas vezes uma criança pode chorar de susto, mesmo que seja uma surpresa boa, como uma festa de aniversário. Quando sentimos surpresa é como se o nosso corpo se preparasse pra sentir medo, mas o nosso pensamento nos diz que nada de ruim vai acontecer. É como um medo bom. É por isso também, por ser um sistema muito rápido a ser ativado, que muitas vezes não conseguimos fingir gostar de algo que não gostamos quando somos pegos de surpresa.",
      };
      gameType0Alegria = new MiniGameType0(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de ",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame0"),
        "Selecione apenas as expressões faciais de ",
        new MiniGame0Image[][]{
          new MiniGame0Image[8]{
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_3"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_6"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_9"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_11"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_12"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_18"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/0/errado_20"), false, "Esta expressão é neutra.\n")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_19"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_25"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_27"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_21"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_29"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_23"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_24"), false, "Esta expressão é de raiva.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_17"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_26"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_22"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/errado_28"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/1/correto_0"), true, "")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_2"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_13"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_14"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_10"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_0"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_1"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_16"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_7"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_5"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_8"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Alegria/adulto/2/errado_15"), false, "Esta expressão é de raiva.\n")
          }
        }
      );
      gameType0Raiva = new MiniGameType0(
        "Selecione a Emoção",
        "Neste jogo, você encontrará diferentes expressões faciais de uma ou mais emoções. Selecione apenas as expressões faciais de ",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame0"),
        "Selecione apenas as expressões faciais de ",
        new MiniGame0Image[][]{
          new MiniGame0Image[8]{
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_0_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_5_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_6_surpresa"), false, "Esta expressão é surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/0/errado_4_nojo"), false, "Esta expressão é de nojo.\n")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_7_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_4_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_9_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_8_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_5_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_10_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_6_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/1/errado_0_medo"), false, "Esta expressão é de medo.\n")
          },
          new MiniGame0Image[12] {
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_6_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_2_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_8_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/correto_0"), true, ""),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_1_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_5_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_3_neutra"), false, "Esta expressão é neutra.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_9_surpresa"), false, "Esta expressão é de surpresa.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_7_tristeza"), false, "Esta expressão é de tristeza.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_0_medo"), false, "Esta expressão é de medo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_4_nojo"), false, "Esta expressão é de nojo.\n"),
            new MiniGame0Image(Resources.Load<Sprite>("MiniGame0Images/Raiva/adulto/2/errado_10_surpresa"), false, "Esta expressão é de surpresa.\n")
          }
        }
      );
      game1 = new MiniGameType1(
        "Montando a Face",
        "TODO",
        GameObject.Find("MinigameCanvas").transform.Find("Image/Scroll View/Viewport/Content/MiniGame1"),
        "Monte uma expressão de ",
        new MiniGame1Image[][][]{
          new MiniGame1Image[][]{
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[2]{
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGame1Image[][]{
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[3] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), true, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente.")
            }
          },
          new MiniGame1Image[][]{
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            },
            new MiniGame1Image[4] {
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/1"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/2"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/3"), false, "Que pena, você errou! Tente novamente."),
              new MiniGame1Image(Resources.Load<Sprite>("MiniGame1Images/0"), true, "Que pena, você errou! Tente novamente.")
            }
          }
        }
      );
    }
    EMOTIONS = new Emotion[6]{
      new Emotion(CHESTS_TITLE[0], CHESTS_TEXT[0], gameType0Alegria),
      new Emotion(CHESTS_TITLE[1], CHESTS_TEXT[1], game2),
      new Emotion(CHESTS_TITLE[2], CHESTS_TEXT[2], game2),
      new Emotion(CHESTS_TITLE[3], CHESTS_TEXT[3], game1),
      new Emotion(CHESTS_TITLE[4], CHESTS_TEXT[4], gameType0Raiva),
      new Emotion(CHESTS_TITLE[5], CHESTS_TEXT[5], game1),
    };
  }

  void Awake() {
    DontDestroyOnLoad(transform.gameObject);
  }
}

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

abstract public class MiniGame
{
  public string name;
  public string explanation;
  public string shortExplanation;
  public Transform sceneElement;
  protected int currentChallenge = 0;

  public MiniGame(string name, string explanation, Transform sceneElement, string shortExplanation)
  {
    this.name = name;
    this.explanation = explanation;
    this.sceneElement = sceneElement;
    this.shortExplanation = shortExplanation;
  }

  public void SetUIExplanationText(string emotionName)
  {
    sceneElement.Find("miniGameExplanation").gameObject.SetActive(true);
    sceneElement.Find("miniGameExplanation").GetComponent<Text>().text = this.explanation + emotionName.ToLower() + ".";
  }

  public void HideUIExplanation()
  {
    sceneElement.Find("miniGameExplanation").gameObject.SetActive(false);
  }

  public void ShowMiniGame()
  {
    sceneElement.Find("MiniGame").gameObject.SetActive(true);
  }

  public void SetShortExplanation(string emotionName)
  {
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(true);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").GetComponent<Text>().text = shortExplanation + emotionName.ToLower() + ".";
  }

  abstract public MiniGameResponse ValidateAnswear();
  abstract public bool HasNextChallenge();
  abstract public void NextChallenge();

  abstract public void SetupMiniGame();
  abstract public void FinishGame();
  abstract public void ClearImagesColors();
}

public class MiniGame0Image
{
  public Sprite image;
  public bool isCorrect;
  public string wrongMessage;

  public MiniGame0Image(Sprite image, bool isCorrect, string wrongMessage)
  {
    this.image = image;
    this.isCorrect = isCorrect;
    this.wrongMessage = wrongMessage;
  }
}

public class MiniGame1Image
{
  public Sprite image;
  public bool isCorrect;
  public string wrongMessage;

  public MiniGame1Image(Sprite image, bool isCorrect, string wrongMessage)
  {
    this.image = image;
    this.isCorrect = isCorrect;
    this.wrongMessage = wrongMessage;
  }
}

public class MiniGame2Image
{
  public Sprite image;
  public bool isCorrect;
  public string wrongMessage;

  public MiniGame2Image(Sprite image, bool isCorrect, string wrongMessage)
  {
    this.image = image;
    this.isCorrect = isCorrect;
    this.wrongMessage = wrongMessage;
  }
}

public class MiniGameType0: MiniGame
{
  public MiniGame0Image[][] images;

  public MiniGameType0(string name, string explanation, Transform sceneElement, string shortExplanation, MiniGame0Image[][] images) : base(name, explanation, sceneElement, shortExplanation)
  {
    this.images = images;
  }

  public override void SetupMiniGame()
  {
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    int i = 0;
    while (i < images[this.currentChallenge].Length)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(true);
      sceneElement.Find("MiniGame/image" + i).GetComponent<Image>().sprite = images[this.currentChallenge][i].image;
      i++;
    }
    while (i < 12)
    {
      sceneElement.Find("MiniGame/image" + i).gameObject.SetActive(false);
      i++;
    }
  }

  public override bool HasNextChallenge()
  {
    return this.currentChallenge < (images.Length - 1);
  }

  public override void NextChallenge()
  {
    this.currentChallenge++;
    SetupMiniGame();
  }

  public override void FinishGame()
  {
    sceneElement.Find("MiniGame").gameObject.SetActive(false);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(false);
    this.currentChallenge = 0;
  }

  public override MiniGameResponse ValidateAnswear()
  {
    if (ImageSelection.selectedImage0 == PlayerInfo.NOT_SELECTED_ANSWEAR)
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Selecione uma imagem!");
    }
    else if (images[this.currentChallenge][ImageSelection.selectedImage0].isCorrect)
    {
      return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
    }
    return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, images[this.currentChallenge][ImageSelection.selectedImage0].wrongMessage);
  }

  public override void ClearImagesColors()
  {
    for (int i = 0; i < images[this.currentChallenge].Length; i++)
      ImageSelection.SetImageColor(i, new Color(255, 255, 255, 255));
  }
}

public class MiniGameType1 : MiniGame
{
  public MiniGame1Image[][][] images;

  public MiniGameType1(string name, string explanation, Transform sceneElement, string shortExplanation, MiniGame1Image[][][] images) : base(name, explanation, sceneElement, shortExplanation)
  {
    this.images = images;
  }

  public override void SetupMiniGame()
  {
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage1 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    ImageSelection.selectedImage2 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    int i = 0;

    for (int j = 0; j < 3; j++)
    {
      while (i < images[this.currentChallenge][j].Length)
      {
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).gameObject.SetActive(true);
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).GetComponent<Image>().sprite = images[this.currentChallenge][j][i].image;
        i++;
      }
      while (i < 4)
      {
        sceneElement.Find("MiniGame/bodyPart" + j + "/image" + i).gameObject.SetActive(false);
        i++;
      }
      i = 0;
    }
  }

  public override bool HasNextChallenge()
  {
    return this.currentChallenge < (images.Length - 1);
  }

  public override void NextChallenge()
  {
    this.currentChallenge++;
    SetupMiniGame();
  }

  public override void FinishGame()
  {
    sceneElement.Find("MiniGame").gameObject.SetActive(false);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(false);
    this.currentChallenge = 0;
  }

  public override MiniGameResponse ValidateAnswear()
  {
    if ((ImageSelection.selectedImage0 == PlayerInfo.NOT_SELECTED_ANSWEAR) || (ImageSelection.selectedImage1 == PlayerInfo.NOT_SELECTED_ANSWEAR) || (ImageSelection.selectedImage2 == PlayerInfo.NOT_SELECTED_ANSWEAR))
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Selecione uma de cada categoria!");
    }
    else if (images[this.currentChallenge][0][ImageSelection.selectedImage0].isCorrect && images[this.currentChallenge][1][ImageSelection.selectedImage1].isCorrect && images[this.currentChallenge][2][ImageSelection.selectedImage2].isCorrect)
    {
      return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
    }
    return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Tente novamente.");
  }

  public override void ClearImagesColors()
  {
    for (int i = 0; i < 3; i++)
      for (int j = 0; j < images[this.currentChallenge][i].Length; j++)
        ImageSelection.SetImageOfMultiplesColor(i, j, new Color(255, 255, 255, 255));
  }
}

public class MiniGameType2 : MiniGame
{
  public MiniGame2Image[][] images;
  GameObject imageCellGameObject;

  public MiniGameType2(string name, string explanation, Transform sceneElement, string shortExplanation, MiniGame2Image[][] images) : base(name, explanation, sceneElement, shortExplanation)
  {
    this.images = images;
    imageCellGameObject = sceneElement.Find("MiniGame").Find("imageCell").gameObject;
  }

  private void cleanCells() {
    for (int i = 0; i < 12; i++) {
      Transform cellContainer = sceneElement.Find("MiniGame/Images/image" + i);
      if (cellContainer.childCount == 0)
      {
        GameObject newImageCell = GameObject.Instantiate(imageCellGameObject, cellContainer);
        newImageCell.name = "image" + i;
        newImageCell.transform.localPosition = new Vector3(0, 0, 0);
      }

      Transform cellEmotionsContainer = sceneElement.Find("MiniGame/Emotions/EmotionsContainer/Images/image" + i);
      if (cellEmotionsContainer.childCount > 0)
      {
        foreach (Transform emotionCell in cellEmotionsContainer)
        {
          GameObject.Destroy(emotionCell.gameObject);
        }
      }

      Transform cellNeutralContainer = sceneElement.Find("MiniGame/Emotions/NeutralContainer/Images/image" + i);
      if (cellNeutralContainer.childCount > 0)
      {
        foreach (Transform emotionCell in cellNeutralContainer)
        {
          GameObject.Destroy(emotionCell.gameObject);
        }
      }
    }
  }

  public override void SetupMiniGame()
  {
    ImageSelection.selectedImage0 = PlayerInfo.NOT_SELECTED_ANSWEAR;
    cleanCells();
    int i = 0;
    while (i < images[this.currentChallenge].Length)
    {
      Transform cellContainer = sceneElement.Find("MiniGame/Images/image" + i);
      Transform cellItem = cellContainer.Find("image" + i);
      cellItem.gameObject.SetActive(true);
      cellItem.GetComponent<Image>().sprite = images[this.currentChallenge][i].image;
      cellItem.GetComponent<DraggableImageCellInfo>().isMainEmotion = images[this.currentChallenge][i].isCorrect;
      i++;
    }
    while (i < 12)
    {
      sceneElement.Find("MiniGame/Images/image" + i + "/image" + i).gameObject.SetActive(false);
      i++;
    }
  }

  public override bool HasNextChallenge()
  {
    return this.currentChallenge < (images.Length - 1);
  }

  public override void NextChallenge()
  {
    this.currentChallenge++;
    SetupMiniGame();
  }

  public override void FinishGame()
  {
    cleanCells();
    sceneElement.Find("MiniGame").gameObject.SetActive(false);
    GameObject.Find("MinigameCanvas/Image/shortExplanation").gameObject.SetActive(false);
    this.currentChallenge = 0;
  }

  private bool isCellBeingUsed(Transform cell) {
    if (cell.childCount == 0) {
      return false;
    }

    Transform[] cellChildren = new Transform[cell.childCount];
    for (int i = 0; i < cell.childCount; i++)
    {
      cellChildren[i] = cell.GetChild(i);
    }

    return Array.FindIndex(cellChildren, child => child.gameObject.activeSelf) != -1;
  }

  private static Transform[] getChildren(Transform originalObject) {
    Transform[] arrayToReturn = new Transform[originalObject.childCount];
    for (int i = 0; i < originalObject.childCount; i++)
    {
      arrayToReturn[i] = originalObject.GetChild(i);
    }

    return arrayToReturn;
  }

  private static bool isCellWrong(Transform cell) {
    return cell.childCount > 0 && !cell.GetChild(0).GetComponent<DraggableImageCellInfo>().isMainEmotion;
  }

  public override MiniGameResponse ValidateAnswear()
  {
    Transform[] imagesCellContainers = getChildren(sceneElement.Find("MiniGame/Images"));
    if (Array.FindIndex(imagesCellContainers, cell => isCellBeingUsed(cell)) != -1)
    {
      return new MiniGameResponse(PlayerInfo.NOT_SELECTED_ANSWEAR, "Separe todas as imagens arrastando!");
    }

    Transform[] answearImagesCellContainers = getChildren(sceneElement.Find("MiniGame/Emotions/EmotionsContainer/Images"));
    if (Array.FindIndex(answearImagesCellContainers, cell => isCellWrong(cell)) != -1)
    {
      return new MiniGameResponse(PlayerInfo.WRONG_ANSWEAR, "Pelo menos uma imagem foi colocada no grupo errado. Tente novamente!");
    }
    return new MiniGameResponse(PlayerInfo.CORRECT_ANSWEAR, "Parabéns! Você acertou!");
  }

  public override void ClearImagesColors() {  }
}

public class MiniGameResponse
{
  public int code;
  public string message;

  public MiniGameResponse(int code, string message)
  {
    this.code = code;
    this.message = message;
  }
}
