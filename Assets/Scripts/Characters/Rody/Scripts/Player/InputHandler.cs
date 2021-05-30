using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    /*
    -------------------------------
    InputHandler detecta cuando usamos los botones/teclas
    -------------------------------
    */

    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    //botones ataques/defensa...
    public bool b_Input;
    public bool a_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool changeWeapon1_input;
    public bool changeWeapon2_input;
    //Inventario / UI
    public bool inventory_Input;

    public bool inventoryFlag;
    public bool rollflag;
    public bool sprintflag;
    public bool comboFlag;
    public float rollInputTimer; // var que decide si hace roll o sprint.
    public bool jump_Input;//salto fijo
    //var modo enfoque
    public bool lockOnFlag;
    public bool lockOnInput;
    //bools for cambio de enfoque
    public bool right_Stick_Right_Input;
    public bool right_Stick_left_Input;

    PlayerControls inputActions; //para configurar los botones en que var tienen que guardarse
    PlayerAttacker playerAttacker; //para hacer ataques
    PlayerInventory playerInventory; //para al atacar usar el arma/utilizarla en la funcion de PlayerAttacker
    PlayerManager playerManager;
    PlayerStats playerStats;
    CameraHolder cameraHolder;
    UIManager UIManager;

    //para el salto
    PlayerLocomotion playerLocomotion;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();

        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerStats = GetComponent<PlayerStats>();
        cameraHolder = FindObjectOfType<CameraHolder>();
        UIManager = FindObjectOfType<UIManager>();

    }



    public void OnEnable() // OnEnable -> cada vez que se utiliza este script se activa a esta funcion
    {
        //configuracion de inputs (camera/movement)
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            //pra el salto estatico
            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;

            //roll / sprint
            inputActions.PlayerActions.Roll.performed += i => b_Input = true; //cuando se pulsa ese boton
            inputActions.PlayerActions.Roll.canceled += i => b_Input = false; //cambia el bool
            //new input system actions
            inputActions.PlayerActions.RB.performed += i => rb_Input = true; //si se pulsa las teclas asignadas a RB
            inputActions.PlayerActions.RT.performed += i => rt_Input = true; //si se pulsa las teclas asignadas a RT
            //quickslotsUi
            inputActions.ChangeWeapon.ChangeWeapon1.performed += i => changeWeapon1_input = true;
            inputActions.ChangeWeapon.ChangeWeapon2.performed += i => changeWeapon2_input = true;
            //modo enfoque
            inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            //cambio modo enfoque
            inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_left_Input = true;
            inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
            //playeractions -> interactable objects
            inputActions.PlayerActions.A.performed += i => a_Input = true;
            //inventario / UI
            inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
        }

        inputActions.Enable();
    }

    private void OnDisable() // inversa a onEnasble()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta) //funcion que llama a todas las funciones de movimientos
    {
        HandleMoveInput(delta); // conf de botones movimiento
        HandleRollInput(delta); // conf roll/sprinting
        HandleAttackInput(delta); //conf attacks
        HandleQuickSlotInput();
        HandleJumpingInput();//salto estatico
        HandleLockOnInput(); //manejador modo enfoque
        HandleInventoryInput(); //inventory button logic
       
    }
    private void HandleMoveInput(float delta) //conf de movimiento
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //Clamp random number between zero and one
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {


        if (b_Input) //sprint
        {
            rollInputTimer += delta;

            if (playerStats.currentStamina <= 0)
            {
                b_Input = false;
                sprintflag = false;
            }

            if (moveAmount > 0.5f && playerStats.currentStamina > 0)
            {
                sprintflag = true;
            }
        }
        else
        {
            sprintflag = false;

            if (rollInputTimer > 0 && rollInputTimer < 0.5f) //roll
            {
                rollflag = true;
            }

            rollInputTimer = 0;
        }
    }

    private void HandleAttackInput(float delta)
    {
        

        //Rb input maneja los ataques leves con la mano derecha
        if (rb_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
        }
        //RT -> ataques potentes mano derecha
        if (rt_Input)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

    private void HandleQuickSlotInput()
    {
        

        if (changeWeapon1_input)
        {
            playerInventory.ChangeRightWeapon();

        }
        else if (changeWeapon2_input)
        {
            playerInventory.ChangeLeftWeapon();
        }
    }
    //salto estatico
    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            //playerLocomotion.HandleJump();
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            //cameraHolder.ClearLockOnTarget();
            lockOnInput = false;
            cameraHolder.HandleLockOn();
            if(cameraHolder.nearestLookOnTarget != null)
            {
                cameraHolder.currentLockOnTarget = cameraHolder.nearestLookOnTarget; //le indicamos el objetivo
                lockOnFlag = true;
            }

        }
        else if (lockOnInput && lockOnFlag)
        {
            lockOnFlag = false;
            lockOnInput = false;
            cameraHolder.ClearLockOnTarget();
        }

        if(lockOnFlag && right_Stick_left_Input) //si esta en enfoque y le das a la izquierda del pad
        {
            right_Stick_left_Input = false; 
            cameraHolder.HandleLockOn(); //manejador del enfoque
            if (cameraHolder.leftLockTarget != null) //si existe target a la izquierda
            {
                cameraHolder.currentLockOnTarget = cameraHolder.leftLockTarget; //enfocar al nuevo target
            }
        }

        if(lockOnFlag && right_Stick_Right_Input)
        {
            right_Stick_Right_Input = false;
            cameraHolder.HandleLockOn();
            if (cameraHolder.rightLockTarget != null) //si existe target a la derecha
            {
                cameraHolder.currentLockOnTarget = cameraHolder.rightLockTarget; //enfocar al nuevo target
            }
        }

        cameraHolder.SetCameraHeight();
    }

    private void HandleInventoryInput()
    {
        if (inventory_Input) //si se pulsa el boton del inventario / UI
        {
            inventoryFlag = !inventoryFlag; //descativar/activar si se pulsa el boton

            if (inventoryFlag) //si el flag es true
            {
                Time.timeScale = 0; //pausar juego
                UIManager.OpenSelectedWindow();
                UIManager.UpdateUI(); //actualizar los slots del inventario
                //abrimos los windows principales
                UIManager.OpenAllInventoryWindows();
                UIManager.hudWindow.SetActive(false); //cuando abrimos el inventario cerramos el HUD
            }
            else // si inventoryFlag es false, cerrar windows
            {
                Time.timeScale = 1; //renaudar juego
                UIManager.CloseSelectedWindow(); //cerrar menu de seleccion
                UIManager.CloseAllInventoryWindows(); //cerrar inventory window
                UIManager.hudWindow.SetActive(true);//Al cerrar inventario activamos el hud
            }
        }
    }

    public void setFalseInventoryFlag()
    {
        inventoryFlag = false;
    }
}


