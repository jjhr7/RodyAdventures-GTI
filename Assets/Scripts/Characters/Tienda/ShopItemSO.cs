using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public Sprite item;
    public int basecost;
    public WeaponItem weaponItem;
    public FireWeponItem fireWeponItem;

    [Header(" Items Types ")]
    public bool isWeapon;
    public bool isKepotVd;
    public bool isFireWeapon;
}
