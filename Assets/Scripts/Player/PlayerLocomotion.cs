using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    /*
    -------------------------------
    PlayerLocomotion controla todas las funcionalidades de nuestro personaje que hace que se mueva
    -------------------------------
    */

    CameraHolder cameraHolder;
    PlayerManager playerManager;
    PlayerStats playerStats;
    Transform cameraObject;
    InputHandler inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;


    [Header("Ground & Air Detection Stats")]
    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f; //posicion del raycast inicial (justo debajo del collider del jugador)
    [SerializeField]
    float minimumDistanceNeededToBeginFall = 1f; //distancia en la que el jugador va a empezar a caer.
    [SerializeField]
    float groundDirectionRayDistance = 0.2f; // offset del raycast 
    LayerMask ignoreForGroundCheck;
    public float inAirTime;


    [Header("Movement Stats")]
    [SerializeField] //make private variables visible
    public float movementSpeed = 5;
    [SerializeField] //make private variables visible
    public float sprintSpeed = 7;
    [SerializeField] //make private variables visible
    public float rotationSpeed = 10;
    [SerializeField] //make private variables visible
    public float fallingSpeed = 350;

    //para el salto estatico
    public bool isJumping;
    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;


    private void Awake()
    {
        //dejar esto en el awake si se cambia de lugar lo de abajo
        cameraHolder = FindObjectOfType<CameraHolder>();

        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }
    void Start()
    {

        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();

        playerManager.isGrounded = true; //empieza el jugador en el suelo
        ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        if (isJumping == true)
        {
            //rigidbody.AddRelativeForce(0, 0.8f, 0.8f, ForceMode.Impulse);
        }
    }
    private void Update()
    {

        if (isJumping == true)
        {
            /*if (rigidbody.velocity.magnitude<2)
            {
                //rigidbody.AddRelativeForce(new Vector3(0, 1, 0) * 1.75f, ForceMode.Impulse);
                Debug.Log("DENGUE");
            }
            else
            {
                rigidbody.AddRelativeForce(new Vector3(0, 2, 1) * 2.5f, ForceMode.Impulse);
            }*/
        }
    }


    // #region es un organizador de codigo en bloques
    #region Movement 
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        if (isJumping) //si esta saltando no rotar
            return;

        if (inputHandler.lockOnFlag) //si esta en modo enfoque
        {
            if (inputHandler.sprintflag || inputHandler.rollflag) //si esta en modo bola o roll
            {
                Vector3 targetDirection = Vector3.zero;
                targetDirection = cameraHolder.cameraTransform.forward * inputHandler.vertical;
                targetDirection += cameraHolder.cameraTransform.right * inputHandler.horizontal;
                targetDirection.Normalize();
                targetDirection.y = 0;

                if (targetDirection == Vector3.zero)
                {
                    targetDirection = transform.forward;
                }

                Quaternion tr = Quaternion.LookRotation(targetDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                transform.rotation = targetRotation;
            }
            else
            {
                Vector3 rotationDirection = moveDirection;
                rotationDirection = cameraHolder.currentLockOnTarget.transform.position - transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                transform.rotation = targetRotation;
            }
        }
        else
        {

            Vector3 targetDir = Vector3.zero; //variable vector(0,0,0)
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir); //quaternion -> rotation
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;

        }

    }

    public void HandleMovement(float delta)
    {
        if (isJumping)//salto estatico
            return;

        if (inputHandler.rollflag) //si hace roll salirse de esta funcion
            return;

        if (playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;
        //si pulsa sprint y tiene stamina suficiente
        if (inputHandler.sprintflag && inputHandler.moveAmount > 0.5 && playerStats.currentStamina > 0) //si hace sprint
        {
            speed = sprintSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
        }
        else
        {
            if (inputHandler.moveAmount < 0.5)
            {
                moveDirection *= movementSpeed;
                playerManager.isSprinting = false;
            }
            else
            {
                moveDirection *= speed;
                playerManager.isSprinting = false;
            }

        }


        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        //animaciones modo enfoque
        if (inputHandler.lockOnFlag && inputHandler.sprintflag == false)
        { //SI ESTA EN MODO ENFOQUE y no esta en modo bola

            animatorHandler.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);

        }
        else
        { //sino

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

        }



        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    public void HandleRollingAndSprinting(float delta)
    {
        if (animatorHandler.anim.GetBool("isInteracting"))
            return;

        if (playerStats.currentStamina <= 0) //sino tiene stamina salirse de la funcion
            return;

        if (inputHandler.rollflag)
        { //si la variable en la clase inputHandler es true
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if (inputHandler.moveAmount > 0)
            { //si el personahe se mueve
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else //sino
            {
                animatorHandler.PlayTargetAnimation("Backstep", true);

            }
        }
    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }
        if (playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up * fallingSpeed); //fuerza en la velocidad
            rigidbody.AddForce(moveDirection * fallingSpeed / 5f); //fuerza en la direccion de caida
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;
        //Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
        {
            normalVector = hit.normal; //si raycast golpea algo estamos en el suelo
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if (playerManager.isInAir) //si esta en el aire
            {
                if (inAirTime > 0.5f)
                {

                    animatorHandler.PlayTargetAnimation("Land", true);
                    inAirTime = 0;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Locomotion", false); //vuelve a las animacion/estado originales
                    inAirTime = 0;
                }

                playerManager.isInAir = false;
            }
        }
        else
        {
            if (playerManager.isGrounded) //esta en el suelo
            {
                playerManager.isGrounded = false;
            }
            if (playerManager.isInAir == false && !isJumping) //si no esta en el aire
            {
                if (playerManager.isInteracting == false) // si no tiene ninguna interaccion
                {
                    animatorHandler.PlayTargetAnimation("Falling", true); //reproducir animacion llamada Falling
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
        }

        if (playerManager.isGrounded) //esta en el suelo
        {
            if (playerManager.isInteracting || inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, delta / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }
        }

    }
    //para el salto estatico
    public void HandleJumping()
    {
        if (playerStats.currentStamina <= 0) //sino tiene stamina salirse de la funcion
            return;
        if (playerManager.isGrounded)
        {
            Salto(rigidbody.velocity.magnitude);


            animatorHandler.anim.SetBool("isJumping", true);

            animatorHandler.PlayTargetAnimation("Jump", false);


        }
    }

    private void Salto(float velocity)
    {
        if (velocity < 2)
        {
            transform.Translate(Vector3.up * 2);
            rigidbody.AddRelativeForce(new Vector3(0, 1, 0) * 80f, ForceMode.Impulse);
           
        }
        else
        {
            rigidbody.AddRelativeForce(new Vector3(0, 4, 3) * 25f, ForceMode.Impulse);
        }
    }

    #endregion
    //endregion -> fin #region movement
}


