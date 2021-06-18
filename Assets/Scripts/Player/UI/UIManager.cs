using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerInventory playerInventory; //para saber el inventario del player
    public EquipmentWindowUI equipmentWindowUI; //equipment window

    [Header("UI Windows")]
    public GameObject hudWindow; //hud window
    public GameObject selectWindow; //ventana que le pasamos por el inspector
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow; //inventoryWindow
    public GameObject GameOptionsWindow; //settigns window

    [Header("Equipment Window Slot Selected")]
    //bools que utilizaremos para ver que slot se elige
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool consumableHandSlotSelected;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab; //slot prefab que vamos a duplicar
    public Transform weaponInventorySlotsParent; //contenedor UI donde estan todos los slots del inventario
    [Header("Fire Weapon Inventory")]
    public GameObject fireWeaponInventorySlotPrefab; //slot prefab que vamos a duplicar
    public Transform fireWeaponInventorySlotsParent; //contenedor UI donde estan todos los slots del inventario
    [Header("Consumable Inventory")]
    public GameObject consumableInventorySlotPrefab; //slot prefab que vamos a duplicar
    public Transform consumableInventorySlotsParent; //contenedor UI donde estan todos los slots del inventario

    WeaponInventorySlot[] weaponInventorySlots; //lista de los slots del inventario
    WeaponInventorySlot[] fireWeaponInventorySlots;
    WeaponInventorySlot[] ConsumableInventorySlots;

    private void Awake()
    {
        //equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
    }
    private void Start()
    {
        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        fireWeaponInventorySlots = fireWeaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        ConsumableInventorySlots = consumableInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory); //le pasamos EquipmentWindowUi el invenario
    }
    public void UpdateUI()
    {
        #region Weapon Inventory Slots
        //mele weapon inventory
        for (int i = 0; i < weaponInventorySlots.Length; i++) //for que recorre el array
        {
            if (i < playerInventory.weaponsInventory.Count) //recorro todos los items del array de playerInventory
            {
                //si hay que a�adir mas slots al inventarios
                if(weaponInventorySlots.Length < playerInventory.weaponsInventory.Count) 
                {
                    //instanciamos el slot 
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    //get los el array de slots completo
                    weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                //add los items a los slots del inventario
                weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            }
            else //si el item no esta / esta vacio limpio ese Slot para que no haya slots vacioss
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
        //fire weapon inventory
        for (int i = 0; i < fireWeaponInventorySlots.Length; i++) //for que recorre el array
        {
            if (i < playerInventory.fireWeaponsInventory.Count) //recorro todos los items del array de playerInventory
            {
                //si hay que a�adir mas slots al inventarios
                if (fireWeaponInventorySlots.Length < playerInventory.fireWeaponsInventory.Count)
                {
                    //instanciamos el slot 
                    Instantiate(fireWeaponInventorySlotPrefab, fireWeaponInventorySlotsParent);
                    //get los el array de slots completo
                    fireWeaponInventorySlots = fireWeaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                //add los items a los slots del inventario
                fireWeaponInventorySlots[i].AddFireWeponItem(playerInventory.fireWeaponsInventory[i]);
            }
            else //si el item no esta / esta vacio limpio ese Slot para que no haya slots vacioss
            {
                fireWeaponInventorySlots[i].ClearFireWeponItemInventorySlot();
            }
        }
        //consumable inventory
        for (int i = 0; i < ConsumableInventorySlots.Length; i++) //for que recorre el array
        {
            if (i < playerInventory.consumableInventory.Count) //recorro todos los items del array de playerInventory
            {
                //si hay que a�adir mas slots al inventarios
                if (ConsumableInventorySlots.Length < playerInventory.consumableInventory.Count)
                {
                    //instanciamos el slot 
                    Instantiate(consumableInventorySlotPrefab, consumableInventorySlotsParent);
                    //get los el array de slots completo
                    ConsumableInventorySlots = consumableInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                //add los items a los slots del inventario
                ConsumableInventorySlots[i].AddConsumableItem(playerInventory.consumableInventory[i]);
            }
            else //si el item no esta / esta vacio limpio ese Slot para que no haya slots vacioss
            {
                ConsumableInventorySlots[i].ClearConsumableItemInventorySlot();
            }
        }
        #endregion
    }

    public void OpenSelectedWindow() //abrir ventana que le pase al onclick de button
    {
        selectWindow.SetActive(true);
    }

    public void CloseSelectedWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows() //cerrar todas las ventanas
    {
        ResetAllSelectedSlots(); //reiniciar bools de los slots seleccionados
        weaponInventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
        GameOptionsWindow.SetActive(false);
    }

    //reinicio de los bool de los slots cuando se cierra
    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Selected = false;
        rightHandSlot02Selected = false;
        leftHandSlot01Selected = false;
        leftHandSlot02Selected = false;
        consumableHandSlotSelected = false;
    }

    //funciones aparte que he hecho
    public void OpenAllInventoryWindows()
    {
        GameOptionsWindow.SetActive(true);
    }

    public void BackToMainMenuScene()
    {
        SceneManager.LoadScene("_MenuRodyAdventures");
    }

    public void ContinueGameButton()
    {
        Time.timeScale = 1; //renaudar juego
        CloseSelectedWindow();
        CloseAllInventoryWindows();
        hudWindow.SetActive(true);
    }

   

}
