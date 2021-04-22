using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{

    public class RecibirDanyo : MonoBehaviour
    {


        [Range(0, 20)]
        [SerializeField]
        private float salud = 20f;
        public bool recibiendoDanyo;
        double timer = 0.0;
        public float pausa;
        PlayerStats PlayerStats;
        GameObject[] player;
        private GameObject myplayer;
        public float Salud
        {
            get { return salud; }
            set
            {
                salud = Mathf.Clamp01(value);
                if (salud == 0)
                {
                    Destroy(gameObject);
                }
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            Collider bala = collision.gameObject.GetComponent<Collider>();
            Destruirbala Dbala = collision.gameObject.GetComponent<Destruirbala>();
            if (bala.tag.Equals("Bala"))
            {
                if (!recibiendoDanyo)
                {
                    Salud -= Dbala.danyo;
                    recibiendoDanyo = true;
                }
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            recibiendoDanyo = false;

            player = GameObject.FindGameObjectsWithTag("Player");
            myplayer = player[0];
            PlayerStats = myplayer.GetComponent<PlayerStats>();

        }

        // Update is called once per frame
        void Update()
        {


            if (recibiendoDanyo)
            {
                //temporizador recibir danyo
                timer += Time.deltaTime;

                if (timer > pausa)
                {
                    recibiendoDanyo = false;
                    timer = 0;
                }
            }


        }

    }

}