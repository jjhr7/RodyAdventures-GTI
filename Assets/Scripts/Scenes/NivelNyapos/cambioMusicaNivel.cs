using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioMusicaNivel : MonoBehaviour
{
    public AudioSource musicaSonando;
    public AudioClip[] listaMusicas = new AudioClip[2];
    public bool taCambiao = false;
    public void cambiarPista()
    {
        if (!taCambiao)
        {
            musicaSonando.clip = listaMusicas[1];
            taCambiao = true;
            musicaSonando.Play();
        }
        
    }
}
