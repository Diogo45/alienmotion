using Questionnaire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationManager : MonoBehaviour
{

    [SerializeField] private ScreenManager _screenManager;
    [SerializeField] private AudioSource _audioSource;


    [SerializeField] private List<AudioClip> _narrationClips;

    private int currentClip = -1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_screenManager._currentScreen >= _narrationClips.Count)
        {
            _audioSource.Stop();
            return;

        }

        if (_screenManager._currentScreen != currentClip)
        {
            
            _audioSource.clip = _narrationClips[_screenManager._currentScreen];
            _audioSource.Play();


            //_audioSource.time = _narrationClips[_screenManager._currentScreen].length * 0.05f;
            currentClip = _screenManager._currentScreen;
        }
    }
}
