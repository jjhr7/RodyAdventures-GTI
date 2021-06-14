using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer queMixer;

    public void setMasterEffectVolumen(float sfxLevel)
    {
        queMixer.SetFloat("MasterVol", Mathf.Log10(sfxLevel)*20);
    }
}
