using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaravigilancia : MonoBehaviour
{
    PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;
    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(myplayer.transform.position);
    }
}
