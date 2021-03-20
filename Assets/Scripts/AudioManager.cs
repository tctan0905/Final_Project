using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource effect;
    private float musicVolume = 1f;
    private float effectVolume = 1f;

    private void Awake()
    {
        audioSource.Play();
    }
    private void Update()
    {
        audioSource.volume = musicVolume;
       effect.volume = effectVolume;
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
      
    }
    public void UpdateEfxVolume(float volume)
    {
        effectVolume = volume;

    }

}
