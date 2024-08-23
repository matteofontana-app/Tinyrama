using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    public GameObject GameUI;
    public AudioMixer audioMixer;
    public Animator uiAnimator;
    public MixerFXController mixerFXController; // Reference to MixerFXController

    private float originalMasterVolume;
    private float originalEnvironmentVolume;
    private bool isPaused = false;

    void Start()
    {
        audioMixer.GetFloat("MasterVolume", out originalMasterVolume);
        audioMixer.GetFloat("EnvironmentVolume", out originalEnvironmentVolume);

        // Initialize the FX volume controller
        if (mixerFXController != null)
        {
            mixerFXController.InitializeFXVolume();
        }
    }

    public void Pause()
    {
        if (isPaused) return;
        GameUI.SetActive(false);
        Time.timeScale = 0;

        cinemachineBrain.enabled = false;

        // Reduce volumes for Master and Environment
        audioMixer.SetFloat("MasterVolume", originalMasterVolume - 10f);
        audioMixer.SetFloat("EnvironmentVolume", Mathf.Log10(0.0001f) * 20);

        // Mute FX volume during pause
        if (mixerFXController != null)
        {
            mixerFXController.MuteFXVolume();
        }

        if (uiAnimator != null)
        {
            uiAnimator.speed = 1;
        }

        isPaused = true;
    }

    public void Continue()
    {
        if (!isPaused) return;
        GameUI.SetActive(true);
        Time.timeScale = 1;

        cinemachineBrain.enabled = true;

        audioMixer.SetFloat("MasterVolume", originalMasterVolume);
        audioMixer.SetFloat("EnvironmentVolume", originalEnvironmentVolume);

        // Restore original FX volume
        if (mixerFXController != null)
        {
            mixerFXController.RestoreFXVolume();
        }

        if (uiAnimator != null)
        {
            uiAnimator.speed = 1;
        }

        isPaused = false;
    }

    public void ResumeForQuit()
    {
        Time.timeScale = 1;
    }
}
