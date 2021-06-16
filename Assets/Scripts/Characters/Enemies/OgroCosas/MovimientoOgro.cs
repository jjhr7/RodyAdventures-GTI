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


    //Sonidos
    public AudioSource audioSource;
    public AudioClip[] audios;
    void Start()
    {
        correEnemigo = false;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        targeteado = false;
        stats = this.gameObject.GetComponent<EnemyStats>();
        timer = 0;
        colliderEspada.enabled = false;

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
                    
                }
                else
                {
                    nav.SetDestination(transform.position);
                    anim.SetBool("pegando", true);
                    timer += Time.deltaTime;
                    if (timer > velocidadAtaque)
                    {
                        anim.SetBool("pegando", false);
                        timer = 0;
                    }
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
            anim.SetBool("pegando", false);
        }




    }


    public void EnableDamageCollider() //activar damage collider
    {
        colliderEspada.enabled = true;
    }
    public void DisableDamageCollider() //desactivar damage collider
    {
        colliderEspada.enabled = false;
    }


    public void playMuerte()
    {

    }
    public void playDanyo()
    {

        //Sonido recibir danyo
        audioSource.clip = audios[1];
        audioSource.Play();
    }

    public void playAtaque()
    {

        //sonido atacar

        audioSource.clip = audios[0];
        audioSource.Play();
    }

    public void PauseSound() 
    {
        audioSource.Stop();

    }
}
