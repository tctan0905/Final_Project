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

    private void Awake()
    {
        i_FirstPlay = PlayerPrefs.GetInt(FirstPlay);
        if(i_FirstPlay == 0)
        {
            fl_Background = .125f;
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

    }
    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(BackgroundPref, slider_Background.value);
        PlayerPrefs.SetFloat(SoundEffectPref, slider_SoundEffects.value);

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
        for(int i =0; i < effectAudio.Length; i ++)
        { 
            effectAudio[i].volume = slider_SoundEffects.value;
        }
    }
}
