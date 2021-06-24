using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motilloAudioManager : MonoBehaviour
{
    public AudioSource sonidoMoto;
    private float motoAcelerada;

    public void AcelerarMoto()
    {
        sonidoMoto.pitch = 1.20f;
    }
    
    public void DejarAcelerar()
    {
        sonidoMoto.pitch = 1f;
    }
}
