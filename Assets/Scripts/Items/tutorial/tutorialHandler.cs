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

    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player");
        myplayer = player[0];
        PlayerStats = myplayer.GetComponent<PlayerStats>();
        playerManager = myplayer.GetComponent<PlayerManager>();
        playerLocomotion = myplayer.GetComponent<PlayerLocomotion>();
        ms = playerLocomotion.movementSpeed;
        playerLocomotion.movementSpeed = 0;
        posicionActual = new PositionData(myplayer.transform.position, myplayer.transform.rotation);
        cajasmonedas.SetActive(false);
        subtitles.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Timert += Time.deltaTime;
        if (Timert > 5)
        {
            playerLocomotion.movementSpeed = ms;
            subitlesTX.text = " Utiliza el Joystick o WASD para MOVERTE";
        }


        if (Vector3.Distance(posicionActual.Position, myplayer.transform.position) > 2)
        {
            subitlesTX.text = "Pulsa E o el botón B en gamepad para ATACAR, también puedes DISPARAR con click derecho o R1, prueba a romper esas cajas";
            Destroy(cuboMover);
            cajasmonedas.SetActive(true);

        }
        if (cajamonedas == null)
        {
            Destroy(cuboVerde);
            subitlesTX.text = "A tu izquierda tienes un KEPOT verde, al comerlo te subirá vida";

        }
        if (kepotV == null)
        {
            subitlesTX.text = "Como tenías la salud completa, te proporcionará un ESCUDO que decaerá con el tiempo, aunque quizá te haga oler peor...";
            timerV += Time.deltaTime;
        }

        if (timerV > 5)
        {
            Destroy(cubo1);
            subitlesTX.text = "A tu derecha tienes un KEPOT de fuego, acércate a él para comertelo y ganar fuerza";
        }
        if (kepot == null)
        {
            timer += Time.deltaTime;
            subitlesTX.text = "También hay otros kepots MORADOS que no te conviene comer...";

        }
        if (timer > 5)
        {
            Destroy(cuboArma);
            subitlesTX.text = "Si cojes un ARMA podrás cambiar de arma a melé con z y las armas de fuego con x, en gamepad puedes usar la cruzeta";
        }
        if (arma == null)
        {
            Destroy(cuboTienda);
            subitlesTX.text = "Prueba a interactuar con el bazar para comprar cosas con las monedas que has obtenido";
        }

        if (playerManager.entroEnLaTienda)
        {
            subitlesTX.text = "Utiliza Shift o LB para activar el MODO BOLA, así podrás rodar para ir mas rápido";
            Destroy(cubo2);
            if (bola.activeSelf)
            {
                subtitles.SetActive(false);
                Destroy(gameObject);
            }
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

