using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarMurcielago : MonoBehaviour
{
    //Atributos de la clase

    //El jugador
    public Transform target;
    //El propio murciélago
    public Transform bat;
    //Balas del murciélago
    public Transform bullet;
    //Spawn de balas del murciélago
    public Transform bulletSpawn;

    //Cadencia de disparo y contador para llegar a ésta
    double timer = 0.0;
    public double cadencia = 2;

    //Distancia donde el muriélago empieza a disparar
    public float MinDist;

    //Indica si el jugador ha sido visto o no por el murciélago
    bool targeteado;

    //Velocidad del murciélago
    public int MoveSpeed;
    //Posicion del murciélago
    PositionData posicionInicial;

    //Script de salud + renderer + esfera del murciélago para cambiarle de color al recibir daño
    RecibirDaño salud;
    Renderer rend;
    public Transform cuerpo;


    void Start()
    {

        //Inicializamos valores y scripts
        salud = bat.gameObject.GetComponent<RecibirDaño>();
        rend = cuerpo.gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.white);
        targeteado = false;

        //Creamos un objeto PositionData para guardar la pos inicial del murcíélago y que no varíe
        posicionInicial = new PositionData(bat.transform.position, bat.transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {

        //Ponemos roja la esfera si recibe daño y activamos el audio
        if (salud.recibiendoDanyo)
        {
            rend.material.SetColor("_Color", Color.red);

            FindObjectOfType<AudioManager>().Play("deathEscupe");
        }
        else
        {
            rend.material.SetColor("_Color", Color.white);
        }




        //Estados del murciélago
        //Si el murciélago ve al jugador
        if (targeteado)
        {
            //Este if sirve para que el murciélago pare unos instantes al recibir daño
            if (!salud.recibiendoDanyo)
            {
                //Calculamos la distancia con el jugador
                float dist = Vector3.Distance(target.position, bat.position);
                //El timer empieza a contar para que al llegar le dispare 
                timer += Time.deltaTime;
                //El muriélago apunta al jugador
                bat.transform.LookAt(target.position + new Vector3(0, 1, 0));
                if (dist >= MinDist)
                {
                    //Lo movemos a la velocidad deseada
                    bat.position += bat.forward * MoveSpeed * Time.deltaTime;

                }
                else
                {
                    //Si llega a la distancia instancia balas para herir al jugador
                    if (timer > cadencia)
                    {
                        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                        timer = 0.0;
                        FindObjectOfType<AudioManager>().Play("shootEscupe");
                    }
                }
            }
        }
        //Si el murciélago NO ve al jugador
        else
        {
            //Trata de volver a su posición inicial
            if (Vector3.Distance(bat.position, posicionInicial.Position) > 0.2)
            {
                bat.transform.LookAt(posicionInicial.Position);
                bat.position += bat.forward * MoveSpeed * Time.deltaTime;
            }
            //Cuando se encuentra muy cerca de ésta le detenemos para evitar bugs
            else
            {
                bat.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bat.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                bat.transform.rotation = posicionInicial.Rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Targeteado es true si el jugador entra en el área del murciélago
        if (other.transform == target)
        {
            targeteado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Targeteado es false si el jugador sale del el área del murciélago
        if (other.transform == target)
        {
            targeteado = false;
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
