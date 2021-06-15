using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WeaponSlotManager : MonoBehaviour
    {
        //WeaponSlotManager -> centro de control de armas, aqui se decide que arma va en que lado y que modelos cargar

        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider; //colliders para las manos/armas
        DamageCollider rightHandDamageCollider;
        
        Animator animator;
        
        public QuickSlotsUI quickSlotsUI;

        PlayerStats playerStats;
        PlayerInventory playerInventory;

        int leftHandDamageColliderDanyo;
        int rightHandDamageColliderDanyo;

    private void Awake()
        {
        Debug.Log("Recargando escena");
        playerStats = GetComponentInParent<PlayerStats>();
        playerInventory = GetComponentInParent<PlayerInventory>();

        animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();

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

    public void LoadBothWeaponsOnSlots()
    {
        if (playerInventory.isFireWeaponEquiped == true)
        {
            LoadFireWeaponOnSlot(playerInventory.fireRightWeapon, false);
            LoadFireWeaponOnSlot(playerInventory.fireLeftWeapon, true);
        }
        else
        {
            LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            LoadWeaponOnSlot(playerInventory.leftWeapon, true);
        }


    }

    private void Update()
    {
        if (rightHandDamageCollider != null)
        {
            if (playerStats != null)
            {
                if (playerStats.FLAGFuego)
                {
                    rightHandDamageCollider.currentWeaponDamage = rightHandDamageColliderDanyo + playerStats.extraFireDamage;
                }
                else
                {
                    rightHandDamageCollider.currentWeaponDamage = rightHandDamageColliderDanyo;
                }
            }
        }

        if (leftHandDamageCollider != null)
        {
            if (playerStats != null)
            {
                if (playerStats.FLAGFuego)
                {
                    leftHandDamageCollider.currentWeaponDamage = leftHandDamageColliderDanyo + playerStats.extraFireDamage;
                }
                else
                {
                    leftHandDamageCollider.currentWeaponDamage = leftHandDamageColliderDanyo;
                }
            }
        }
    }


    public void LoadWeaponOnSlot(WeaponItem weaponItem,bool isLeft) // cargar armas a las manos
        {
            if (isLeft) // si es para la izquierda
            {
                leftHandSlot.LoadWeapomodel(weaponItem); //llamamos a la funcion de la clase WeaponHolderSlot.cs para cargar 
                LoadLeftWeaponDamageCollider(); //metodo que anyade el collider al left hand
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
            if (leftHandDamageCollider != null)
            {
                leftHandDamageColliderDanyo = leftHandDamageCollider.currentWeaponDamage;

            }
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
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                
                rightHandDamageColliderDanyo = rightHandDamageCollider.currentWeaponDamage;
            if (rightHandDamageCollider != null)
            {

                rightHandDamageColliderDanyo = rightHandDamageCollider.currentWeaponDamage;
            }
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
            quickSlotsUI.modifyVisibilityWeponSlot(true);
            
        }
        
        public void LoadFireWeaponOnSlot(FireWeponItem fireWeponItem, bool isLeft)
        {
            if (isLeft) // si es para la izquierda
            {
                leftHandSlot.LoadFireWeapomodel(fireWeponItem); //llamamos a la funcion de la clase WeaponHolderSlot.cs para cargar 
                LoadLeftWeaponDamageCollider(); //metodo que anyade el collider al left hand
                quickSlotsUI.UpdateFireWeaponQuickSlotsUI(true, fireWeponItem);
                #region Handle Left  Weapon Idle Animations
                if (fireWeponItem != null)
                {
                    animator.CrossFade(fireWeponItem.left_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm emty", 0.2f);
                }
                #endregion
            }
            else //si esta en la derecha
            {
                rightHandSlot.LoadFireWeapomodel(fireWeponItem); //cargar modelo arma
                LoadRightWeaponDamageCollider();//metodo que anyade el collider al right hand
                quickSlotsUI.UpdateFireWeaponQuickSlotsUI(false, fireWeponItem);
                #region Handle Right Weapon Idle Animations
                if (fireWeponItem != null)
                {
                Debug.Log(animator.ToString());
                animator.CrossFade(fireWeponItem.right_hand_idle, 0.2f);
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

