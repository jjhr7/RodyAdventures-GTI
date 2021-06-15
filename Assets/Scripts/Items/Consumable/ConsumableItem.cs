using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    public virtual void AttemptToConsumeItem(AnimatorHandler playerAnimatorManager, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        if(currentItemAmount > 0 && maxItemAmount >= currentItemAmount) //si tiene items que consumir
        {
            //hacer animacion
            playerAnimatorManager.PlayTargetAnimation(consumeAnimation, isInteracting);
            currentItemAmount= currentItemAmount-1;
        }
        else
        {
            //hacer animacion de que no tiene mas consumibles , deteniendo al player un instante
            //playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}
