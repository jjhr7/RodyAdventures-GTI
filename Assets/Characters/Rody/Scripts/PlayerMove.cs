using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   Animator animator;
   Vector2 input;
    
    public float runSpeed = 7;
    public float rotationSpeed = 250;
   
    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 30;
    public float jumpspeed = 12.5f;
   
    


    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    


    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");


        animator.SetFloat("InputX", x);
        animator.SetFloat("InputY", y);

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        if (Input.GetKey("b"))
        {
            animator.Play("Bola");
        }
        if (Input.GetKey("z"))
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe1");
        }
        if (Input.GetKey("x"))
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe2");
        }
        if (Input.GetKey("c"))
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe3");
        }
        if (Input.GetKey("m"))//tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Other", false);
            animator.Play("Muerte");
        }
        if (Input.GetKey("i"))//tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Other", true);
            animator.Play("Danyo");
        }
        if (Input.GetKey("mouse 0"))//tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Disparar", false);
            animator.Play("Dpistola");
        }
        if (Input.GetKey("mouse 1"))//tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Disparar", false);
            animator.Play("Dfusil");
        }
        if (Input.GetKey("f"))//tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Disparar", true);
            animator.Play("fusil");
        }

        if (x > 0 || x < 0 || y > 0 || y < 0)
        {
            animator.SetBool("Other", true);
            animator.SetBool("Disparar", true);
        }

        //Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            input.y -= jumpspeed;
            animator.Play("Jump");
        }


    }
}
