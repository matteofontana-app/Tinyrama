using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to your AudioMixer
    public Slider masterVolumeSlider; // Reference to the Master Volume Slider
    public Slider environmentVolumeSlider; // Reference to the Environment Volume Slider

    private float originalMasterVolume; // To store the original Master volume level
    private float originalEnvironmentVolume; // To store the original Environment volume level

    private void Start()
    {
        // Get the current volume of the Master and Environment groups when the game starts
        audioMixer.GetFloat("MasterVolume", out originalMasterVolume);
        audioMixer.GetFloat("EnvironmentVolume", out originalEnvironmentVolume);

        // Initialize slider values based on current audio mixer settings
        masterVolumeSlider.value = Mathf.Pow(10, originalMasterVolume / 20);
        environmentVolumeSlider.value = Mathf.Pow(10, originalEnvironmentVolume / 20);

        // Add listeners for slider changes
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        environmentVolumeSlider.onValueChanged.AddListener(SetEnvironmentVolume);
    }

    // Update the Master volume in the AudioMixer
    private void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    // Update the Environment volume in the AudioMixer
    private void SetEnvironmentVolume(float value)
    {
        audioMixer.SetFloat("EnvironmentVolume", Mathf.Log10(value) * 20);
    }

    // Call this method to reset volume sliders to original values
    public void ResetVolumes()
    {
        audioMixer.SetFloat("MasterVolume", originalMasterVolume);
        audioMixer.SetFloat("EnvironmentVolume", originalEnvironmentVolume);

        masterVolumeSlider.value = Mathf.Pow(10, originalMasterVolume / 20);
        environmentVolumeSlider.value = Mathf.Pow(10, originalEnvironmentVolume / 20);
    }
}
