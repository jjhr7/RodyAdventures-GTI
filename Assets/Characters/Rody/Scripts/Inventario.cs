using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public int Cantidad = 0;
    public GameObject inventario;
    void Start()
    {

    }
    void Update()
    {
        if(Cantidad == 1)
        {
            inventario.SetActive(true);
        }
        else
        {
            inventario.SetActive(false);
        }
    }
}
