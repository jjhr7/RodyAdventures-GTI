using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{

    public float velocidad = 4f;
    public CharacterController characterController;
    public Transform bullet;
    public Transform bulletSpawn;
    double timer =0.0;
    private double cadencia = 0.6;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movimiento = new Vector3();
        movimiento.x = Input.GetAxis("Horizontal");
        movimiento.z = Input.GetAxis("Vertical");
        movimiento *= velocidad;
        characterController.SimpleMove(movimiento);
        if (Input.GetKey("c"))
        {
            timer += Time.deltaTime;
                if (timer > cadencia)
                {
                    Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                    timer = 0;
                }

        }
    }

}