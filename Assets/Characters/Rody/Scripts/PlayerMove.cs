using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   Animator animator;
   Vector2 input;
    
    public float runSpeed = 7;
    public float rotationSpeed = 20;
   
    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 1;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool puede_saltar = true;


    public float jumpForce = 3;
  


    void Start()
    {
        animator = GetComponent<Animator>();
       
       
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");


        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        transform.Rotate(0, input.x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, input.y * Time.deltaTime * runSpeed );

        if (Input.GetKey("b"))
        {
            animator.Play("Bola");
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
        if(input.y == 0)
        {
            puede_saltar = true;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) ;
        if(Input.GetKey("space")&& isGrounded && puede_saltar==true)
        {
            animator.Play("Jump");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            puede_saltar = false;

        }

    }
    
    
}
