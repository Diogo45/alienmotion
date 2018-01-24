using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadMapOnSpeciesSelection: MonoBehaviour {
    public void LoadScene(int species)
    {
        SceneManager.LoadScene(1);
        PlayerInfo.selectedSpecies = species;
    }
}
