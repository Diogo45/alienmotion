using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadMapOnSpeciesSelection: MonoBehaviour {
    public void LoadScene(int species)
    {
        SceneManager.LoadScene(1);
        PlayerInfo.selectedSpecies = species;

        string ageName = "Criança";
        if (species == 1)
        {
            ageName = "Adulto";
        }

        string path = "historico.txt";
        using (var tw = new StreamWriter(path, true))
        {
            tw.WriteLine("\n\nComeço de jogo!\nIdade escolhida: " + ageName);
        }
    }
}
