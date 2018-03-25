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
  public const int WRONG_ANSWEAR = -2;
  public const int NOT_SELECTED_ANSWEAR = -1;
  public const int CORRECT_ANSWEAR = 1;

  public static string[] CHESTS_TITLE = new string[6]{
    "Alegria",
    "Tristeza",
    "Medo",
    "Nojo",
    "Raiva",
    "Surpresa",
  };

  public static string[] CHESTS_TEXT = new string[6]{
    "Emoção 1	Lorem ipsum mi diam non luctus egestas mollis turpis aenean pretium, inceptos luctus arcu ullamcorper suspendisse ultricies semper pulvinar ipsum platea, id lacus massa aenean auctor morbi per in nisi. suscipit habitant vulputate dictum a consequat adipiscing tellus eros inceptos, tellus augue tortor habitant cubilia class tempus enim venenatis quisque, aenean quisque at urna lobortis est erat quam. tempus magna iaculis viverra ut metus luctus etiam ligula, auctor sit tristique amet vestibulum rutrum fames, taciti urna fames himenaeos aliquam senectus mattis. eget mi pulvinar gravida turpis suspendisse nunc, bibendum integer pulvinar pellentesque etiam aliquet faucibus, accumsan cras faucibus turpis taciti. ",
    "Emoção 2	Lorem ipsum class hac turpis consequat litora tempor etiam, convallis fusce senectus sollicitudin porta id ligula, proin adipiscing iaculis dictum amet neque tempus. lacus ut sapien sem orci varius iaculis, vulputate cras sem vel quisque. placerat sem consequat nec id ultricies dapibus neque fusce litora libero, imperdiet dictumst proin donec imperdiet pellentesque primis congue sed, nam lectus elementum rutrum curabitur feugiat accumsan eleifend nisi. nam quis mi nunc mollis risus facilisis mauris pharetra placerat varius, duis cubilia vitae varius viverra neque venenatis consectetur ut, posuere integer mi inceptos etiam elit libero ipsum pulvinar. \n\nMauris pulvinar urna sociosqu sapien enim dapibus tristique velit, suscipit mattis nullam tristique vel eros pretium, ornare etiam metus tempus elit tempor urna. accumsan donec sem class pharetra interdum et turpis, facilisis ante eget potenti felis consectetur, purus nisl integer etiam sem litora. lectus porttitor porta enim primis, tortor netus fames, amet curabitur himenaeos. euismod interdum sociosqu tellus enim placerat magna maecenas donec blandit facilisis habitasse, dictum aliquet elit amet erat elit quisque praesent duis ultricies. donec ultricies euismod augue est nisl vulputate turpis felis luctus feugiat, nisl ligula vehicula nunc augue ante porta adipiscing eget ac sapien, sagittis maecenas aliquet amet egestas ipsum elementum accumsan proin. ",
    "Emoção 3	Lorem ipsum habitasse etiam egestas volutpat suspendisse vulputate pulvinar fames ornare nec vulputate, himenaeos turpis purus nibh bibendum hendrerit nunc malesuada turpis massa. semper cursus quisque sit etiam non quisque sem id magna taciti, adipiscing orci nullam nisl porttitor tincidunt id duis. ultricies dapibus semper congue lacinia non per cursus posuere rutrum, class etiam morbi lorem eu rhoncus aenean fames, euismod purus tellus faucibus ut habitant dictumst aenean. sem blandit hac a nec torquent scelerisque taciti, purus faucibus donec nam nec arcu felis, urna torquent imperdiet lobortis odio facilisis. potenti magna euismod inceptos id fringilla pharetra et in ullamcorper, viverra dui proin potenti conubia venenatis lectus at, sapien pulvinar integer dui molestie vestibulum ullamcorper posuere. \n\nDonec auctor neque lacus a mattis pretium vestibulum cursus ante arcu lacus scelerisque dictum ligula donec, habitant cubilia euismod nostra consequat curabitur dapibus habitasse vestibulum turpis luctus etiam pellentesque dictumst. vitae maecenas vulputate dapibus tellus fusce ullamcorper est litora sed sodales tellus id, laoreet metus aenean rhoncus est cursus sodales praesent fermentum amet nullam. elit bibendum eget suspendisse pretium viverra dolor hac ipsum fringilla, dictum lorem ut justo nostra nam hendrerit consequat nunc, condimentum odio vitae lorem volutpat placerat nisi fringilla. in pulvinar laoreet potenti cras amet curabitur quisque, pellentesque semper lacinia fusce elementum accumsan, auctor praesent nostra rhoncus nulla in. \n\nDui gravida curae vel aenean a ipsum lectus curae, elit condimentum rhoncus ultrices et hac magna vel, est nisl ac tortor felis laoreet vestibulum. cubilia quisque lacus ultrices senectus aliquam hac cras, ante conubia viverra hendrerit egestas neque libero massa, odio viverra quis fringilla donec velit. ultrices ut vitae mollis arcu aptent mattis ultrices aenean, torquent sapien fusce ullamcorper gravida purus morbi viverra sagittis, pretium viverra hac pellentesque cubilia sagittis justo. nulla nullam habitant placerat mi ad cubilia posuere, erat habitasse lorem torquent porttitor. ",
    "Emoção 4 Lorem ipsum tortor integer luctus porta amet varius, bibendum elementum sit pellentesque vel fusce tellus imperdiet, iaculis quisque a aliquet est blandit. maecenas quisque scelerisque eros nisi arcu fames facilisis potenti, porttitor velit porta in porta eros fusce phasellus, diam augue eget mauris netus nisl lacus. vitae integer quam porttitor risus donec varius urna lacinia orci suspendisse donec, lorem per rhoncus vulputate felis eros leo vitae et lacus. fames mauris nibh vitae sagittis aliquet aptent cursus, in pretium nisi tortor interdum cursus. ligula purus placerat tristique tellus volutpat eleifend duis suscipit orci auctor libero, quisque egestas purus maecenas habitant ad ligula aliquet enim morbi, duis lorem tincidunt fusce donec tellus ut nec adipiscing blandit. \n\nLacinia nisi commodo etiam non commodo pretium dictumst nibh tincidunt aenean, ultrices vulputate sapien sollicitudin vestibulum orci accumsan viverra morbi curabitur, pulvinar cras taciti condimentum varius tincidunt at proin erat. vestibulum molestie ultrices scelerisque ad diam curabitur placerat, nostra lacus ipsum sollicitudin metus iaculis blandit sed, lectus velit nostra conubia fames maecenas. fames pretium amet lacinia arcu quis gravida erat commodo, elit non fusce eu leo primis at, convallis elit eget ipsum sagittis consectetur netus. enim mi velit integer leo elit facilisis suspendisse ullamcorper, consectetur torquent purus luctus hac sem turpis ultrices lacinia, platea taciti urna enim metus magna iaculis.\n\nA justo semper cras quisque sociosqu praesent fringilla mi nibh lacus aliquet tortor, quam non mattis libero dui ac sapien nulla aptent mi potenti mauris orci, suspendisse feugiat sollicitudin dapibus aliquam gravida nisi ligula sociosqu nisi class. placerat ut nullam justo cursus pretium non accumsan enim vehicula luctus mi convallis rhoncus mi torquent aenean, blandit taciti sit tellus nunc ultrices tempor id duis nulla mauris molestie neque quisque fames.aliquam hendrerit platea cursus ipsum quisque diam, sagittis metus aenean taciti lacus magna vivamus, a curabitur dictum hac laoreet.varius cursus eleifend platea sagittis adipiscing urna rutrum interdum accumsan lorem, aptent ipsum luctus rhoncus volutpat fringilla laoreet dapibus blandit, convallis lacus vivamus senectus ac curabitur mollis leo mollis.\n\nEgestas luctus aliquam himenaeos proin eros, ac suscipit integer convallis, rhoncus torquent congue lectus.aenean morbi ultrices proin vel vivamus eleifend aliquet fermentum euismod, litora maecenas congue accumsan fusce enim ultricies mollis, est duis etiam fringilla aenean semper sed nostra. sagittis nec sem curae odio habitasse accumsan varius eget, tortor ut senectus enim taciti quis tortor, ipsum augue aliquet tristique donec lacinia ac.eget metus dictum nulla, vulputate purus. ",
    "Emoção 5 Lorem ipsum neque placerat dolor lectus arcu elementum, amet pretium et sapien iaculis condimentum, pretium imperdiet duis volutpat ad aliquam. aptent non primis himenaeos fermentum ut duis primis netus libero sagittis risus varius, cursus morbi tempus tincidunt integer justo lacinia elit vehicula hendrerit euismod, vitae suspendisse lacinia habitasse at non dolor risus neque varius hac. diam ut accumsan pharetra est turpis congue dolor ligula rutrum consectetur at, habitasse ut magna cubilia vivamus donec velit posuere placerat. nec proin molestie sed conubia porta accumsan commodo quisque volutpat fermentum lobortis morbi, accumsan dui nibh risus metus posuere sapien luctus sem urna neque. \n\nIaculis ultricies curabitur neque arcu dictum adipiscing congue egestas semper ad lacus fusce at, lacus ultrices dui tempus turpis aenean erat libero ac fames donec. nisi commodo egestas torquent quis class iaculis eleifend tortor, gravida nisi a vel inceptos felis in risus sollicitudin, gravida est quisque pharetra aliquam primis at.ultricies facilisis sodales aenean sollicitudin inceptos at, phasellus leo vel suscipit turpis ornare maecenas, sit praesent quisque feugiat per.mattis accumsan pellentesque posuere fames accumsan lacus per euismod bibendum, nam placerat sollicitudin habitant erat semper vestibulum platea, posuere amet eleifend pretium conubia et velit placerat. \n\nDictum curae primis curabitur justo lacus euismod quis felis pharetra auctor, suspendisse urna diam suspendisse tempus dolor et taciti. enim elementum convallis dolor ornare dapibus tincidunt iaculis, mauris duis leo faucibus justo euismod, posuere lobortis odio ut orci neque. est feugiat adipiscing vitae sociosqu lorem in curae phasellus arcu, elementum hendrerit integer ipsum cursus imperdiet semper sapien vehicula neque, augue nunc accumsan turpis at consectetur nisi vehicula. risus accumsan interdum suspendisse sodales urna proin eu primis dui netus, auctor duis sociosqu posuere lorem euismod lectus arcu aliquam vel mi, facilisis erat suspendisse sed curabitur congue nulla libero habitant.\n\nNibh sollicitudin orci fringilla vel donec lectus congue aliquam nec quis aliquam ut risus blandit, arcu dolor imperdiet pulvinar rhoncus litora massa libero facilisis lorem nunc felis cras.dictumst mattis vestibulum libero tempus habitasse urna felis nibh curabitur nec, tristique eget nunc dolor potenti venenatis sodales maecenas sit.augue malesuada justo quisque dictumst lacinia consectetur eu convallis scelerisque dictum velit, viverra pulvinar tempor lacus turpis tempor donec risus lectus a. volutpat tincidunt inceptos eleifend in nec nisl taciti platea, ut molestie curabitur egestas ac felis ultricies sollicitudin, etiam scelerisque augue dapibus consectetur sociosqu imperdiet.\n\nMolestie et imperdiet torquent sed eu dolor sit, interdum elementum inceptos purus tristique nam augue, curabitur duis erat et etiam felis. fusce vivamus turpis enim risus eros ante lacinia aliquam risus aenean accumsan aenean, sagittis condimentum eleifend consequat mollis congue aliquam nibh posuere euismod sapien pulvinar, in neque justo lorem tincidunt dictumst tortor non posuere curabitur venenatis. ac vel metus habitant lobortis sollicitudin ac maecenas senectus egestas, augue risus vitae dapibus rhoncus pulvinar sed ut luctus, integer pretium maecenas varius aliquam aptent facilisis laoreet. sodales class dictum mollis ligula suscipit interdum nec per luctus quis, dictumst faucibus malesuada quam nisl platea proin fusce ultricies, eleifend imperdiet neque dui nulla fames ornare vulputate magna. ",
    "Emoção 6 Lorem ipsum potenti tortor taciti phasellus praesent eget conubia feugiat, integer blandit venenatis integer bibendum ante ullamcorper class cursus lobortis, eu quisque rutrum etiam nisi condimentum vehicula sociosqu. hac lorem orci magna aliquet lectus molestie eget senectus aenean, elementum porta suspendisse enim luctus ad sed. accumsan vel diam aliquam nisi aenean aliquet fames arcu, turpis cubilia erat integer blandit sociosqu mattis class accumsan, commodo tortor lacus cubilia quisque morbi curae. placerat pellentesque rutrum amet malesuada velit, placerat sed taciti habitasse, bibendum dapibus dolor aliquam. tortor consequat cras sollicitudin elit quis taciti tempor facilisis conubia habitasse, faucibus primis donec pulvinar duis est mi est euismod facilisis, semper tincidunt ut congue sodales auctor quis blandit at. \n\nClass posuere aenean enim sit vel ipsum, commodo in purus sapien a donec euismod, dictumst sapien ut vestibulum imperdiet. eros arcu velit tortor magna metus accumsan dapibus, sollicitudin dictumst sollicitudin cubilia nibh convallis, etiam nullam lorem odio eleifend donec. varius quis facilisis feugiat id justo sit amet sit risus aliquam, consectetur vivamus odio velit porta aenean lobortis vestibulum eleifend, nostra netus elementum tristique lobortis habitasse erat egestas nunc. aenean posuere erat sagittis aliquet tincidunt porta purus, ornare aliquet enim dictumst euismod etiam sollicitudin nostra, non ante augue facilisis ad pulvinar.\n\nHabitasse litora ut malesuada et purus quisque ipsum fringilla litora, ut ipsum at justo commodo molestie rutrum himenaeos curabitur gravida, ornare aptent ullamcorper imperdiet eros metus turpis suscipit. tortor fusce aliquam class ante convallis eleifend aliquet egestas eleifend etiam tellus tempor donec, consequat adipiscing porta faucibus elementum pharetra augue himenaeos dapibus est tristique rhoncus.pulvinar ut placerat mauris platea aliquam nullam lacinia, etiam aliquam fermentum dictum vitae facilisis nulla, et nunc id libero aenean torquent. porta per magna vitae etiam placerat iaculis cras conubia, id vestibulum pretium mollis aenean aliquam quisque fermentum ultricies, pharetra massa est maecenas consequat fringilla tellus.\n\nOrci vestibulum nulla aliquam ac cursus ultricies sit diam tellus, scelerisque eu pharetra ut nulla lectus elementum donec in, egestas mauris vulputate aliquam tortor nibh vehicula potenti. integer ornare a dapibus eu vestibulum tristique ac tempus ullamcorper morbi, vivamus nunc gravida at luctus turpis congue venenatis ligula sodales, fames vitae proin lorem aenean congue fringilla fames quam.dui mauris feugiat nostra ornare, amet lobortis justo himenaeos mauris, etiam tellus malesuada.conubia commodo dictumst donec sapien ante iaculis potenti egestas lorem, fames torquent donec auctor mollis convallis ut vulputate. velit cras auctor torquent class conubia lectus, blandit enim diam dictum proin purus sollicitudin, nulla venenatis mauris interdum pulvinar.\n\nMauris libero ut viverra mi tempus varius lectus blandit consectetur ipsum, tempus accumsan torquent blandit sociosqu ante integer quam.aptent vehicula sagittis vel aenean sit per luctus placerat, aenean ullamcorper aenean erat fringilla feugiat ante fames dictum, quisque massa lorem curabitur fames nulla tempor.imperdiet integer dictumst curabitur leo dapibus proin at integer, taciti vel felis aliquam himenaeos vehicula ultrices blandit et, bibendum sodales ipsum accumsan dapibus per nisi.massa morbi varius himenaeos morbi etiam a congue ante, accumsan magna sit sociosqu etiam placerat nisi tortor ornare, molestie suspendisse vitae quisque imperdiet nunc tortor.\n\nAliquam augue himenaeos quisque platea eget porta class ad phasellus sagittis, class luctus libero tristique tincidunt quis sociosqu laoreet facilisis fusce quisque, placerat orci elementum enim vestibulum lectus libero potenti commodo.dolor vulputate tincidunt platea eu volutpat elit donec, cubilia non vel at mi praesent et rhoncus, bibendum turpis donec tempor massa egestas.sodales nulla sem curae donec quam vulputate leo conubia pellentesque interdum, tristique sed sodales arcu faucibus viverra sodales dolor mattis, feugiat pharetra eu quam potenti sagittis sodales mollis curabitur.condimentum pellentesque mi donec faucibus purus amet aliquet, arcu sit aenean semper lacinia. ",
  };

  public static MiniGame gameType0Alegria;
  public static MiniGame gameType0Raiva;
  public static MiniGame game1;
  public static MiniGame game2;
  public static Emotion[] EMOTIONS;

  public static void SetMiniGames()
  {
    if (selectedSpecies == 0)
    {
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
