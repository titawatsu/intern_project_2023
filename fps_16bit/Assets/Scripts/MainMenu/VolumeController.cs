using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public const string music_mixer = "Music";
    public const string sfx_mixer = "SoundEffect";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.music_key, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.sfx_key, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.music_key, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.sfx_key, musicSlider.value);
    }

    void SetMusicVolume(float value)
    {
        masterMixer.SetFloat(music_mixer, Mathf.Log10(value) * 20);
    }

    void SetSfxVolume(float value)
    {
        masterMixer.SetFloat(sfx_mixer, Mathf.Log10(value) * 20);
    }
}
