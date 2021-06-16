using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodySoundsManager : MonoBehaviour
{
    public AudioSource AudioPlayerRody;
    public AudioClip[] listaSonidosRoyd = new AudioClip[7];

    public void prepararSonido(int numeroSonido)
    {
        if (numeroSonido > listaSonidosRoyd.Length)
        {
            Debug.Log("Sonido inexistente");
            return;
        }
            
        AudioPlayerRody.clip = listaSonidosRoyd[numeroSonido];
        AudioPlayerRody.Play();
    }

    public void VaciarAudioPlayer()
    {
        AudioPlayerRody.clip = null;
    }
    
    
}
