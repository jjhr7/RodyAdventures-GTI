using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerManager : MonoBehaviour
    {

        /*
        -------------------------------
        PlayerManager es un centro de control, en el que manejamos los metodos desde el update
        -> manejamos los flags de los movimientos, set las variables a false para que no haga la animacion 
        -> -> conectamos todos los scripts del personaje <- <-
        -> aqui se sabra si el personaje esta haciendo una animacion o no / todo tipo de flags para animaciones
        -------------------------------
        */

        InputHandler inputHandler;
        Animator anim;
        CameraHolder cameraHolder;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;
        [Header("Player Flags")]
        public bool isSprinting; //bool , true sprint, false no sprint
        public bool isInAir; //bool para saber si esta en el aire
        public bool isGrounded; //bool para saber si esta en el suelo
        public bool canDoCombo;

        private void Awake()
        {
            //cameraHolder = CameraHolder.singleton;
            cameraHolder = FindObjectOfType<CameraHolder>();
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

      
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            inputHandler.TickInput(delta); // -> primeroo se leen los imput que se utilizan luego los movimientos....
            playerLocomotion.HandleRollingAndSprinting(delta); //IMPORTANTE -> SPRINT y JUMP ACA.
            //manejadores/inicializador de movimiento
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        }

        private void FixedUpdate()                    
        {                         
            float delta = Time.fixedDeltaTime;

            if (cameraHolder != null) //si la camara esta inicializada
            {
                cameraHolder.FollowTarget(delta);
                cameraHolder.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

        }

        private void LateUpdate() //IMPORTANTE -> TODO lo relacionado a la camara en el LateUpdate.
        {
            //declaro los botones de movimientos como falso, es decir
            // cuando se punsa el boton es true pero se vuelve falso con esta funcion para solo
            // ser pulsado/hacer la anim 1 vez
            inputHandler.rollflag = false;
            //inputHandler.sprintflag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            
            inputHandler.changeWeapon1_input = false;
            inputHandler.changeWeapon2_input = false;

            if (isInAir)  //si esta en el aire
            {
                playerLocomotion.inAirTime = playerLocomotion.inAirTime + Time.deltaTime;    
            }
            //para el salto
            isInteracting = anim.GetBool("isInteracting");
            playerLocomotion.isJumping = anim.GetBool("isJumping");
            anim.SetBool("isGrounded", isGrounded);
        }
    }


