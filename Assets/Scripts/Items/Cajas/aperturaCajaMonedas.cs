using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaMonedas : MonoBehaviour
{
    EnemyStats stats;
    public int valor = 10;
    public Transform coin;
    PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;


    public AudioSource audioSource;
    public AudioClip[] audios;
    double timer = 0;
    bool tamuerto;
    public GameObject modelo3d;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();

        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();
        tamuerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.recibiendoDanyo)
        {
            if (!tamuerto)
            {

                for (int i = 0; i < 20; i++)
                {

                    Vector2 r = Random.insideUnitCircle * 3;
                    Vector3 tras = transform.position + new Vector3(r.x, Random.Range(0, 3), r.y);
                    Instantiate(coin, tras, this.transform.rotation);

                }
                modelo3d.SetActive(false);
                audioSource.Play();

            }
            tamuerto = true;
        }

        if (tamuerto)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                Destroy(gameObject);

            }
        }
    }
}

