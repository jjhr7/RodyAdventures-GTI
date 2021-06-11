using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : MonoBehaviour
{
    
    public Image leftWeaponIcon;
    public Image rightWeaponIcon;
    public GameObject RightQuickSlot;
    public GameObject LeftQuickSlot;
    public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
    {
        
        if (!isLeft)
        {
            if (weapon.itemIcon != null)
            {
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;
            }
            else
            {
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = true;
            }
                
        }
    }
    
    public void UpdateFireWeaponQuickSlotsUI(bool isLeft, FireWeponItem fireWeponItem)
    {
        if (!isLeft)
        {
            if (fireWeponItem.itemIcon != null)
            {
                leftWeaponIcon.sprite = fireWeponItem.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = true;
            }
                
        }
    }

    public void modifyVisibilityWeponSlot( bool newState)
    {
        RightQuickSlot.SetActive(newState);
    }

    public void modifyVisibilityFireWeapon(bool newState)
    {
        LeftQuickSlot.SetActive(newState);
    }
}
