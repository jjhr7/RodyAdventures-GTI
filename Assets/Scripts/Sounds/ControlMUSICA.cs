using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlMUSICA : MonoBehaviour
{

    //CONTROL GLOBAL
    private static ControlMUSICA instanciaM = null;


    public static ControlMUSICA InstanciaMusica
    {
        get
        {
            return instanciaM;
        }
    }

    void Awake()
    {
        if (instanciaM != null && instanciaM != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instanciaM = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }


}
