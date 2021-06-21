using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
    {
        //PlayerInventory -> inventario para los item/weapons ...

        WeaponSlotManager weaponSlotManager; //control de weapons

        public WeaponItem rightWeapon; //armas R y L
        public WeaponItem leftWeapon;
        //consumible item
        public ConsumableItem currentConsumable;
        public Image consumableSlot;
        public Text consumableText;
        
        //public WeaponItem unarmedWeapon;
        
        public FireWeponItem fireRightWeapon;
        public FireWeponItem fireLeftWeapon;
        
        
        public WeaponItem[] weaponInRightHandSlots = new WeaponItem[1];
        public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[1];
        
        public FireWeponItem[] fireWeaponInRightHandSlots = new FireWeponItem[1];
        public FireWeponItem[] fireWeaponInLeftHandSlots = new FireWeponItem[1];

        
        
        public int currentRightWeaponIndex = 0;
        public int currentLeftWeaponIndex = 0;
        
        public int currentRightFireWeaponIndex = 0;
        public int currentLeftFireWeaponIndex = 0;
        
        public bool isFireWeaponEquiped = false;

        // Lista con las armas del inventario
        public List<WeaponItem> weaponsInventory;
        public List<FireWeponItem> fireWeaponsInventory;
        public List<ConsumableItem> consumableInventory;
        
        //UI Armas
        public GunSheet _gunSheet;
        
        private void Awake() //se llama al cargar la isntancia del script
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();

            try{

                EquipCurrentFireWeapon();
                EquipCurrentWeapon();


            }catch (Exception ex)
            {
                
            }
            
            
        
        }

        private void Start() //se llama a start antes que a los metodos update...
        {
            //cargamos las armas
            rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
            leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];

            initConsumableItemValues(currentConsumable); //iniciamos los valores del item comestible
        }
        
        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
            
            if (currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
            }
            else if(currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
                
            }else if (currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);

            }
            else if(currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] == null)
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }

            if (currentRightWeaponIndex > weaponInRightHandSlots.Length-1 )
            {
                currentRightWeaponIndex = 0;
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            
            isFireWeaponEquiped = false;
            _gunSheet.DisableGunSheet();
            
        }
        
        public void ChangeRightFireWeapon()
        {
            currentRightFireWeaponIndex = currentRightFireWeaponIndex + 1;
            
            if (currentRightFireWeaponIndex == 0 && fireWeaponInRightHandSlots[0] != null)
            {
                fireRightWeapon = fireWeaponInRightHandSlots[currentRightFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInRightHandSlots[0],false);
            }
            else if(currentRightFireWeaponIndex == 0 && fireWeaponInRightHandSlots[0] == null)
            {
                currentRightFireWeaponIndex = currentRightFireWeaponIndex + 1;
                
            }else if (currentRightFireWeaponIndex == 1 && fireWeaponInRightHandSlots[1] != null)
            {
                fireRightWeapon = fireWeaponInRightHandSlots[currentRightFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInRightHandSlots[currentRightFireWeaponIndex], false);

            }
            else if(currentRightFireWeaponIndex == 1 && fireWeaponInRightHandSlots[1] == null)
            {
                currentRightFireWeaponIndex = currentRightFireWeaponIndex + 1;
            }

            if (currentRightFireWeaponIndex > fireWeaponInRightHandSlots.Length-1 )
            {
                currentRightFireWeaponIndex = 0;
                fireRightWeapon = fireWeaponInRightHandSlots[currentRightFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInRightHandSlots[currentRightFireWeaponIndex], false);
            }
            
            isFireWeaponEquiped = true;
            _gunSheet.EnableGunSheet(fireWeaponInRightHandSlots[currentRightFireWeaponIndex]);
            
            
        }
        
        
        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            
            if (currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
            }
            else if(currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
                
            }else if (currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);

            }
            else if(currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] == null)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }

            if (currentLeftWeaponIndex > weaponInLeftHandSlots.Length-1 )
            {
                currentLeftWeaponIndex = 0;
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            isFireWeaponEquiped = false;
        }
        
        public void ChangeLeftFireWeapon()
        {
            currentLeftFireWeaponIndex = currentLeftFireWeaponIndex + 1;
            
            if (currentLeftFireWeaponIndex == 0 && fireWeaponInLeftHandSlots[0] != null)
            {
                fireLeftWeapon = fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex],true);
            }
            else if(currentLeftFireWeaponIndex == 0 && fireWeaponInLeftHandSlots[0] == null)
            {
                currentLeftFireWeaponIndex = currentLeftFireWeaponIndex + 1;
                
            }else if (currentLeftFireWeaponIndex == 1 && fireWeaponInLeftHandSlots[1] != null)
            {
                fireLeftWeapon = fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex], true);

            }
            else if(currentLeftFireWeaponIndex == 1 && fireWeaponInLeftHandSlots[1] == null)
            {
                currentLeftFireWeaponIndex = currentLeftFireWeaponIndex + 1;
            }
           
            if (currentLeftFireWeaponIndex > fireWeaponInLeftHandSlots.Length-1 )
            {
                currentLeftFireWeaponIndex = 0;
                fireLeftWeapon = fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex];
                weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex], true);
            }
            
            isFireWeaponEquiped = true;
        }
        
        public void EquipCurrentWeapon()
        {
            isFireWeaponEquiped = false;
            _gunSheet.DisableGunSheet();
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
            weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
        }
        
        public void EquipCurrentFireWeapon()
        {
            isFireWeaponEquiped = true;
            _gunSheet.EnableGunSheet(fireWeaponInRightHandSlots[currentRightFireWeaponIndex]);
            
            weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInRightHandSlots[currentRightFireWeaponIndex],false);
            weaponSlotManager.LoadFireWeaponOnSlot(fireWeaponInLeftHandSlots[currentLeftFireWeaponIndex],true);
        }
        
        //funcion para cambiar los valores del item consumible
        public void setConsumableItemValues(ConsumableItem consumableItem)
        {
            consumableSlot.sprite = consumableItem.itemIcon;
            consumableText.text = consumableItem.currentItemAmount.ToString();
        }
        public void addConsumableItemValue()
        {
            if (currentConsumable.currentItemAmount < currentConsumable.maxItemAmount)
            {
                currentConsumable.currentItemAmount++;
                setConsumableItemValues(currentConsumable);
            }
        }
        public void initConsumableItemValues(ConsumableItem consumableItem)
        {
            consumableItem.currentItemAmount = consumableItem.maxItemAmount/2;
            setConsumableItemValues(currentConsumable);
        }

}


