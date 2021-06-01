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
    PlayerStats playerStats;
    public Animator anim;
    InputHandler inputHandler;
    PlayerLocomotion playerLocomotion;
    int vertical;
    int horizontal;
    public bool canRotate;
    //Asignacion para el modo bola
    public GameObject bola;
    public GameObject rody;
    bool sprintt;// variable para guardar la velocidad de sprint de modo normal

    public void Initialize()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        playerStats = GetComponentInParent<PlayerStats>();
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
        sprintt = isSprinting;
        if (isSprinting && playerStats.currentStamina > 0) //cuando esta en sprint y tiene stamina suficiente
        {
            v = 2; //aumenta su velocidad
            h = horizontalMovement;

            //para el modo bola
            bola.gameObject.SetActive(true);

            rody.gameObject.SetActive(false);
        }
        else//si no esta en modo bola
        {
            bola.gameObject.SetActive(false);

            rody.gameObject.SetActive(true);
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
        if (!sprintt)
        { //si esta en modo bola
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }
    }

    public void CanRotate()
    {
        canRotate = true;
    }
    public void StopRotation()
    {
        canRotate = false;
    }

    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
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


