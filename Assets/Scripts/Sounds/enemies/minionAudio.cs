using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionAudio : MonoBehaviour
{


    //Sonidos
    public AudioSource audioSource;
    public AudioClip[] audios;
    public void PlayAtaque()
    {
        audioSource.clip = audios[0];
        audioSource.Play();
    }


    public void PauseSound()
    {
        audioSource.Pause();

    }
}
