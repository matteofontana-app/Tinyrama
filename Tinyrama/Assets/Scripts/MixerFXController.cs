using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerFXController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    private float originalFXVolume; // Store the original FX volume

    void Start()
    {
        // Initialize the original FX volume when the script starts
        InitializeFXVolume();
    }

    public void InitializeFXVolume()
    {
        // Get and store the current FX volume
        myAudioMixer.GetFloat("FXVolume", out originalFXVolume);
    }

    public void SetFXVolumeMultiplier(float sliderValue)
    {
        // Ensure the slider value is clamped between 0.0001 and 1
        sliderValue = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        
        // Calculate the new volume level
        float currentFXVolumeInLinear = Mathf.Pow(10, originalFXVolume / 20);
        float newVolume = currentFXVolumeInLinear * sliderValue;
        
        // Convert back to dB and set the volume
        myAudioMixer.SetFloat("FXVolume", Mathf.Log10(newVolume) * 20);
    }

    // Call this method to set the FX volume to mute (0 dB) during pause
    public void MuteFXVolume()
    {
        myAudioMixer.SetFloat("FXVolume", Mathf.Log10(0.0001f) * 20);
    }

    // Call this method to restore the original FX volume when resuming
    public void RestoreFXVolume()
    {
        myAudioMixer.SetFloat("FXVolume", originalFXVolume);
    }
}
