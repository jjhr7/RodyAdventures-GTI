using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modoBolaSound : MonoBehaviour
{
    public AudioSource sonidoPisada;
    public void OnTriggerEnter(Collider other)
    {
        sonidoPisada.pitch = Random.Range(0.5f,1.5f);
        sonidoPisada.Play();
    }
}
