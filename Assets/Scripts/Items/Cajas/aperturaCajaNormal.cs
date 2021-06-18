using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aperturaCajaNormal : MonoBehaviour
{
    EnemyStats stats;
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
        tamuerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.recibiendoDanyo)
        {
            if (!tamuerto)
            {
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
