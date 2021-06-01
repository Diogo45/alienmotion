using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{

    [field: SerializeField] public AudioSource _musicAudioSource { get; private set; }
    //[field: SerializeField] public AudioSource _narrationAudioSource { get; private set; }
    [field: SerializeField] public AudioSource _sfxAudioSource { get; private set; }

    [SerializeField] private AudioAsset _buttonClicks;
    [SerializeField] private AudioAsset _music;
    [SerializeField] private AudioAsset _walk;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (EmotionHuntersController.instance && EmotionHuntersController.instance.IsIntervention)
        {
            _musicAudioSource.Stop();
            return;
        }

        if (!_musicAudioSource.isPlaying)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = _music.Play();
            _musicAudioSource.Play();
        }

    }

    // Update is called once per frame
    public void ButtonClickSound()
    {
        _sfxAudioSource.Stop();
        _sfxAudioSource.PlayOneShot(_buttonClicks.Play());
    }
}
