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


        PlayerManager playerManager;
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
        float movementSpeed = 5;
        [SerializeField] //make private variables visible
        float sprintSpeed = 7;
        [SerializeField] //make private variables visible
        float rotationSpeed= 10;
        [SerializeField] //make private variables visible
        float fallingSpeed = 45;


        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();

            playerManager.isGrounded = true; //empieza el jugador en el suelo
            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        }

        

        // #region es un organizador de codigo en bloques
        #region Movement 
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta) 
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
        
        public void HandleMovement(float delta)
        {

            if (inputHandler.rollflag) //si hace roll salirse de esta funcion
                return;

            if (playerManager.isInteracting)
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            if (inputHandler.sprintflag && inputHandler.moveAmount > 0.5) //si hace sprint
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                if(inputHandler.moveAmount < 0.5)
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

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
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

            if(Physics.Raycast(origin,myTransform.forward, out hit, 0.4f))
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
            if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal; //si raycast golpea algo estamos en el suelo
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if (playerManager.isInAir) //si esta en el aire
                {
                    if(inAirTime > 0.5f) 
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
                if (playerManager.isInAir == false) //si no esta en el aire
                {
                    if(playerManager.isInteracting == false) // si no tiene ninguna interaccion
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

        #endregion
        //endregion -> fin #region movement
    }


