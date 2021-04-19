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
    RecibirDaño salud;
    public Transform me;
    double timer = 0.0;
    public SphereCollider area;

    void Start()
    {
        correEnemigo = false;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        targeteado = false;
        salud = this.gameObject.GetComponent<RecibirDaño>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("golpeado", salud.recibiendoDanyo);
        if (!salud.recibiendoDanyo){
            if (targeteado)
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

                }
                else
                {
                    nav.SetDestination(transform.position);
                    anim.SetBool("pegando", true);
                    timer+=Time.deltaTime;
                    if (timer > velocidadAtaque)
                    {
                        dist = Vector3.Distance(player.position, transform.position);
                        if (dist <= area.radius)
                        {
                            Destroy(player.gameObject);
                        }
                        if (targeteado)
                        {
                            anim.SetBool("corriendo", true);
                        }
                        else
                        {
                            anim.SetBool("corriendo", false);
                        }
                        timer = 0;
                    }
                    Debug.Log(timer);
                }
            }
            else
            {
                bool correEnemigo = false;
                anim.SetBool("corriendo", correEnemigo);
            }
        }
        else
        {
            nav.SetDestination(transform.position);
            anim.SetBool("golpeado", true);
            FindObjectOfType<AudioManager>().Play("deathOgro");

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
