using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsSettings : MonoBehaviour
{
    
    /*public AudioMixerSnapshot snap1;
    public AudioMixerSnapshot snap2;
    
    snap2.Transiotion(segundos);*/
    public AudioMixer queMixer;

    public void SetMasterEfectoVolumen(float sfxLevel)
    { 
        queMixer.SetFloat("MusicaVol",Mathf.Log10(sfxLevel)*20);
    }
    
    public void SetEffectsVolume(float sfxLevel)
    {
        queMixer.SetFloat("SFXVol",Mathf.Log10(sfxLevel)*20);
    }
}
