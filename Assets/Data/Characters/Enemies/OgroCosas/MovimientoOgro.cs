using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class MovimientoOgro : MonoBehaviour
    {

        public Transform player;
        UnityEngine.AI.NavMeshAgent nav;
        private Animator anim;
        public int velocidadAtaque = 2;
        private bool targeteado;
        public float vel = 1;
        private float dist;
        bool correEnemigo;
        EnemyStats stats;
        public Transform me;
        double timer = 0.0;
        public SphereCollider area;
        public int damage;
        public Collider colliderEspada;

        void Start()
        {
            correEnemigo = false;
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
            anim = GetComponent<Animator>();
            targeteado = false;
            stats = this.gameObject.GetComponent<EnemyStats>();
            colliderEspada.enabled = false;
            timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetBool("golpeado", stats.recibiendoDanyo);
            if (!stats.recibiendoDanyo)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < 35)
                {
                    correEnemigo = true;
                    dist = Vector3.Distance(player.position, this.transform.position);
                    if (dist > area.radius)
                    {
                        correEnemigo = true;
                        nav.SetDestination(player.position);
                        nav.speed = vel;
                        anim.SetBool("corriendo", correEnemigo);
                        anim.SetBool("pegando", false);
                        colliderEspada.enabled = false;

                    }
                    else
                    {
                        nav.SetDestination(transform.position);
                        anim.SetBool("pegando", true);
                        colliderEspada.enabled = true;
                        timer += Time.deltaTime;
                        if (timer > velocidadAtaque)
                        {
                            anim.SetBool("pegando", false);

                            colliderEspada.enabled = false;
                            if (targeteado)
                            {
                                anim.SetBool("corriendo", true);
                                anim.SetBool("pegando", false);
                                colliderEspada.enabled = false;

                                Debug.Log("ooooo");
                            }
                            else
                            {
                                colliderEspada.enabled = false;
                                anim.SetBool("pegando", false);
                                anim.SetBool("corriendo", false);
                            }
                            timer = 0;
                        }
                    }
                }
                else
                {
                    bool correEnemigo = false;
                    anim.SetBool("corriendo", correEnemigo);
                    colliderEspada.enabled = false;
                }
            }
            else
            {
                nav.SetDestination(transform.position);
                anim.SetBool("golpeado", true);
                anim.SetBool("pegando", true);
                FindObjectOfType<AudioManager>().Play("deathOgro");
                colliderEspada.enabled = false;
            }




        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                targeteado = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                targeteado = false;
            }
        }
    }

