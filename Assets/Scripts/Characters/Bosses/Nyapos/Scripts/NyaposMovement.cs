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

    //Cadencia de disparo y contador para llegar a ésta
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

    Rigidbody rb;


    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        empiezaLaPelea = false;
        //Creamos un objeto PositionData para guardar la pos inicial del Nyapos y que no varíe
        posicionInicial = new PositionData(transform.position, transform.rotation);

        rb = GetComponent<Rigidbody>();
        timerBalas = 0;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        caseSwitch = 2;
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

        segundaFase = false;
    }

    void FixedUpdate()
    {
        if (empiezaLaPelea)
        {
            //Recibir Danyo
            if (stats.recibiendoDanyo)
            {
                //rend.material.SetColor("_Color", Color.magenta);
            }
            else
            {
                if (segundaFase)
                {
                    //rend.material.SetColor("_Color", Color.red);
                }
                else
                {
                    //rend.material.SetColor("_Color", Color.green);
                }
            }


            if (stats.Salud < 100)
            {
                if (!segundaFase)
                {
                    nav.speed = MoveSpeed * 2;
                    caseSwitch = 6;
                }
            }
            //Cambio entre los diferentes ataques de Ñapos
            switch (caseSwitch)
            {
                case 0:
                    Debug.Log("case 0");
                    colliderBrazo.enabled = false;
                    colliderCuerpo.enabled = false;
                    dist = Vector3.Distance(posicionInicial.Position, transform.position);
                    Debug.Log(dist);
                    if (dist > 5)
                    {
                        nav.SetDestination(posicionInicial.Position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        timer = 0;
                        anim.SetBool("Corriendo", false);
                        dmColliderBrazo.haspegado = false;
                        caseSwitch = Random.Range(2,4);
                    }
                    break;
                case 1:
                    Debug.Log("case 1");
                    if (segundaFase)
                    {
                        caseSwitch = 0;
                    }
                    dist = Vector3.Distance(target.position, this.transform.position);
                    if (dist > 60)
                    {
                        nav.SetDestination(target.position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        
                            nav.SetDestination(transform.position);
                            colliderBrazo.enabled = false;
                            timerBalas += Time.deltaTime;
                            timer += Time.deltaTime;
                            if (timerBalas > cadencia)
                            {
                                Instantiate(bulletMini, bulletSpawn.position, bulletSpawn.rotation);
                                timerBalas = 0.0;
                                FindObjectOfType<AudioManager>().Play("shootTorreta");
                            }
                            if (timer<3.5)
                            {
                            transform.Rotate(direccion * (MoveSpeed / 5) * Time.deltaTime);
                            }
                            else
                            {
                                Debug.Log("CAMBIO");
                                rb.rotation=Quaternion.identity;
                                timer = 0;
                                caseSwitch = 1;
                                cuerpoYbrazos.localEulerAngles = Vector3.zero;
                            }
                    }

                    break;
                case 2:
                    Debug.Log("case 2");
                    dmColliderBrazo.haspegado = false;
                    colliderBrazo.enabled = true;
                    dist = Vector3.Distance(target.position, this.transform.position);
                    if (dist > 3)
                    {
                        anim.SetBool("Corriendo", true);
                        nav.SetDestination(target.transform.position);

                    }else{
                        timer += Time.deltaTime;
                        anim.SetBool("Corriendo", false);
                        anim.SetBool("Pegando", true);
                        nav.SetDestination(transform.position);
                        if (timer > 1.5)
                        {
                            colliderBrazo.enabled = false;
                            if (Random.Range(0, 2) > 0)
                            {
                                anim.SetBool("Pegando", false);
                                caseSwitch = 3;
                            }
                            else
                            {
                                anim.SetBool("Pegando", false);
                                caseSwitch = 0;
                            }
                            timer = 0.0;
                        }

                    }
                    
                    break;
                case 3:
                    Debug.Log("case 3");
                    colliderBrazo.enabled = false;
                    colliderCuerpo.enabled = true;
                    dmColliderCuerpo.haspegado = false;
                    rb.isKinematic = false;
                    rb.rotation = Quaternion.identity;
                    caseSwitch = 4;
                    timer = 0;
                    nav.speed =MoveSpeed* 5;
                    break;
                case 4:
                    Debug.Log(timer);
                    anim.SetBool("Embistiendo", true);
                    timer += Time.deltaTime;
                    if (timer < 3)
                    {
                        nav.SetDestination(target.position);
                    }
                    else
                    {
                        nav.speed = MoveSpeed / 5;
                        anim.SetBool("Embistiendo", false);
                        colliderBrazo.enabled = false;
                        timer = 0;
                        if (Random.Range(0, 2) > 0)
                        {
                            caseSwitch = -1;
                        }
                        else
                        {
                            caseSwitch = 0;
                        }
                    }
                    break;

                case 5:
                    Debug.Log("case 5");
                    nav.SetDestination(transform.position);


                    //La torreta recarga si se le acaban las balas
                    if (currentBalas == 0)
                    {
                        currentBalas = maxbalas;
                        if (Random.Range(0, 2) > 0)
                        {
                            caseSwitch = 1;
                        }
                        else
                        {
                            caseSwitch = 0;
                        }
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        //Este if permite disparar a la cadencia deseada
                        if (timer > cadencia * 3)
                        {
                            if (timer > (cadencia * 3) + 0.1)
                            {
                                //Restamos una bala al cargador, disparamos y reproducimos el sonido
                                Instantiate(bulletBig, bulletSpawn.position, bulletSpawn.rotation);
                                currentBalas--;
                                timer = 0.0;
                                FindObjectOfType<AudioManager>().Play("shootTorreta");
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
                    Debug.Log("case 6");

                    cuerpoYbrazos.localEulerAngles = Vector3.zero;
                    colliderBrazo.enabled = false;
                    distIni = Vector3.Distance(posicionInicial.Position, this.transform.position);
                    if (distIni > 1)
                    {
                        timer = 0;
                        nav.SetDestination(posicionInicial.Position);
                    }
                    else
                    {

                        timer += Time.deltaTime;
                        if (timer < 1.5)
                        {
                            rend.material.SetColor("_Color", Color.red);
                            transform.LookAt(target.position);
                        }
                        else
                        {
                            MoveSpeed *= 2;
                            transform.LookAt(target);
                            caseSwitch = 0;
                            timer = 0;
                            segundaFase = true;
                            damage += 5;
                        }
                    }
                    break;
                default:
                    Debug.Log("case -1");
                    colliderBrazo.enabled = false;
                    colliderCuerpo.enabled = false;
                    nav.speed = MoveSpeed;
                    dist = Vector3.Distance(posicionInicial.Position, transform.position);
                    if (dist > 5)
                    {
                        nav.SetDestination(posicionInicial.Position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        timer = 0;
                        anim.SetBool("Corriendo", false);
                        dmColliderBrazo.haspegado = false;
                        if (Random.Range(0, 2) > 0)
                        {
                            caseSwitch = 1;
                        }
                        else
                        {
                            caseSwitch = 5;
                        }
                    }
                    break;
            }


        }
        



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            empiezaLaPelea = true;
            Col.radius = 1;
        }
    }



    //Simple clase personalizada usada para serializar la posicion y rotacion iniciales del murciélago
    [System.Serializable]
    public class PositionData
    {
        //Posición a serializar
        public Vector3 Position;

        //Rotación a serializar
        public Quaternion Rotation;

        //Dejamos un constructor vacío
        public PositionData() { }

        //Creamos un constructor con parámetros
        public PositionData(Vector3 pos, Quaternion rot)
        {
            Position = pos;
            Rotation = rot;
        }
    }
}
