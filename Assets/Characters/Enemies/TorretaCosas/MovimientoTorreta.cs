using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{

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
        RecibirDanyo salud;
        Renderer rend;
        public Transform cuerpo;

        // Start is called before the first frame update
        void Start()
        {
            //Inicializamos variables y Gameobjects
            balas = cargador;
            salud = turret.gameObject.GetComponent<RecibirDanyo>();
            rend = cuerpo.gameObject.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.white);
        }

        // Update is called once per frame
        void Update()
        {
            if (salud.recibiendoDanyo)
            {
                //Animación y sonido de recibir danyo
                rend.material.SetColor("_Color", Color.red);
                FindObjectOfType<AudioManager>().Play("deathTorreta");

            }
            else
            {
                rend.material.SetColor("_Color", Color.white);
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("Player"))
            {

                if (!salud.recibiendoDanyo)
                {
                    //Miramos al jugador si entra en el trigger
                    turret.transform.LookAt(target.position + new Vector3(0, 1, 0));

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
                                FindObjectOfType<AudioManager>().Play("shootTorreta");
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
}