using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreyTest
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

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

        public void TickInput(float delta) 
        {
            MoveInput(delta);
        }
        public void MoveInput(float delta) //conf de movimiento
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //Clamp random number between zero and one
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

    }
}

