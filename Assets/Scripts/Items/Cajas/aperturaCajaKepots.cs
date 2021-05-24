using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaKepots : MonoBehaviour
{
    EnemyStats stats;
    public GameObject box;
    public Transform coinV;
    public Transform coinF;
    public Transform coinD;
    public Transform transformMe;

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
            Vector3 tras = transformMe.position;
            int caso = Random.Range(0, 4);
            switch (caso)
            {
                case 0:
                    Instantiate(coinV, tras, this.transformMe.rotation);
                    break;
                case 1:
                    Instantiate(coinF, tras, this.transformMe.rotation);
                    break;
                case 2:
                    Instantiate(coinD, tras, this.transformMe.rotation);
                    break;
                case 3:
                    Instantiate(coinV, tras, this.transformMe.rotation);
                    break;

            }

            //playerStats.TakeMoney(valor);
            Destroy(box);
        }
    }
}
