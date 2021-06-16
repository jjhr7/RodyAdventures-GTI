using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidosNyapos : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audios;


    public void PlayAudioPatadas()
    {

        audioSource.clip = audios[0];
        audioSource.Play();
    }
    public void PlayAudioRisa()
    {

        audioSource.clip = audios[1];
        audioSource.Play();
    }

    public void PlayAudioGolpe()
    {

        audioSource.clip = audios[3];
        audioSource.Play();
    }

    public void PlayAudioCambio()
    {

        audioSource.clip = audios[2];
        audioSource.Play();
    }

    public void StopAudio()
    {

        audioSource.Stop();
    }
}
