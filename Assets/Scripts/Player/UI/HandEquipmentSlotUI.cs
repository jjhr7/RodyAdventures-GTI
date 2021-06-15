using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
    // CLASE para los button slots del equipment window

    UIManager uIManager; //para hacer get a los bool de "Header("Equipment Window Slot Selected")"
    public Image icon;
    WeaponItem weapon;
    FireWeponItem fireWeapon;

    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool leftHandSLot01;
    public bool leftHandSlot02;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }
    //weaponItem methods
    public void AddItem(WeaponItem newWeapon)
    {
        weapon = newWeapon;
        try
        {

            icon.sprite = weapon.itemIcon;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    //fire weapon item methods
    public void AddFireItem(FireWeponItem newWeapon)
    {
        fireWeapon = newWeapon;
        try
        {

            icon.sprite = fireWeapon.itemIcon;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        icon.enabled = true;
        gameObject.SetActive(true);
    }
    public void ClearFireItem()
    {
        fireWeapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    //metodo que pone a true los bool del uiManager si se selecciona un slot de equipment window
    public void SelectThisSlot()
    {
        if (rightHandSlot01)
        {
            uIManager.rightHandSlot01Selected = true;
        }
        else if (rightHandSlot02)
        {
            uIManager.rightHandSlot02Selected = true;
        }
        else if (leftHandSLot01)
        {
            uIManager.leftHandSlot01Selected = true;
        }
        else 
        {
            uIManager.leftHandSlot02Selected = true;
        }
    }
}
