using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialHandler : MonoBehaviour
{
    // PAREDES INVISIBLES
    public GameObject cubo1;
    public GameObject cubo2;
    public GameObject cubo3;
    public GameObject cuboTienda;
    public GameObject cuboArma;
    public GameObject cuboMover;
    public GameObject cuboVerde;



    // ITEMS
    public GameObject cajamonedas;
    public GameObject cajasmonedas;
    public GameObject kepot;
    public GameObject kepotV;
    public GameObject arma;
    public GameObject shopWindow;
    public GameObject bola;



    double timer = 0;//Ultimo timer
    double timerV = 0;//Segundo timer
    double Timert = 0;//Primer timer



    // ATRIBUTOS
    PlayerStats PlayerStats;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;
    private PositionData posicionActual;
    GameObject[] player;
    private GameObject myplayer;
    public GameObject subtitles;
    public Text subitlesTX;
    private float ms;
    private float Rs;


    //Audios

    public AudioSource audioSource;
    public AudioClip[] audios;
    int fase;
    int faseAnterior;
    bool hapasao;

    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        PlayerStats = myplayer.GetComponent<PlayerStats>();
        playerManager = myplayer.GetComponent<PlayerManager>();
        playerLocomotion = myplayer.GetComponent<PlayerLocomotion>();
        ms = playerLocomotion.movementSpeed;
        playerLocomotion.movementSpeed = 0;

        Rs = playerLocomotion.sprintSpeed;
        playerLocomotion.sprintSpeed = 0;
        posicionActual = new PositionData(myplayer.transform.position, myplayer.transform.rotation);
        cajasmonedas.SetActive(false);
        subtitles.SetActive(true);
        Timert = 0;
        fase = -1;
        faseAnterior = -1;
        hapasao = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timert += Time.deltaTime;

        if (Timert > 2)
        {
            fase = 0;

        }

        if (Timert > 34)
        {
            audios[0] = null;
            fase = 1;
        }
        if (Timert > 56)
        {
            fase = 2;
            playerLocomotion.movementSpeed = ms;
            playerLocomotion.sprintSpeed = Rs;

            subitlesTX.text = " Utiliza el Joystick o WASD para MOVERTE";
        }

            if (Vector3.Distance(posicionActual.Position, myplayer.transform.position) > 2)
            {
                hapasao = true;
                audios[2] = null;
                fase = 3;
                subitlesTX.text = "Pulsa E o el botón B en gamepad para ATACAR, también puedes DISPARAR con click derecho o R1, prueba a romper esas cajas";
                Destroy(cuboMover);
                cajasmonedas.SetActive(true);

            }
        if (hapasao)
        {
            audios[2] = null;
            fase = 3;
            subitlesTX.text = "Pulsa E o el botón B en gamepad para ATACAR, también puedes DISPARAR con click derecho o R1, prueba a romper esas cajas";
            Destroy(cuboMover);
            cajasmonedas.SetActive(true);

        }
        if (cajamonedas == null)
        {
            fase = 4;

            Destroy(cuboVerde);
            subitlesTX.text = "A tu izquierda tienes un KEPOT verde, al comerlo te subirá vida";

        }
        if (kepotV == null)
        {
            fase = 5;

            subitlesTX.text = "Como tenías la salud completa, te proporcionará un ESCUDO que decaerá con el tiempo, aunque quizá te haga oler peor...";
            timerV += Time.deltaTime;
        }

        if (timerV > 9)
        {
            fase = 6;

            Destroy(cubo1);
            subitlesTX.text = "A tu derecha tienes un KEPOT de fuego, acércate a él para comertelo y ganar fuerza";
        }
        if (kepot == null)
        {
            fase = 7;

            timer += Time.deltaTime;
            subitlesTX.text = "También hay otros kepots MORADOS que no te conviene comer...";

        }
        if (timer > 5)
        {
            fase = 8;

            audios[7] = null;
            Destroy(cuboArma);
            subitlesTX.text = "Si cojes un ARMA podrás cambiar de arma a melé con z y las armas de fuego con x, en gamepad puedes usar la cruzeta";
        }
        if (arma == null)
        {
            fase = 9;
            Destroy(cuboTienda);
            subitlesTX.text = "Prueba a interactuar con el bazar para comprar cosas con las monedas que has obtenido";
        }

        if (playerManager.entroEnLaTienda)
        {
            fase = 10;
            subitlesTX.text = "Utiliza Shift o LB para activar el MODO BOLA, así podrás rodar para ir mas rápido";
            Destroy(cubo2);
            if (bola.activeSelf)
            {
                subtitles.SetActive(false);
                Destroy(gameObject);
            }
        }
        cambiarDeFase(fase);


    }

    public void cambiarDeFase(int fase)
    {
        if (fase != faseAnterior)
        {
            audioSource.clip = audios[fase];
            audioSource.Play();
            faseAnterior = fase;
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

