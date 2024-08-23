using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteEnvironment : MonoBehaviour
{
    public AudioMixer audioMixer;  // Reference to the Audio Mixer
    private bool isMuted = false;  // Track if the audio is currently muted
    private const string environmentVolumeParameter = "EnvironmentVolume";  // Name of the exposed parameter for the Environment volume

    // This function is called when the object is clicked
    private void OnMouseDown()
    {
        ToggleMute();
    }

    // Function to toggle mute on the Environment group
    void ToggleMute()
    {
        isMuted = !isMuted;

        // Set the volume of the environment group to -80 dB (mute) or 0 dB (unmute)
        audioMixer.SetFloat(environmentVolumeParameter, isMuted ? -80f : 0f);
    }
}
