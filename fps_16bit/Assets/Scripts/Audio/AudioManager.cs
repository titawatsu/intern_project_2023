using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioSource audioSource;
    
    public List<AudioClip> getClips = new List<AudioClip>();
    private static AudioManager instance;

    public const string music_key = "Music";
    public const string sfx_key = "SoundEffect";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolumeData();
    }
    
    public void GetSoundEffect()
    {
        AudioClip clip = getClips[UnityEngine.Random.Range(0, getClips.Count)];
        
        audioSource.PlayOneShot(clip);
    }

    private void LoadVolumeData()
    {
        float musicVolume = PlayerPrefs.GetFloat(music_key,1f);
        float sfxVolume = PlayerPrefs.GetFloat(sfx_key,1f);

        masterMixer.SetFloat(VolumeController.music_mixer, Mathf.Log10(musicVolume * 20));
        masterMixer.SetFloat(VolumeController.sfx_mixer, Mathf.Log10(sfxVolume * 20));
    }
}
