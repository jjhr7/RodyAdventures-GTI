using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ApuntarMurcielago : MonoBehaviour
    {
        //Atributos de la clase

        //El jugador
        public Transform target;
        //El propio murci�lago
        public Transform bat;
        //Balas del murci�lago
        public Transform bullet;
        //Spawn de balas del murci�lago
        public Transform bulletSpawn;

        //Cadencia de disparo y contador para llegar a �sta
        double timer = 0.0;
        public double cadencia = 2;

        //Distancia donde el muri�lago empieza a disparar
        public float MinDist;

        //Indica si el jugador ha sido visto o no por el murci�lago
        bool targeteado;

        //Velocidad del murci�lago
        public int MoveSpeed;
        //Posicion del murci�lago
        PositionData posicionInicial;

        //Script de salud + renderer + esfera del murci�lago para cambiarle de color al recibir danyo
        EnemyStats stats;
        public Transform cuerpo;


        public AudioSource audioSource;
        public AudioClip[] audios;

        void Start()
        {

            //Inicializamos valores y scripts
            stats = bat.gameObject.GetComponent<EnemyStats>();
            targeteado = false;

            //Creamos un objeto PositionData para guardar la pos inicial del murc��lago y que no var�e
            posicionInicial = new PositionData(bat.transform.position, bat.transform.rotation);
        }


        // Update is called once per frame
        void Update()
        {

            //Ponemos roja la esfera si recibe danyo y activamos el audio
            if (stats.recibiendoDanyo)
            {
                audioSource.clip = audios[0];
                audioSource.Play();
            }
            else
            {

            }




            //Estados del murci�lago
            //Si el murci�lago ve al jugador
            if (targeteado)
            {
                //Este if sirve para que el murci�lago pare unos instantes al recibir danyo
                if (!stats.recibiendoDanyo)
                {
                    //Calculamos la distancia con el jugador
                    float dist = Vector3.Distance(target.position, bat.position);
                    //El timer empieza a contar para que al llegar le dispare 
                    timer += Time.deltaTime;
                    //El muri�lago apunta al jugador
                    bat.transform.LookAt(target.position + new Vector3(0, 1, 0));
                    if (dist >= MinDist)
                    {
                        //Lo movemos a la velocidad deseada
                        bat.position += bat.forward * MoveSpeed * Time.deltaTime;

                    }
                    else
                    {
                        //Si llega a la distancia instancia balas para herir al jugador
                        if (timer > cadencia)
                        {
                            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                            timer = 0.0;

                            audioSource.clip = audios[1];
                            audioSource.Play();
                    }
                    }
                }
            }
            //Si el murci�lago NO ve al jugador
            else
            {
                //Trata de volver a su posici�n inicial
                if (Vector3.Distance(bat.position, posicionInicial.Position) > 0.2)
                {
                    bat.transform.LookAt(posicionInicial.Position);
                    bat.position += bat.forward * MoveSpeed * Time.deltaTime;
                }
                //Cuando se encuentra muy cerca de �sta le detenemos para evitar bugs
                else
                {
                    bat.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bat.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    bat.transform.rotation = posicionInicial.Rotation;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Targeteado es true si el jugador entra en el �rea del murci�lago
            if (other.transform == target)
            {
                targeteado = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Targeteado es false si el jugador sale del el �rea del murci�lago
            if (other.transform == target)
            {
                targeteado = false;
            }
        }



        //Simple clase personalizada usada para serializar la posicion y rotacion iniciales del murci�lago
        [System.Serializable]
        public class PositionData
        {
            //Posici�n a serializar
            public Vector3 Position;

            //Rotaci�n a serializar
            public Quaternion Rotation;

            //Dejamos un constructor vac�o
            public PositionData() { }

            //Creamos un constructor con par�metros
            public PositionData(Vector3 pos, Quaternion rot)
            {
                Position = pos;
                Rotation = rot;
            }
        }
    }
