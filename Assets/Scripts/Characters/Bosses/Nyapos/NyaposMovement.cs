using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyaposMovement : MonoBehaviour
{

    //Atributos de la clase



    public SphereCollider Col;
    //El jugador
    public Transform target;

    //Balas
    public Transform bulletMini;
    public Transform bulletBig;

    //Spawn de balas 
    public Transform bulletSpawn;
    public Transform escupeSpawn;


    //Cadencia de disparo y contador para llegar a �sta
    double timer = 0.0;
    double timerBalas = 0.0;

    public double cadencia = 2;

    //Script de salud + renderer + cuerpo para cambiarle de color al recibir danyo
    EnemyStats stats;
    Renderer rend;
    public Transform cuerpo;

    public Transform cuerpoYbrazos;
    UnityEngine.AI.NavMeshAgent nav;

    public Vector3 direccion;

    int caseSwitch;

    //Velocidad del Nyapos
    public int MoveSpeed;
    //Posicion inicial
    PositionData posicionInicial;
    //Posicion actual
    PositionData posicionActual;


    private float dist;
    private float distIni;

    private float timerSecondPhase;
    public int damage;
    public Collider colliderBrazo;

    public Collider colliderCuerpo;

    public GameObject brazoFuerte;
    public GameObject cuerpoGO;

    DamageColliderNyapos dmColliderBrazo;
    DamageColliderNyapos dmColliderCuerpo;


    private bool girado;
    private int currentBalas;
    public int maxbalas;

    bool segundaFase;
    private bool empiezaLaPelea;
    public Animator anim;

    bool transformacion;

    Rigidbody rb;

    public GameObject barraGo;
    public HealthBar health;


    public AudioSource audioSource;
    public AudioSource audioSourceDanyo;

    public AudioClip[] audios;

    public cambioMusicaNivel cambioMusicaNivel;
    public GameObject puertaBoss;
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        empiezaLaPelea = false;
        //Creamos un objeto PositionData para guardar la pos inicial del Nyapos y que no var�e
        posicionInicial = new PositionData(transform.position, transform.rotation);

        rb = GetComponent<Rigidbody>();
        timerBalas = 0;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = MoveSpeed;
        caseSwitch = 5;
        currentBalas = maxbalas;
        colliderBrazo = brazoFuerte.GetComponent<Collider>();
        colliderBrazo.enabled = false;
        dmColliderCuerpo=cuerpoGO.GetComponent<DamageColliderNyapos>();

        dmColliderBrazo = brazoFuerte.GetComponent<DamageColliderNyapos>();

        dmColliderBrazo.damage = damage;
        dmColliderBrazo.player = target;
        dmColliderBrazo.haspegado = false;

        dmColliderCuerpo.damage = damage;
        dmColliderCuerpo.player = target;
        dmColliderCuerpo.haspegado = false;
        posicionActual = new PositionData(transform.position, transform.rotation);
        transformacion = false;
        segundaFase = false;
        health.SetMaxHealth(stats.Salud);
    }

    void FixedUpdate()
    {
        if (empiezaLaPelea)
        {
            puertaBoss.SetActive(true);
            barraGo.SetActive(true);
            //Recibir Danyo
            if (stats.recibiendoDanyo)
            {
                health.SetCurrentHealth(stats.Salud);
                audioSourceDanyo.Play();
            }
            else
            {
                if (segundaFase)
                {
                    if (stats.Salud < 150)
                    {
                        timerSecondPhase += Time.deltaTime;
                        if (timerSecondPhase > 2)
                        {
                            timerSecondPhase = 0;
                            stats.Salud += 3;
                        }
                    }
                }
                else
                {
                    //rend.material.SetColor("_Color", Color.green);
                }
            }


            if (stats.Salud < 150)
            {
                if (!segundaFase)
                {
                    SegundaFase();
                }
            }
            if (!transformacion)
            {
                //Cambio entre los diferentes ataques de �apos
                switch (caseSwitch)
                {
                    case 0:
                        //Fase 0 - Volver a la posicion inicial
                        Debug.Log("case 0");
                        colliderBrazo.enabled = false;
                        colliderCuerpo.enabled = false;
                        dist = Vector3.Distance(posicionInicial.Position, transform.position);
                        Debug.Log(dist);
                        if (dist > 8)
                        {
                            anim.SetBool("Corriendo", true);
                            nav.SetDestination(posicionInicial.Position);
                        }
                        else
                        {
                            timer = 0;
                            anim.SetBool("Corriendo", false);
                            dmColliderBrazo.haspegado = false;
                            caseSwitch = CambiarDeFase(caseSwitch);
                        }
                        break;
                    case 1:
                        //Fase 1 - Rafaga de disparos

                        audioSource.clip = audios[1];
                        audioSource.Play();
                        Debug.Log("case 1");
                        nav.SetDestination(transform.position);
                        colliderBrazo.enabled = false;
                        timerBalas += Time.deltaTime;
                        timer += Time.deltaTime;
                        if (timerBalas > cadencia)
                        {
                            anim.SetBool("Disparando", true);
                            Instantiate(bulletMini, bulletSpawn.position, bulletSpawn.rotation);
                            timerBalas = 0.0;
                        }
                        if (timer < 3.5)
                        {
                            transform.Rotate(direccion * MoveSpeed * Time.deltaTime);
                        }
                        else
                        {
                            anim.SetBool("Disparando", false);
                            rb.rotation = Quaternion.identity;
                            timer = 0;
                            caseSwitch = CambiarDeFase(caseSwitch);
                            cuerpoYbrazos.localEulerAngles = Vector3.zero;
                        }
                        break;
                    case 2:
                        //Fase 2 - Acercarse para Pu�etazo basico

                        timer = 0;
                        Debug.Log("case 2");
                        dist = Vector3.Distance(target.position, this.transform.position);
                        if (dist > 3)
                        {
                            anim.SetBool("Corriendo", true);
                            nav.SetDestination(target.transform.position);

                        }
                        else
                        {
                            nav.SetDestination(transform.position);
                            caseSwitch = 7;
                        }

                        break;
                    case 3:

                        //Fase 3 - Preparar la embestida

                        Debug.Log("case 3");
                        colliderBrazo.enabled = false;
                        colliderCuerpo.enabled = true;
                        dmColliderCuerpo.haspegado = false;
                        rb.isKinematic = false;
                        rb.rotation = Quaternion.identity;
                        caseSwitch = 4;
                        timer = 0;
                        break;
                    case 4:

                        //Fase 4 - Embestida ninja

                        Debug.Log(timer);
                        anim.SetBool("Embistiendo", true);
                        timer += Time.deltaTime;
                        dist = Vector3.Distance(target.position, this.transform.position);
                        if (timer < 3)
                        {

                            if (dist > 3)
                            {
                                nav.SetDestination(target.position);

                            }
                            else
                            {
                                nav.SetDestination(transform.position);
                            }
                        }
                        else
                        {
                            anim.SetBool("Embistiendo", false);
                            colliderBrazo.enabled = false;
                            timer = 0;
                            caseSwitch = CambiarDeFase(caseSwitch);
                        }
                        break;

                    case 5:

                        //Fase 5 - Escupitajos

                        Debug.Log("case 5");
                        nav.SetDestination(transform.position);


                        if (currentBalas == 0)
                        {
                            anim.SetBool("Escupiendo", false);

                            currentBalas = maxbalas;
                            caseSwitch = CambiarDeFase(caseSwitch);
                        }
                        else
                        {
                            timer += Time.deltaTime;
                            //Este if permite disparar a la cadencia deseada
                            if (timer > cadencia * 3)
                            {
                                anim.SetBool("Escupiendo", true);

                                if (timer > (cadencia * 2) + 0.1)
                                {

                                    audioSource.clip = audios[3];
                                    audioSource.Play();
                                    //Restamos una bala al cargador, disparamos y reproducimos el sonido
                                    Instantiate(bulletBig, escupeSpawn.position, escupeSpawn.rotation);
                                    currentBalas--;
                                    timer = 0.0;
                                }
                            }
                            else
                            {
                                //Miramos al jugador si entra en el trigger
                                transform.LookAt(target.position);
                            }
                        }
                        break;
                    case 6:

                        //Fase 6 - Llegar a la 2da fase

                        break;
                    case 7:
                        //Fase 7 - Pu�etazo basico

                        timer += Time.deltaTime;
                        anim.SetBool("Corriendo", false);
                        anim.SetBool("Pegando", true);
                        nav.SetDestination(transform.position);
                        if (timer > 0.4)
                        {
                            colliderBrazo.enabled = true;
                            dmColliderBrazo.haspegado = false;
                        }
                        if (timer > 1)
                        {

                            audioSource.clip = audios[1];
                            audioSource.Play();
                            anim.SetBool("Pegando", false);
                            colliderBrazo.enabled = false;
                            caseSwitch = CambiarDeFase(caseSwitch);
                            timer = 0.0;
                        }
                        break;
                    default:
                        caseSwitch = 0;
                        break;
                }
            }
            


        }
        



    }


    private int CambiarDeFase(int faseActual)
    {
        anim.SetBool("Corriendo", false);
        anim.SetBool("Escupiendo", false);
        anim.SetBool("Disparando", false);
        anim.SetBool("Pegando", false);
        anim.SetBool("Embistiendo", false);
        audioSource.Pause();
        if (faseActual == 2)
        {
            if (Random.Range(0, 5) > 0)
            {
                return 0;
            }
            else
            {
                PlayAudioById(2);
                return 3;
            }
        }
        if (caseSwitch == 6)
        {
            return 0; 
        }

        if (faseActual == 4)
        {
            if (Random.Range(0, 6) > 0)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }

        if (faseActual == 5)
        {
            if (Random.Range(0, 2) > 0)
            {
                return 0;
            }
            else
            {
                transform.rotation = Quaternion.identity;
                PlayAudioById(1);
                return 1;
            }
        }

        if (faseActual == 1)
        {
            if (Random.Range(0, 4) > 0)
            {
                return 2;
            }
            else
            {
                transform.rotation = Quaternion.identity;
                return 5;
            }
        }

        if (faseActual == 0)
        {
            int caso = Random.Range(0, 6);
            if (caso == 4)
            {
                return 3;
            }
            if (caso == 0)
            {
                transform.rotation = Quaternion.identity;
                return 1;
            }
            return caso;
        }
        else
        {
            return 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            cambioMusicaNivel.cambiarPista();
            
            empiezaLaPelea = true;
            Col.enabled = false;
        }
    }

    private void SegundaFase()
    {
        transformacion = true;


        timer += Time.deltaTime;
        anim.SetBool("Corriendo", true);
        anim.SetBool("Escupiendo", false);
        anim.SetBool("Disparando", false);
        anim.SetBool("Pegando", false);
        anim.SetBool("Embistiendo", false);
        Debug.Log("case 6");
        colliderBrazo.enabled = false;
        colliderCuerpo.enabled = false;
        distIni = Vector3.Distance(posicionInicial.Position, this.transform.position);
        if (distIni > 3)
        {
            nav.SetDestination(posicionInicial.Position);
        }
        else
        {
            anim.SetBool("Corriendo", false);
            if (timer < 2.5)
            {
                nav.SetDestination(transform.position);

                anim.SetBool("Gritando", true);
                transform.LookAt(target.position);
            }
            else
            {
                anim.SetBool("Gritando", false);
                MoveSpeed *= 2;
                transformacion = false;
                caseSwitch = 0;
                timer = 0;
                segundaFase = true;
                damage += 10;
            }
        }
    }
    public void PlayAudioById(int id)
    {

        audioSource.clip = audios[id];
        audioSource.Play();
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
