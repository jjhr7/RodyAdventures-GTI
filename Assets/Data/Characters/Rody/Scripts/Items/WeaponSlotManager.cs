using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WeaponSlotManager : MonoBehaviour
    {
        //WeaponSlotManager -> centro de control de armas, aqui se decide que arma va en que lado y que modelos cargar

        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider; //colliders para las manos/armas
        DamageCollider rightHandDamageCollider;
        
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>(); //array for weapon slots
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots) //bucle que determina que arma va en la L o en la R
            {
                if (weaponSlot.isLeftHandSlot) //si el arma es de L
                {
                    leftHandSlot = weaponSlot; //guardar en la var L
                }
                else if(weaponSlot.isRightHandSlot) //si el arma es de R
                {
                    rightHandSlot = weaponSlot; //guardar en la var R
                }
            } 

        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem,bool isLeft) // cargar armas a las manos
        {
            if (isLeft) // si es para la izquierda
            {
                leftHandSlot.LoadWeapomodel(weaponItem); //llamamos a la funcion de la clase WeaponHolderSlot.cs para cargar 
                LoadLeftWeaponDamageCollider(); //metodo que anyade el collider al left hand
                
                #region Handle Left  Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm emty", 0.2f);
                }
                #endregion
            }
            else //si esta en la derecha
            {
                rightHandSlot.LoadWeapomodel(weaponItem); //cargar modelo arma
                LoadRightWeaponDamageCollider();//metodo que anyade el collider al right hand
                #region Handle Right Weapon Idle Animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm emty", 0.2f);
                }
                #endregion
            }
        }

        #region Handle Weapons Damage Collider
            private void LoadLeftWeaponDamageCollider()
            {
                //accedemos a la clase weaponHolderSlot y recogemos el valor de la var currentWeaponModel para almacenarlo
                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            }

            private void LoadRightWeaponDamageCollider()
            {
                //accedemos a la clase weaponHolderSlot y recogemos el valor de la var currentWeaponModel para almacenarlo
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            }

            //activar colliders llamando a la funcion en DamageCollider
            public void OpenRightDamageCollider()
            {
                rightHandDamageCollider.EnableDamageCollider();
            }

            public void OpenLeftDamageCollider()
            {
                leftHandDamageCollider.EnableDamageCollider();
            }

            //descativar colliders llamando a la funcion en DamageCollider.cs
            public void CloseRightDamageCollider()
            {
                rightHandDamageCollider.DisableDamageCollider();
            }

            public void CloseLeftDamageCollider()
            {
                leftHandDamageCollider.DisableDamageCollider();
            }
        #endregion

    }

