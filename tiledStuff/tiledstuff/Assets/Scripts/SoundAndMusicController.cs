using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicController : MonoBehaviour
{
    private AudioSource musicSource;
    public void ToggleMusic()
    {
        musicSource = this.gameObject.GetComponent<AudioSource>();

    }

    public void ToggleSounds()
    {

    }
}
