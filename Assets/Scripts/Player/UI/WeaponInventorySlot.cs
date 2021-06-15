using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    //a esta clase le pasaremos un item que representa los slots del inventario

    PlayerInventory playerInventory; //para add//remove items al inventario
    WeaponSlotManager weaponSlotManager;
    UIManager uiManager; //control del ui

    public Image icon; //icon del slot del inventario
    WeaponItem item; //item del inventario , en este caso weapon
    FireWeponItem fireItem;

    private void Awake()
    {
        //otra manera es hacer publico playerInventory y arrastrarlo desde el instector
        playerInventory = FindObjectOfType<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
    }
    //upload weapon icons method
    public void AddItem(WeaponItem newItem) //add item to weapon inventory slot
    {
        //sustituimos los icons del arma nueva
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot() //limpiamos el slot del inventario
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem() //sustituimos el arma seleccionada por la que tenemos puesto en el slot de rody
    {
        //Remove current item
        if (uiManager.rightHandSlot01Selected)
        {
            //cogemos el item del slotSelected y lo añadimos a nuestro inventario
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlots[0]);
            playerInventory.weaponInRightHandSlots[0] = item; //remplazamos los items
            playerInventory.weaponsInventory.Remove(item); //delete from weaponsInventory
        }
        else if (uiManager.rightHandSlot02Selected)
        {
            //cogemos el item del slotSelected y lo añadimos a nuestro inventario
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlots[1]);
            playerInventory.weaponInRightHandSlots[1] = item; //remplazamos los items
            playerInventory.weaponsInventory.Remove(item); //delete from weaponsInventory
        }
        else if (uiManager.leftHandSlot01Selected)
        {
            //cogemos el item del slotSelected y lo añadimos a nuestro inventario
            playerInventory.fireWeaponsInventory.Add(playerInventory.fireWeaponInLeftHandSlots[0]);
            playerInventory.fireWeaponInLeftHandSlots[0] = fireItem; //remplazamos los items
            playerInventory.fireWeaponsInventory.Remove(fireItem); //delete from weaponsInventory
        }
        else if(uiManager.leftHandSlot02Selected)
        {
            //cogemos el item del slotSelected y lo añadimos a nuestro inventario
            playerInventory.fireWeaponsInventory.Add(playerInventory.fireWeaponInLeftHandSlots[1]);
            playerInventory.fireWeaponInLeftHandSlots[1] = fireItem; //remplazamos los items
            playerInventory.fireWeaponsInventory.Remove(fireItem); //delete from weaponsInventory
        }
        else //sino salimos de la funcion
        {
            return;
        }

        
        //add el nuevo arma en la variable rigthweapon
        playerInventory.rightWeapon = playerInventory.weaponInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
        
        //cargamos las armas a rody
        
        Debug.Log("ASADASDASDADADS:   "+playerInventory.currentRightWeaponIndex+ " "+playerInventory.currentLeftWeaponIndex);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
        //update ui of the new weapons
        uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlots();
        uiManager.UpdateUI();


    }
}
