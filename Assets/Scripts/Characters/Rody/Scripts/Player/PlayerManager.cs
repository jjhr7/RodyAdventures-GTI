using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager //hijo que adquiere los atributos de characterManager
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
    PlayerStats playerStats;
    Animator anim;
    CameraHolder cameraHolder;
    PlayerLocomotion playerLocomotion;
    //UI CLASSES
    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;
    public GameObject shopWindow;

    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting; //bool , true sprint, false no sprint
    public bool isInAir; //bool para saber si esta en el aire
    public bool isGrounded; //bool para saber si esta en el suelo
    public bool canDoCombo;
    public bool entroEnLaTienda;


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
        playerStats = GetComponent<PlayerStats>();
        interactableUI = FindObjectOfType<InteractableUI>();
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
        playerStats.RegenerateStamina(); // regeneracion de stamina
        //interactable objects
        CheckForInteractableObject();

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

    private void LateUpdate() //IMPORTANTE -> el bool de cada boton a false despues de pulsarlo
    {
        //declaro los botones de movimientos como falso, es decir
        // cuando se punsa el boton es true pero se vuelve falso con esta funcion para solo
        // ser pulsado/hacer la anim 1 vez
        inputHandler.rollflag = false;
        //inputHandler.sprintflag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        //inventory / Ui
        inputHandler.inventory_Input = false;

        /*inputHandler.changeWeapon1_input = false;
        inputHandler.changeWeapon2_input = false;*/
        //player actions -> interactable
        inputHandler.a_Input = false;

        if (isInAir)  //si esta en el aire
        {
            playerLocomotion.inAirTime = playerLocomotion.inAirTime + Time.deltaTime;
        }
        //para el salto
        isInteracting = anim.GetBool("isInteracting");
        playerLocomotion.isJumping = anim.GetBool("isJumping");
        anim.SetBool("isGrounded", isGrounded);
    }

    public void CheckForInteractableObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 0.8f))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if(interactableObject != null)
                {
                    if (itemInteractableGameObject.activeSelf.Equals(true)) //si ya hay un pop activo
                    {
                        itemInteractableGameObject.SetActive(false); //lo desactivamos para activar otro
                    }
                    string interactableText = interactableObject.interactableText;
                    //set the ui text to the interactable object
                    interactableUI.interactableText.text = interactableText; //set text
                    interactableUIGameObject.SetActive(true); //activamos pop up

                    if (inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }

                }
            }else
                if (hit.collider.tag == "Shop")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject != null)
                {
                    if (itemInteractableGameObject.activeSelf.Equals(true)) //si ya hay un pop activo
                    {
                        itemInteractableGameObject.SetActive(false); //lo desactivamos para activar otro
                    }
                    string interactableText = interactableObject.interactableText;
                    //set the ui text to the interactable object
                    interactableUI.interactableText.text = interactableText; //set text
                    interactableUIGameObject.SetActive(true); //activamos pop up

                    if (inputHandler.shop_Input)
                    {
                        entroEnLaTienda = true;
                        Time.timeScale = 0;
                        shopWindow.SetActive(true);
                    }
                    else
                    {
                        Time.timeScale = 1;
                        
                    }

                }
            }
        }
        else //cuando salimos de la zona
        {
            if (interactableUIGameObject != null) // si no da dengue
            {
                interactableUIGameObject.SetActive(false); //desactivamos pop up
            }

            if (itemInteractableGameObject != null && inputHandler.a_Input)
            {
                itemInteractableGameObject.SetActive(false);
            }
        }
    }
}



