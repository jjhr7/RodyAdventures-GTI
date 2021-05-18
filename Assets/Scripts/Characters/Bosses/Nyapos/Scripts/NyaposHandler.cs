using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyaposHandler : MonoBehaviour
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

    private bool girado;
    private int currentBalas;
    public int maxbalas;

    bool segundaFase;
    private bool empiezaLaPelea;
    public Animator anim;

    Rigidbody rb;

    private void Awake()
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
        dmColliderBrazo = brazoFuerte.GetComponent<DamageColliderNyapos>();

        dmColliderBrazo.damage = damage;
        dmColliderBrazo.player = target;
        dmColliderBrazo.haspegado = false;
        posicionActual = new PositionData(transform.position, transform.rotation);

        segundaFase = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            empiezaLaPelea = true;
            Col.radius = 1;
        }
    }


    private void AtaqueDisparo1()
    {
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
            if (timer < 3.5)
            {
                rb.AddRelativeTorque(Vector3.up * MoveSpeed * 2);
            }
            else
            {
                Debug.Log("CAMBIO");
                rb.rotation = Quaternion.identity;
                timer = 0;
                caseSwitch = 0;
                cuerpoYbrazos.localEulerAngles = Vector3.zero;
            }
        }
    }

    #region PositionData
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
    #endregion
}
