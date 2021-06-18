using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour
{

    PlayerStats playerStats;
    InputHandler inputHandler;
    WeaponSlotManager weaponSlotManager;
    //the particles that will play of the current effect that is effecting the player
    //(drinking , estus, poison etc..)
    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel; //item model 
    public int amountToBeHealed;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponentInParent<WeaponSlotManager>();
        inputHandler = GetComponentInParent<InputHandler>();
    }
    public void HealPlayerFromEffect() //sanar al player
    {
        playerStats.TakeHealth(amountToBeHealed); //llamo a una funcion de playerStats para aumentar la vida
        GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform); //add particles
        Destroy(instantiatedFXModel.gameObject); 
        weaponSlotManager.LoadBothWeaponsOnSlots();
        inputHandler.cont = 0;
    }
}
