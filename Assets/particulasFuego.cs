using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasFuego : MonoBehaviour
{


    PlayerStats PlayerStats;
    GameObject[] player;
    private GameObject myplayer;
    PlayerStats ps;
    public GameObject particle;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        PlayerStats = myplayer.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.FLAGFuego)
        {
            particle.SetActive(true);
        }
        else
        {
            particle.SetActive(false);
        }
    }
}
