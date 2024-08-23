using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    private float currentGeneralVolume; // To store the current volume level

    void Start()
    {
        // Get the current volume of the General group when the game starts
        myAudioMixer.GetFloat("GeneralVolume", out currentGeneralVolume);
    }

    public void SetVolumeMultiplier(float sliderValue)
    {
        // Ensure the slider value is clamped between 0.0001 and 1
        sliderValue = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        
        // Calculate the new volume level
        float newVolume = Mathf.Pow(10, currentGeneralVolume / 20) * sliderValue;
        
        // Convert back to dB and set the volume
        myAudioMixer.SetFloat("GeneralVolume", Mathf.Log10(newVolume) * 20);
    }
}
