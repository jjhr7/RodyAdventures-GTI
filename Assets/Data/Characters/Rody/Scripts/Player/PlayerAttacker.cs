using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerAttacker : MonoBehaviour
    {

        //PlayerAttacker -> maneja los ataques SOBRE el jugador (light/heavy attacks/sprint attacks)

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void HandleLightAttack(WeaponItem weapon) //ataque ligero
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true); //hacer animacion
        }

        public void HandleHeavyAttack(WeaponItem weapon) //atque modo diablo
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true); //hacer animacion
        }
    }


