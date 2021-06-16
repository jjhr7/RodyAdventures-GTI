using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pisadaRody : MonoBehaviour
{
    public AudioSource sonidoPisada;
    public void OnTriggerEnter(Collider other)
    {
        sonidoPisada.Play();
    }
}
