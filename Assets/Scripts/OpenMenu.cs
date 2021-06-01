using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{

    [SerializeField] private GameObject _optionsMenu;


    private bool _on = false;

    public void OpenClose()
    {
        if (_on)
            _optionsMenu.SetActive(false);
        else
            _optionsMenu.SetActive(true);

        _on = !_on;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose();
        }
    }
}
