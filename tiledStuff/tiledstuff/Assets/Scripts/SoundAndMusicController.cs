using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicController : MonoBehaviour
{
    /// <summary>
    /// source for the music
    /// </summary>
    private AudioSource musicSource;

    /// <summary>
    /// toggles music ingame on/off
    /// </summary>
    public void ToggleMusic()
    {
        musicSource = this.gameObject.GetComponent<AudioSource>();
        musicSource.enabled = !musicSource.enabled;

    }

}
