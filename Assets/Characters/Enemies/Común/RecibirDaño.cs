using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibirDaño : MonoBehaviour
{


    [Range(0, 1)]
    [SerializeField]
    private float salud = 1f;
    public bool recibiendoDanyo;
    double timer =0.0;
    public float pausa;

    public float Salud
    {
        get { return salud; }
        set
        {
            salud = Mathf.Clamp01(value);
            if (salud == 0)
            {
                Destroy(gameObject);
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
                Salud -= Dbala.danyo;
                recibiendoDanyo = true;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        recibiendoDanyo = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (recibiendoDanyo)
        {
            //temporizador recibir daño
            timer += Time.deltaTime;

            if (timer > pausa)
            {
                recibiendoDanyo = false;
                timer = 0;
            }
        }


    }

}
