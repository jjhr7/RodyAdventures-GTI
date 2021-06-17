using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStats : CharacterManager
{

    [Range(0, 500)]
    [SerializeField]
    private int salud = 20;
    public bool recibiendoDanyo;
    double timer = 0.0;
    double timerMuerte = 0.0;

    public float pausa;
    PlayerStats PlayerStats;
    GameObject[] player;
    private GameObject myplayer;
    InputHandler inputHandler;
    CameraHolder cameraHolder;
    //loot -> si es 0 entonces el enmigo no va a soltar items de ese tipo 
    public int numCoinsLoot = 15;
    public int numMunicionEscopetaLoot = 1;
    public int numMunicionPistolaLoot = 1;
    public int numMunicionEspecialLoot = 1;
    public int numMoto = 1;
    //monedas 
    public Transform coin;
    //municion escopeta 
    public Transform municionEscopeta;
    //municion pistola 
    public Transform municionPistola;
    //municion especial 
    public Transform municionEspecial;
    // recompensa al derrotar al nyapos 
    public Transform moto;

    public AudioSource audioSource;


    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public int Salud
    {
        get { return salud; }
        set
        {
            salud = value;
            if (salud <= 0) //cuando muera
            {
                timerMuerte += Time.deltaTime;
                if (audioSource != null)
                {

                    audioSource.Play();
                }
                inputHandler.lockOnFlag = false; //quito el modo enfoque
                cameraHolder.ClearLockOnTarget(); //limpio la lista de enemigos cerca que puede enfocar
                Destroy(gameObject); //destruir objeto
                if (!isItem)
                {
                    enemyLoot();
                }
            }
        }
    }

    private void enemyLoot()
    {
        if (coin != null)
        {
            if (numCoinsLoot > 0) //si el enmigo puede tener coin como loot 
            {
                for (int i = 0; i < numCoinsLoot; i++)
                {
                    //coins 
                    Vector2 r = Random.insideUnitCircle * 1;
                    Vector3 tras = transform.position + new Vector3(r.x, 1, r.y);
                    Instantiate(coin, tras, this.transform.rotation);
                }
            }
        }
        if (municionEscopeta != null)
        {
            if (numMunicionEscopetaLoot > 0)
            {
                for (int i = 0; i < numMunicionEscopetaLoot; i++)
                {

                    Vector2 r = Random.insideUnitCircle * 1;
                    Vector3 tras = transform.position + new Vector3(r.x, 1, r.y);
                    Instantiate(municionEscopeta, tras, this.transform.rotation);
                }
            }
        }
        if (municionEspecial != null)
        {
            if (numMunicionEspecialLoot > 0)
            {
                for (int i = 0; i < numMunicionEspecialLoot; i++)
                {

                    Vector2 r = Random.insideUnitCircle * 1;
                    Vector3 tras = transform.position + new Vector3(r.x, 1, r.y);
                    Instantiate(municionEspecial, tras, this.transform.rotation);
                }
            }
        }
        if (municionPistola != null)
        {
            if (numMunicionPistolaLoot > 0)
            {
                for (int i = 0; i < numMunicionPistolaLoot; i++)
                {

                    Vector2 r = Random.insideUnitCircle * 1;
                    Vector3 tras = transform.position + new Vector3(r.x, 1, r.y);
                    Instantiate(municionPistola, tras, this.transform.rotation);
                }
            }
        }
        //moto 
        if (moto != null)
        {
            if (numMoto > 0)
            {
                for (int i = 0; i < numMoto; i++)
                {

                    Vector2 r = Random.insideUnitCircle * 1;
                    Vector3 tras = transform.position + new Vector3(r.x, 0, r.y);
                    Instantiate(moto, tras, this.transform.rotation);
                }
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
                //animator.Play("Damage_01");
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
        inputHandler = FindObjectOfType<InputHandler>();
        cameraHolder = FindObjectOfType<CameraHolder>();
        //Debug.Log("Ha entrado en TakeDamege");

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


    public void TakeDamage(int damage)
    {
        //Debug.Log("Ha entrado en TakeDamege");
        if (!recibiendoDanyo)
        {
            Salud -= damage;
            recibiendoDanyo = true;

        }
    }
}

