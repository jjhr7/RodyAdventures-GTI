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

    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        rend = cuerpo.gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.green);

        empiezaLaPelea = false;

        //Creamos un objeto PositionData para guardar la pos inicial del murcíélago y que no varíe
        posicionInicial = new PositionData(transform.position, transform.rotation);


        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        caseSwitch = 1;
        currentBalas = maxbalas;
        colliderBrazo = brazoFuerte.GetComponent<Collider>();
        colliderBrazo.enabled = false;
        dmColliderBrazo= brazoFuerte.GetComponent<DamageColliderNyapos>();
        dmColliderCuerpo = cuerpoGO.GetComponent<DamageColliderNyapos>();
        dmColliderBrazo.damage = damage;
        dmColliderBrazo.player = target;
        dmColliderBrazo.haspegado = false;
        dmColliderCuerpo.damage = damage;
        dmColliderCuerpo.player = target;
        dmColliderCuerpo.haspegado = false;
        posicionActual = new PositionData(transform.position, transform.rotation);

        segundaFase = false;
    }


    void Update()
    {
        Debug.Log(posicionInicial);
        if (empiezaLaPelea)
        {
            //Recibir Danyo
            if (stats.recibiendoDanyo)
            {
                rend.material.SetColor("_Color", Color.magenta);
            }
            else
            {
                if (segundaFase)
                {
                    rend.material.SetColor("_Color", Color.red);
                }
                else
                {
                    rend.material.SetColor("_Color", Color.green);
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

                    colliderBrazo.enabled = true;
                    colliderCuerpo.enabled = true;
                    dist = Vector3.Distance(target.position, this.transform.position);
                    if (dist > 5)
                    {
                        nav.SetDestination(target.position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        dmColliderBrazo.haspegado = false;
                        dmColliderCuerpo.haspegado = false;
                        caseSwitch = Random.Range(2, 4);
                    }
                    break;
                case 1:
                    Debug.Log("case 1");
                    if (segundaFase)
                    {
                        caseSwitch = 0;
                    }
                    dist = Vector3.Distance(target.position, this.transform.position);
                    if (dist > 15)
                    {
                        nav.SetDestination(target.position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        if (!girado)
                        {

                            if (Mathf.Round(cuerpoYbrazos.localEulerAngles.y) != 340f)
                            {
                                cuerpoYbrazos.Rotate(direccion * MoveSpeed);
                            }
                            else
                            {
                                girado = true;
                            }
                        }
                        else
                        {
                            nav.SetDestination(transform.position);
                            colliderBrazo.enabled = false;
                            timer += Time.deltaTime;
                            if (timer > cadencia)
                            {
                                Instantiate(bulletMini, bulletSpawn.position, bulletSpawn.rotation);
                                timer = 0.0;
                                FindObjectOfType<AudioManager>().Play("shootTorreta");
                            }
                            if (Mathf.Round(cuerpoYbrazos.localEulerAngles.y) != 60f)
                            {
                                cuerpoYbrazos.Rotate(direccion * MoveSpeed/60);
                            }
                            else
                            {
                                caseSwitch = 0;
                                cuerpoYbrazos.localEulerAngles = Vector3.zero;
                            }
                        }

                    }

                    break;
                case 2:
                    Debug.Log("case 2");

                    colliderCuerpo.enabled = false;
                    nav.SetDestination(transform.position);
                    timer += Time.deltaTime;
                    if (timer > 1)
                    {
                        if (Mathf.Round(cuerpoYbrazos.localEulerAngles.y) != 30f)
                        {
                            if (segundaFase)
                            {
                                cuerpoYbrazos.Rotate(direccion * MoveSpeed / 4);
                            }
                            else
                            {
                                cuerpoYbrazos.Rotate(direccion * MoveSpeed / 2);
                            }
                        }
                        else
                        {
                            cuerpoYbrazos.localEulerAngles = Vector3.zero;
                            if (Random.Range(0, 2) > 0)
                            {
                                caseSwitch = -1;
                            }
                            else
                            {
                                caseSwitch = 0;
                            }
                            timer = 0.0;
                        }
                    }
                    break;
                case 3:
                    Debug.Log("case 3");
                    colliderBrazo.enabled = false;
                    dist = Vector3.Distance(target.position, this.transform.position);
                    if (dist > 3)
                    {
                        nav.SetDestination(target.position);
                        nav.speed = MoveSpeed;
                    }
                    else
                    {
                        caseSwitch = 4;
                    }
                    break;
                case 4:
                    Debug.Log("case 4");

                    timer += Time.deltaTime;
                    if (timer > 1.4)
                    {
                        if (!girado)
                        {
                            posicionActual.Position.z -= 0f;
                            girado = true;
                        }
                        nav.SetDestination(posicionActual.Position);
                        nav.speed = MoveSpeed *200;
                        if (Vector3.Distance(transform.position, posicionActual.Position) < 1f)
                        {

                            if (Random.Range(0, 2) > 0)
                            {
                                caseSwitch = -1;
                            }
                            else
                            {
                                caseSwitch = 0;
                            }
                            timer = 0;
                            girado = false;
                            colliderCuerpo.enabled = false;
                        }
                        if (timer > 2)
                        {
                            if (Random.Range(0, 2) > 0)
                            {
                                caseSwitch = -1;
                            }
                            else
                            {
                                caseSwitch = 0;
                            }
                            timer = 0;
                            girado = false;
                            colliderCuerpo.enabled = false;
                        }
                    }
                    else
                    {
                        girado = false;
                        nav.SetDestination(transform.position);
                        posicionActual.Position = target.localPosition;
                    }
                    break;

                case 5:
                    Debug.Log("case 5");

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
                            if (timer > (cadencia * 3) + 0.2)
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

                    cuerpoYbrazos.localEulerAngles = Vector3.zero;
                    colliderBrazo.enabled = false;
                    distIni = Vector3.Distance(posicionInicial.Position, this.transform.position);
                    if (distIni > 1)
                    {
                        nav.SetDestination(posicionInicial.Position);
                        nav.speed = MoveSpeed * 2;
                    }
                    else
                    {

                        timer += Time.deltaTime;
                        if (timer < 1.5)
                        {
                            transform.rotation = posicionInicial.Rotation;
                        }
                        else
                        {
                            transform.LookAt(target);
                            if (Random.Range(0, 2) > 0)
                            {
                                if (Random.Range(0, 2) > 0)
                                {
                                    caseSwitch = 1;
                                }
                                else
                                {
                                    caseSwitch = 5;
                                }
                            }
                            else
                            {
                                caseSwitch = 0;
                            }
                            timer = 0;
                        }
                    }
                    break;
            }


        }




    }

    private void OnTriggerEnter(Collider other)
    {
        empiezaLaPelea = true;
        Col.radius = 1;
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
