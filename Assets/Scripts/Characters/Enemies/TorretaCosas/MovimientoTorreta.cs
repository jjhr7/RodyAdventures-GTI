using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MovimientoTorreta : MonoBehaviour
    {

        //Atributos de la clase

        //El jugador
        public Transform target;
        //El propio torreta
        public Transform turret;
        //Balas
        public Transform bullet;
        //Spawn de balas 
        public Transform bulletSpawn;

        //Cadencia de disparo y contador para llegar a ésta
        double timer = 0.0;
        public double cadencia = 2;

        //Contador y cargador de balas + tiempo de espera
        public int tiempoRecarga = 0;
        public int cargador = 0;
        int balas = 0;

        //Indica si el jugador ha sido visto o no por la torreta
        bool targeteado;

        //Script de salud + renderer + cuerpo para cambiarle de color al recibir danyo
        EnemyStats stats;
        public Transform cuerpo;



    //Sonidos
    public AudioSource audioSource;
    public AudioClip[] audios;

    // Start is called before the first frame update
    void Start()
        {
            //Inicializamos variables y Gameobjects
            balas = cargador;
            stats = turret.gameObject.GetComponent<EnemyStats>();
        }

        // Update is called once per frame
        void Update()
        {
            if (stats.recibiendoDanyo)
            {
            //Animación y sonido de recibir danyo
            audioSource.volume = 1f;
            audioSource.clip = audios[1];
            audioSource.Play();

        }
            else
            {
            }

            if(Vector3.Distance(transform.position, target.position) < 45)
        {

        }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("Player"))
            {

                if (!stats.recibiendoDanyo)
                {
                    //Miramos al jugador si entra en el trigger
                    turret.transform.LookAt(target.position + new Vector3(0, 0.3f, 0));

                    //La torreta recarga si se le acaban las balas
                    if (balas == 0)
                    {
                        StartCoroutine(espera(tiempoRecarga));
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        if (other.transform == target)
                        {
                            //Este if permite disparar a la cadencia deseada
                            if (timer > cadencia)
                            {
                                //Restamos una bala al cargador, disparamos y reproducimos el sonido
                                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                                balas--;
                                timer = 0.0;
                                audioSource.volume = 0.3f;
                                audioSource.clip = audios[0];
                                audioSource.Play();
                        }
                        }
                    }
                }

            }

        }

        IEnumerator espera(int t)
        {
            //Esperamos los segundos deseados
            yield return new WaitForSeconds(t);
            //Recargamos las balas de la torreta
            balas = cargador;
        }
    }