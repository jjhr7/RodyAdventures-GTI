using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    //a esta clase le pasaremos un item que representa los slots del inventario

    PlayerInventory playerInventory; //para add//remove items al inventario
    public GameObject rody;
    UIManager uiManager; //control del ui

    public Image icon; //icon del slot del inventario
    WeaponItem item; //item del inventario , en este caso weapon
    FireWeponItem fireItem;
    ConsumableItem consumableItem;
    //unarmed models
    public FireWeponItem fireWeaponUnarmed;
   
    private void Awake()
    {
        //otra manera es hacer publico playerInventory y arrastrarlo desde el instector
        playerInventory = FindObjectOfType<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();
        

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
    //fire weapons methods
    public void AddFireWeponItem(FireWeponItem newItem) //add item to weapon inventory slot
    {
        //sustituimos los icons del arma nueva
        fireItem = newItem;
        icon.sprite = fireItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearFireWeponItemInventorySlot() //limpiamos el slot del inventario
    {
        fireItem = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
    //consumable weapons methods
    public void AddConsumableItem(ConsumableItem newItem) //add item to weapon inventory slot
    {
        //sustituimos los icons del arma nueva
        consumableItem = newItem;
        icon.sprite = consumableItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearConsumableItemInventorySlot() //limpiamos el slot del inventario
    {
        consumableItem = null;
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
            playerInventory.fireWeaponsInventory.Add(playerInventory.fireWeaponInRightHandSlots[0]);
            //si es dual equiparlo en los 2 slots
            if (fireItem.isDual)
            {
                playerInventory.fireWeaponInRightHandSlots[0] = fireItem; //remplazamos los items
                playerInventory.fireWeaponInLeftHandSlots[0] = fireItem; //remplazamos los items
            }
            else
            {
                playerInventory.fireWeaponInRightHandSlots[0] = fireItem; //remplazamos los items
                playerInventory.fireWeaponInLeftHandSlots[0] = fireWeaponUnarmed; //remplazamos los items
            }
            
            playerInventory.fireWeaponsInventory.Remove(fireItem); //delete from weaponsInventory
        }
        else if(uiManager.leftHandSlot02Selected)
        {
            //cogemos el item del slotSelected y lo añadimos a nuestro inventario
            playerInventory.fireWeaponsInventory.Add(playerInventory.fireWeaponInRightHandSlots[1]);
            if (fireItem.isDual)
            {
                playerInventory.fireWeaponInRightHandSlots[1] = fireItem; //remplazamos los items
                playerInventory.fireWeaponInLeftHandSlots[1] = fireItem; //remplazamos los items
            }
            else
            {
                playerInventory.fireWeaponInRightHandSlots[1] = fireItem; //remplazamos los items
                playerInventory.fireWeaponInLeftHandSlots[1] = fireWeaponUnarmed; //remplazamos los items
            }
            playerInventory.fireWeaponsInventory.Remove(fireItem); //delete from weaponsInventory
        }
        else //sino salimos de la funcion
        {
            return;
        }

        
        //add el nuevo arma en la variable rigthweapon
        playerInventory.rightWeapon = playerInventory.weaponInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
        playerInventory.fireRightWeapon = playerInventory.fireWeaponInRightHandSlots[playerInventory.currentRightFireWeaponIndex];
        playerInventory.fireLeftWeapon = playerInventory.fireWeaponInLeftHandSlots[playerInventory.currentLeftFireWeaponIndex];

        //cargamos las armas a rody
        rody.GetComponentInChildren<WeaponSlotManager>().LoadBothWeaponsOnSlots();
        //update ui of the new weapons
        uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlots();
        uiManager.UpdateUI();


    }
    
}
