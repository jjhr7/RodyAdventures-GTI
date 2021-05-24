using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKepotHandler : MonoBehaviour
{


    public int valor = 1;
    private bool targeteado;

    PlayerStats playerStats;
    GameObject[] player;
    private GameObject myplayer;

    public int MoveSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        playerStats = myplayer.GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (targeteado)
        {

            //Calculamos la distancia con el jugador
            float dist = Vector3.Distance(myplayer.transform.position, transform.position);
            //Apunta al jugador
            this.transform.LookAt(myplayer.transform.position + new Vector3(0, 1, 0));
            //Lo movemos a la velocidad deseada
            this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
        }
        transform.Rotate(new Vector3(0,1,0) * MoveSpeed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        //Targeteado es true si el jugador entra en el �rea
        if (other.tag.Equals("Player"))
        {
            targeteado = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destru�mos si toca al jugador
        if (collision.collider.tag.Equals("Player"))
        {
            playerStats.TakeFire(valor);
            Destroy(gameObject);
        }
    }

}
