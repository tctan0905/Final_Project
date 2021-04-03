using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectPref = "SoundEffectPref";
    private float fl_Background, fl_soundEffects;
    public AudioSource backgroundAudio;
    public AudioSource[] effectAudio;

    private void Awake()
    {
        ContinueSetting();
    }

    private void ContinueSetting()
    {
        fl_Background = PlayerPrefs.GetFloat(BackgroundPref);
        fl_soundEffects = PlayerPrefs.GetFloat(SoundEffectPref);

        backgroundAudio.volume = fl_Background;
        
        for (int i = 0; i < effectAudio.Length; i++)
        {
            effectAudio[i].volume = fl_soundEffects;
        }
    }


}
