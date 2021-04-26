using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaKepots : MonoBehaviour
{
    EnemyStats stats;
    public int valor = 10;
    
    PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;

    
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (stats.recibiendoDanyo)
        {
            playerStats.TakeMoney(valor);
            Destroy(gameObject);
        }
    }
}
