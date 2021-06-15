using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    //clase para la logica de la ventana de equipamiento

    //slots of equipment window
    public bool rightHandSlot01Selected; 
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool consumableItemSlot;

    //array of the equipament slots
    public HandEquipmentSlotUI[] handEquipmentSlotUI;


    private void Start()
    {
        //handEquipmentSlotUI = GetComponentsInChildren<HandEquipmentSlotUI>(); //get the class de cada item del array
    }

    //funcion que carga los items equipados en los slots del equipment Window
    public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
    {
        for (int i = 0; i < handEquipmentSlotUI.Length; i++)
        {
            if (handEquipmentSlotUI[i].rightHandSlot01) //si el item esta en el slot de la derecha
            {
                //add item al right slot sacando el item de player inventory array de los item de la mano derecha
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[0]);
            }
            else if (handEquipmentSlotUI[i].rightHandSlot02) //lo mismo de arriba
            {
                //lo sacamos de la posicion 1 porque sino se solapa con el de arriba
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[1]);
            }
            else if (handEquipmentSlotUI[i].leftHandSLot01) //si son slots de la izquierda
            {
                //add to slot correspondiente
                handEquipmentSlotUI[i].AddFireItem(playerInventory.fireWeaponInRightHandSlots[0]);
            }
            else if (handEquipmentSlotUI[i].leftHandSlot02) //si es el left 2
            {
                //add to slot num 2
                handEquipmentSlotUI[i].AddFireItem(playerInventory.fireWeaponInRightHandSlots[1]);
            }
            else
            {
                //add to slot num 2
                handEquipmentSlotUI[i].AddConsumableItem(playerInventory.currentConsumable);
            }
        }
    }

    //right hand slots
    //metodos que usaran los slots al pulsarlos
    public void SelectedRightHandSlot01()
    {
        rightHandSlot01Selected = true;
    }
    public void SelectedRightHandSlot02()
    {
        rightHandSlot02Selected = true;
    }
    //left hand slots
    public void SelectedLeftHandSlot01()
    {
        leftHandSlot01Selected = true;
    }
    public void SelectedLeftHandSlot02()
    {
        leftHandSlot02Selected = true;
    }
    public void SelectedConsumableItemSlot()
    {
        consumableItemSlot = true;
    }
}
