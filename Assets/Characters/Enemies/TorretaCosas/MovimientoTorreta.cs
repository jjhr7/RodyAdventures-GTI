using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTorreta : MonoBehaviour
{


    public Transform target;
    public Transform turret;
    public Transform cuerpo;
    public Transform bullet;
    public Transform bulletSpawn;
    double timer = 0.0;
    public int tiempoRecarga = 0;
    public double cadencia = 0.25;
    public int cargador = 0;
    int balas = 0;
    RecibirDaño salud;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        balas = cargador;
        salud = turret.gameObject.GetComponent<RecibirDaño>();
        
        rend = cuerpo.gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        if (salud.recibiendoDanyo)
        {
            rend.material.SetColor("_Color", Color.red);
            FindObjectOfType<AudioManager>().Play("deathTorreta");

        }
        else
        {
            rend.material.SetColor("_Color", Color.white);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {

            if (!salud.recibiendoDanyo)
            {
                turret.transform.LookAt(target.position + new Vector3(0,1,0));

                if (balas == 0)
                {
                    StartCoroutine(espera(tiempoRecarga));
                }
                else
                {
                    timer += Time.deltaTime;
                    if (other.transform == target)
                    {
                        if (timer > cadencia)
                        {
                            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                            balas--;
                            timer = 0.0;
                            FindObjectOfType<AudioManager>().Play("shootTorreta");                        
                        }
                    }
                }
            }

        }
        
    }

    IEnumerator espera(int t)
    {
        yield return new WaitForSeconds(t);
        balas = cargador;
    }
}