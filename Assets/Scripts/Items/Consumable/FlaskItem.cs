using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/KepotVerde")]
public class FlaskItem : ConsumableItem //herencia de la clase ConsumableItem
{
    [Header("Flask Type")] //tipo de consumibles
    public bool estusFlask;
    public bool ashenFlask;

    [Header("Recovery Amount")] //controlador de ayuda del consumible
    public int healthRecoverAmount;
    public int focusPointsRecoverAmount;

    [Header("Recovery FX")] //fx del consumible
    public GameObject recoveryFX;

    public override void AttemptToConsumeItem(AnimatorHandler playerAnimatorManager, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);
        GameObject flask = Instantiate(itemModel, weaponSlotManager.leftHandSlot.transform); //instanciamos el modelo del item en la mano derecha
        playerEffectsManager.currentParticleFX = recoveryFX; //le pasamos el fx 
        playerEffectsManager.amountToBeHealed = healthRecoverAmount; //le pasamos la vida que le tiene que sumar
        playerEffectsManager.instantiatedFXModel = flask;
        weaponSlotManager.leftHandSlot.UnloadWeapon(); //ocultamos arma en la mano para poner el item consumible
        
    }
}
