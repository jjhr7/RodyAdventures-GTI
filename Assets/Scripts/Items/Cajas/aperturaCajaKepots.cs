using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaKepots : MonoBehaviour
{
    EnemyStats stats;
    public int valor = 10;
    public Transform coin;
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

            for (int i=0; i < 19; i++)
            {

                Vector2 r = Random.insideUnitCircle * 3;
                Vector3 tras = transform.position + new Vector3(r.x, Random.Range(0, 3), r.y);
                Instantiate(coin, tras, this.transform.rotation);

            }
            //playerStats.TakeMoney(valor);
            Destroy(gameObject);
        }
    }
}
