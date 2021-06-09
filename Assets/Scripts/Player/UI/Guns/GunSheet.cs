using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GunSheet : MonoBehaviour
{
    public Image weaponIcon;
    public TextMeshProUGUI bulletsInfo;
    [FormerlySerializedAs("gunSheet")] public GameObject UIcomponent;

    public void EnableGunSheet(FireWeponItem fireWeponItem)
    {
        UIcomponent.SetActive(true);
        
        if (fireWeponItem.itemIcon != null)
        {
            weaponIcon.sprite = fireWeponItem.itemIcon;
            
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
        UIcomponent.SetActive(false);
    }


}
