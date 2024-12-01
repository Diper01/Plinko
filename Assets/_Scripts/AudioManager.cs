using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource musicSource;
    private List<AudioSource> sfxSources;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        InitializeAudioSources();
        InitializeSliders();
    }

    private void InitializeAudioSources()
    {
        var allAudioSources = FindObjectsOfType<AudioSource>().ToList();
        musicSource = allAudioSources.FirstOrDefault(source => source.gameObject.CompareTag("Music"));
        sfxSources = allAudioSources.Where(source => source != musicSource).ToList();
    }

    private void InitializeSliders()
    {
        float defaultMusicVolume = GetSavedVolume(MusicVolumeKey, 0.5f);
        float defaultSFXVolume = GetSavedVolume(SFXVolumeKey, 0.5f);

        SetMusicVolume(defaultMusicVolume);
        SetSFXVolume(defaultSFXVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        musicSlider.value = defaultMusicVolume;
        sfxSlider.value = defaultSFXVolume;
    }

    private float GetSavedVolume(string key, float defaultValue) => PlayerPrefs.GetFloat(key, defaultValue);


    private void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveVolume(MusicVolumeKey, volume);
    }

    private void SetSFXVolume(float volume)
    {
        sfxSources.ForEach(s => s.volume = volume);
        SaveVolume(SFXVolumeKey, volume);
    }

    private void SaveVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
    }
}