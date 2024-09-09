using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("-----------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    public AudioSource sFXSource;

    [Header("-----------Audio Clip------------")]
    public AudioClip backGround;
    public AudioClip bossFight;
    public AudioClip footStep;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicBG();
    }

    public void PlayMusicBG()
    {
        if (musicSource.isPlaying) return;
        musicSource.clip = backGround;
        musicSource.Play();
    }

    public void StopMusicBG()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlayBossFight()
    {
        if (musicSource.isPlaying) return;
        musicSource.clip = bossFight;
        musicSource.Play();
    }

    public void StopBossFight()
    {
        if(musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }


    public void PlayFootStep()
    {
        if(sFXSource.isPlaying) return;
        sFXSource.clip = footStep;
        sFXSource.Play();
    }

    public void StopFootStep()
    {
        if(sFXSource.isPlaying)
        {
            sFXSource.Stop();
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sFXSource.volume = volume;
    }
}
