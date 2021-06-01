using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunSheet : MonoBehaviour
{
    public Image weaponIcon;
    public TextMeshPro bulletsInfo;
    public GameObject gunSheet;

    public void EnableGunSheet(FireWeponItem fireWeponItem)
    {
        gunSheet.SetActive(true);
        
        if (fireWeponItem.itemIcon != null)
        {
            //weaponIcon.sprite = fireWeponItem.itemIcon;
            
        }
        else
        {
            weaponIcon.sprite = null;
            weaponIcon.enabled = true;
        }
    }

    public void updateBulletsInfo(string bulletsNewInfo)
    {
        bulletsInfo.text = bulletsNewInfo;
    }

    public void DisableGunSheet()
    {
        gunSheet.SetActive(false);
    }


}
