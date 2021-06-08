using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Fire Weapon Item")]
public class FireWeponItem : Item
{
    public GameObject modelPrefab;
    
    [Header("Idle Animations")] 
    public string right_hand_idle;
    public string left_hand_idle;
}
