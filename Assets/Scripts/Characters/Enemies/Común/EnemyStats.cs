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

