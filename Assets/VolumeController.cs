using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : Singleton<VolumeController>
{
    [SerializeField] private Slider _globalVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _narrationVolumeSlider;

    [SerializeField] private AudioMixer globalMixer;

    void Awake()
    {
        base.Awake();



        globalMixer.SetFloat("GlobalVolume", Mathf.Log10(PlayerPrefs.GetFloat("GlobalVolume")) * 20f);
        globalMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20f);

        _globalVolumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume");
        _SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _narrationVolumeSlider.value = PlayerPrefs.GetFloat("NarrationVolume");

        _globalVolumeSlider.onValueChanged.AddListener(SetGlobalLevel);
        _SFXVolumeSlider.onValueChanged.AddListener(SetSFXLevel);
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicLevel);
        _narrationVolumeSlider.onValueChanged.AddListener(SetNarrationLevel);

    }

    public void SetGlobalLevel(float sliderValue)
    {
        //Decibels are not linear, they are on a logaritimic scale thats the why of log10(volume) * 20
        globalMixer.SetFloat("GlobalVolume", Mathf.Log10(sliderValue) * 20f);
        PlayerPrefs.SetFloat("GlobalVolume", sliderValue);

    }

    public void SetSFXLevel(float sliderValue)
    {
        globalMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void SetMusicLevel(float sliderValue)
    {
        globalMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetNarrationLevel(float sliderValue)
    {
        globalMixer.SetFloat("NarrationVolume", Mathf.Log10(sliderValue) * 20f);
        PlayerPrefs.SetFloat("NarrationVolume", sliderValue);
    }

}
