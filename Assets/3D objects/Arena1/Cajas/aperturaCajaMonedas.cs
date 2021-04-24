using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaMonedas : MonoBehaviour
{
    [Range(0, 2)]
    [SerializeField]
    private float salud = 2f;
    public bool recibiendoDanyo;

    GameObject[] player;
    private GameObject myplayer;

    public GameObject cajas;

    public float Salud
    {
        get { return salud; }
        set
        {
            salud = Mathf.Clamp01(value);
            if (salud == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        recibiendoDanyo = false;
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destru?mos si toca al jugador
        if (collision.collider.tag.Equals("Player"))
        {
         
            Destroy(gameObject);
        }
    }
}

