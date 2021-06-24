using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class CoinHandler : MonoBehaviour
    {


        public int valor = 1;
        private bool targeteado;

        PlayerStats playerStats;
        GameObject[] player;
        private GameObject myplayer;
        //public AudioSource monedaSound;
        public int MoveSpeed;

        public GameObject monedas;

        void Start()
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            myplayer = player[0];
            playerStats = myplayer.GetComponent<PlayerStats>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(myplayer.transform.position, transform.position)<5)
            {
                targeteado = true;
            }

            if (targeteado)
            {
                //Calculamos la distancia con el jugador
                float dist = Vector3.Distance(myplayer.transform.position, transform.position);
                //Apunta al jugador
                this.transform.LookAt(myplayer.transform.position + new Vector3(0, 1, 0));
                //Lo movemos a la velocidad deseada
                this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Destruímos si toca al jugador
            if (other.tag.Equals("Player"))
            {
                //monedaSound.Play();
                playerStats.TakeMoney(valor);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Destruímos si toca al jugador
            if (collision.collider.tag.Equals("Player"))
            {
                //monedaSound.Play();
                playerStats.TakeMoney(valor);
                Destroy(gameObject);
            }
        }


    }

