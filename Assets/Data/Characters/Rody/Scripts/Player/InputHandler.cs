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
        public bool rb_Input;
        public bool rt_Input;
        public bool changeWeapon1_input;
        public bool changeWeapon2_input;

        public bool rollflag;
        public bool sprintflag;
        public bool comboFlag;
        public float rollInputTimer; // var que decide si hace roll o sprint.
        

        PlayerControls inputActions; //para configurar los botones en que var tienen que guardarse
        PlayerAttacker playerAttacker; //para hacer ataques
        PlayerInventory playerInventory; //para al atacar usar el arma/utilizarla en la funcion de PlayerAttacker
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
        }



        public void OnEnable() // OnEnable -> cada vez que se utiliza este script se activa a esta funcion
        {
            //configuracion de inputs (camera/movement)
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>(); 
            }

            inputActions.Enable();
        }

        private void OnDisable() // inversa a onEnasble()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta) //funcion que llama a todas las funciones de movimientos
        {
            MoveInput(delta); // conf de botones movimiento
            HandleRollInput(delta); // conf roll/sprinting
            HandleAttackInput(delta); //conf attacks
            HandleQuickSlotInput();
        }
        private void MoveInput(float delta) //conf de movimiento
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //Clamp random number between zero and one
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            sprintflag = b_Input;

            if (b_Input) //sprint
            {
                rollInputTimer += delta;
            }
            else
            {
                if(rollInputTimer > 0 && rollInputTimer < 0.5f) //roll
                {
                    sprintflag = false;
                    rollflag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            //new input system actions
            inputActions.PlayerActions.RB.performed += i => rb_Input = true; //si se pulsa las teclas asignadas a RB
            inputActions.PlayerActions.RT.performed += i => rt_Input = true; //si se pulsa las teclas asignadas a RT

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
                    if(playerManager.isInteracting)
                        return;
                    
                    if(playerManager.canDoCombo)
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
            inputActions.ChangeWeapon.ChangeWeapon1.performed += i => changeWeapon1_input = true;
            inputActions.ChangeWeapon.ChangeWeapon2.performed += i => changeWeapon2_input = true;
            
            if (changeWeapon1_input)
            {
                playerInventory.ChangeRightWeapon();

            }else if (changeWeapon2_input)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }
    }


