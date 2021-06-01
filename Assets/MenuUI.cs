using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUI : Singleton<MenuUI>
{

    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _account;

  

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();


    }

    public void FromTo(string fromTo)
    {
        string s_from = fromTo.Substring(0, 1);
        string s_to = fromTo.Substring(1, 1);

        int from = int.Parse(s_from);
        int to = int.Parse(s_to);

        switch (from)
        {
            case 0: _play.SetActive(false);
                break;
            case 1:
                _options.SetActive(false);
                break;
            case 2:
                _account.SetActive(false);
                break;

        }

        switch (to)
        {
            case 0:
                _play.SetActive(true);
                break;
            case 1:
                _options.SetActive(true);
                break;
            case 2:
                _account.SetActive(true);
                break;

        }

    }

   

}
