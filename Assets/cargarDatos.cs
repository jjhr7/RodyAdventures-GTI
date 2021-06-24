using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cargarDatos : MonoBehaviour
{
    public Text tiempo;
    private void Awake()
    {
        string tiempoTime = PlayerPrefs.GetString("time", "77:77");
        tiempo.text = "Tiempo: " + tiempoTime;
    }
}
