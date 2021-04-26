using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaMonedas : MonoBehaviour
{
    PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;
    public EnemyStats stats;


    private void Update()
    {
        if (stats.recibiendoDanyo)
        {
            Destroy(gameObject);
            Debug.Log("hola");
            playerStats.TakeMoney(6);
            
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();
        stats = gameObject.GetComponent<EnemyStats>();

    }

}

