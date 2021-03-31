using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectPref = "SoundEffectPref";

    private int i_FirstPlay;
    public Slider slider_Background, slider_SoundEffects;
    private float fl_Background, fl_soundEffects;
    public AudioSource backgroundAudio;
    public AudioSource[] effectAudio;
    //private float musicVolume = 1f;
    //private float effectVolume = 1f;

    private void Awake()
    {
        i_FirstPlay = PlayerPrefs.GetInt(FirstPlay);
        if(i_FirstPlay == 0)
        {
            fl_Background = 0.25f;
            fl_soundEffects = 0.75f;
            slider_Background.value = fl_Background;
            slider_SoundEffects.value = fl_soundEffects;

            PlayerPrefs.SetFloat(BackgroundPref, fl_Background);
            PlayerPrefs.SetFloat(SoundEffectPref, fl_soundEffects);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            fl_Background = PlayerPrefs.GetFloat(BackgroundPref);
            slider_Background.value = fl_Background;
            fl_soundEffects = PlayerPrefs.GetFloat(SoundEffectPref);
            slider_SoundEffects.value = fl_soundEffects;
        }

        //audioSource.Play();
    }
    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BackgroundPref, fl_Background);
        PlayerPrefs.SetFloat(SoundEffectPref, fl_soundEffects);

    }
    private void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSetting();
        }
    }
    public void UpdateSound()
    {
        backgroundAudio.volume = slider_Background.value;
        for(int i =0; i < effectAudio.Length;i--)
        {
            effectAudio[i].volume = slider_SoundEffects.value;
        }
    }
    private void Update()
    {
       // audioSource.volume = musicVolume;
       //effect.volume = effectVolume;
    }


    public void UpdateVolume(float volume)
    {

       // musicVolume = volume;
      
    }
    public void UpdateEfxVolume(float volume)
    {
        //effectVolume = volume;

    }

}
