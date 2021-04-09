using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //variables
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public Animator animator;
    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 1;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;



    void Update()
    {
        //controles de movimiento del jugador
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed,0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        if (Input.GetKey("b"))//pulsar b
        {
            animator.Play("Bola");
        }
        if (Input.GetKey("z"))//pulsar z
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe1");
        }
        if (Input.GetKey("x"))//pulsar x
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe2");
        }
        if (Input.GetKey("c"))//pulsar c
        {
            animator.SetBool("Other", true);
            animator.Play("Golpe3");
        }
        if (Input.GetKey("m"))// pulsar m tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Other", false);
            animator.Play("Muerte");
        }
        if (Input.GetKey("i"))//pulsar i
        {
            animator.SetBool("Other", true);
            animator.Play("Danyo");
        }
        if (Input.GetKey("mouse 0"))// pulsar click izquierdo del ratón,tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Disparar", false);
            animator.Play("Dpistola");
        }
        if (Input.GetKey("mouse 1"))// pulsar click derecho del ratón,tienes que estar quieto para poder ver la animacion
        {
            animator.SetBool("Disparar", false);
            animator.Play("Dfusil");
        }
        if (Input.GetKey("f"))// pulsar f y moverse
        {
            animator.SetBool("Disparar", true);
            animator.Play("fusil");
        }

        if (x>0 || x<0 || y>0 || y < 0)//condicion para poder ver las animaciones cuando esten quitas
        {
            animator.SetBool("Other", true);
            animator.SetBool("Disparar", true);
        }
        //salto
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey("space")&& isGrounded)
        {
            animator.Play("Jump");
            Invoke("Jump", 1f);
            
        }
        

    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
