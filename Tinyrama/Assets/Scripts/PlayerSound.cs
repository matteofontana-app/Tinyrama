using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Jumping Sound")]
    public AudioClip[] jumpSounds;

    [Header("Walking Sound")]
    public AudioClip[] walkSounds;


    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void PlayJumpAudio()
    {
        AudioClip clip = jumpSounds[(int)Random.Range(0, jumpSounds.Length)];
        source.clip = clip;
        source.Play();
        
    }
}
