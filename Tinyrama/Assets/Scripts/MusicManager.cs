using UnityEngine;
using UnityEngine.UI; // Make sure to include this namespace for UI elements

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] songs;       // Array to store different songs
    public Sprite[] songImages;     // Array to store images for each song
    public Image songImageDisplay;  // Reference to the Image component that displays the song image

    private int currentSongIndex = 0; // Index of the currently playing song

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (songs.Length > 0)
        {
            PlaySong(currentSongIndex); // Start by playing the first song
        }
    }

    // Function to play the next song in the list
    public void PlayNextSong()
    {
        if (songs.Length == 0) return;

        currentSongIndex = (currentSongIndex + 1) % songs.Length; // Move to the next song, loop back to the start
        PlaySong(currentSongIndex);
    }

    // Function to play the previous song in the list
    public void PlayPreviousSong()
    {
        if (songs.Length == 0) return;

        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length; // Move to the previous song, loop to the end
        PlaySong(currentSongIndex);
    }

    // Function to play a song given its index
    private void PlaySong(int index)
    {
        if (audioSource == null || songs.Length == 0) return;

        audioSource.clip = songs[index];
        audioSource.Play();
        UpdateSongImage(index); // Update the image to match the current song
    }

    // Function to update the displayed image based on the song index
    private void UpdateSongImage(int index)
    {
        if (songImageDisplay != null && songImages.Length > index)
        {
            songImageDisplay.sprite = songImages[index];
        }
    }
}
