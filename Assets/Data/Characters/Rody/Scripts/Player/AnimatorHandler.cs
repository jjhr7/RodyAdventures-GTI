using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AnimatorHandler : MonoBehaviour
    {

        /*
        -------------------------------
        AnimatorHandler maneja la realizacion de las animaciones
        -------------------------------
        */

        PlayerManager playerManager;
        public Animator anim;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");

        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            var v = ClampMovementDir(verticalMovement);
            var h = ClampMovementDir(horizontalMovement);

            if (isSprinting) //cuando es true
            {
                v = 2; //aumenta su velocidad
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }


        private static float ClampMovementDir(float dirMovement)
        {
            float moveDirClamped = 0;
            if (dirMovement > 0 && dirMovement < 0.55f)
            {
                moveDirClamped = 0.5f;
            }
            else if (dirMovement > 0.55f)
            {
                moveDirClamped = 1;
            }
            else if (dirMovement < 0 && dirMovement > -0.55f)
            {
                moveDirClamped = -0.5f;
            }
            else if (dirMovement < -0.55f)
            {
                moveDirClamped = -1;
            }
            else
            {
                moveDirClamped = 0;
            }

            return moveDirClamped;
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting) 
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotation()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;
        }
    }

