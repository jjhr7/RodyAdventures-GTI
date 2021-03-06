using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerAttacker : MonoBehaviour
    {

        //PlayerAttacker -> maneja los ataques SOBRE el jugador (light/heavy attacks/sprint attacks)

        AnimatorHandler animatorHandler;
        private InputHandler inputHandler;
        public string lastAttack;
        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
        }
        
        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                
                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }   
            }
            
        }

        public void HandleLightAttack(WeaponItem weapon) //ataque ligero
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true); //hacer animacion
            lastAttack = weapon.OH_Light_Attack_1;
        }

        public void HandleHeavyAttack(WeaponItem weapon) //atque modo diablo
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true); //hacer animacion
            lastAttack = weapon.OH_Light_Attack_1;
        }
    }


