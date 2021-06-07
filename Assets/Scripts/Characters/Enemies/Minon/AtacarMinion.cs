using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class AtacarMinion : MonoBehaviour
    {
        public SphereCollider Col;

        public Transform player;

        private NavMeshAgent nav;

        public int velocidadAtaque = 2;

        //Indica si el jugador ha sido visto por el enemigo
        private bool targeteado;

        public float vel = 1;

        private float dist;

        EnemyStats stats;

        public Transform me;

        double timer = 0.0;
        public SphereCollider area;

        PlayerStats ps;

        public int danyo;

        public Transform cuerpo;

        public Animator anim;



        void Start()
        {

            nav = GetComponent<NavMeshAgent>();
            stats = me.GetComponent<EnemyStats>();
            nav.SetDestination(nav.transform.position);
            targeteado = false;

            ps = player.GetComponent<PlayerStats>();
        }

        void Update()
        {
            if (!stats.recibiendoDanyo)
            {
                //Si el enemigo ve al jugador
                if (targeteado)
                {
                    if (anim != null)
                    {
                        anim.SetBool("pegando", false);
                        anim.SetBool("andando", true);
                    }
                    dist = Vector3.Distance(player.position, this.transform.position);
                    if (dist > area.radius)
                    {

                        nav.SetDestination(player.position);
                        nav.speed = vel;
                    }
                    else
                    {
                        if (anim != null)
                        {
                            anim.SetBool("andando", false);
                            anim.SetBool("pegando", true);

                        }
                        nav.SetDestination(transform.position);
                        timer += Time.deltaTime;
                        if (timer > velocidadAtaque)
                        {
                            if (anim != null)
                            {
                                anim.SetBool("pegando", false);
                                anim.SetBool("andando", true);

                            }
                            dist = Vector3.Distance(player.position, transform.position);
                            if (dist <= area.radius)
                            {
                                ps.TakeDamage(danyo);
                               
                            }
                            timer = 0;
                        }
                    }
                }
                else
                {
                    nav.SetDestination(transform.position);
                }

            }
            else
            {


            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                Col.radius = 1;
                targeteado = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                targeteado = true;

            }
        }
    }
}
