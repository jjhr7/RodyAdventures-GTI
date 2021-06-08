using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandlerMoto : MonoBehaviour
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
    private float jumpInputTimer;
    public bool jump_Input;//salto fijo
    public bool brak_Input;//frenado


    PlayerControls inputActions; //para configurar los botones en que var tienen que guardarse
    PlayerAttacker playerAttacker; //para hacer ataques
    PlayerInventory playerInventory; //para al atacar usar el arma/utilizarla en la funcion de PlayerAttacker
    PlayerManager playerManager;

    //para el salto
    PlayerLocomotion playerLocomotion;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {

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
            inputActions.MotoControls.Jumping.performed += i => jump_Input = true;
            inputActions.MotoControls.Braking.performed += i => brak_Input = true;
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
        HandleJumpingInput();
        HandleBrakingInput();
    }
    private void MoveInput(float delta) //conf de movimiento
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //Clamp random number between zero and one
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jumpInputTimer += Time.deltaTime;
            if (jumpInputTimer > 0.1)
            {
                jump_Input = false;
                jumpInputTimer = 0;
            }
        }
    }

    private void HandleBrakingInput()
    {
        if (brak_Input)
        {
            rollInputTimer += Time.deltaTime;
            if (rollInputTimer > 0.1)
            {
                brak_Input = false;
                rollInputTimer = 0;
            }
        }
    }

}
