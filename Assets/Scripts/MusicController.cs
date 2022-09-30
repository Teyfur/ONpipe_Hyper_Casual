using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private bool music_enable;

    [SerializeField]
    private AudioSource music;

    private void Awake()
    {
        music_enable = true;
    }

    public void ToggleMusic()
    {
        if (!music_enable)
        {
            //enable music
            music.Play();
            music_enable = true;
        }
        else
        {
            //disable music
            music.Pause();
            music_enable = false;
        }
    }
}
