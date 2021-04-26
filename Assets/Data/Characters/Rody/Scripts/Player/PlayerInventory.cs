using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class PlayerInventory : MonoBehaviour
    {
        //PlayerInventory -> inventario para los item/weapons ...

        WeaponSlotManager weaponSlotManager; //control de weapons

        public WeaponItem rightWeapon; //armas R y L
        public WeaponItem leftWeapon;

        private void Awake() //se llama al cargar la isntancia del script
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start() //se llama a start antes que a los metodos update...
        {
            //cargamos las armas
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
    }


