using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    //from Value 0.0001-1
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level){
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSoundFxVolume(float level){
        audioMixer.SetFloat("fxVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level){
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
    
}