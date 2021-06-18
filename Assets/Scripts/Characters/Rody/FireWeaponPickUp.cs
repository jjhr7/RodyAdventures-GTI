using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireWeaponPickUp : Interactable
{
    // Start is called before the first frame update
    public FireWeponItem weapon;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        PickUpItem(playerManager); //le paso la clase a PickUpItem
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        //clases necesariaas
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        AnimatorHandler animatorHandler;
        //get las clases de nuestro player usando el player manager para obtenerlo
        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();  //inChildren porque se encuentra en el modelo de Rody

        playerLocomotion.rigidbody.velocity = Vector3.zero; //el player deja de moverse al recoger un item
        //animatorHandler.PlayTargetAnimation("Pick Up Item", true); //Plas the animation of looting the item
        playerInventory.fireWeaponsInventory.Add(weapon); //añadimos el arma al inventario
        //le ponemos el nombre al item pop up
        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
        //ponemos imagen
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
        //lo activamos
        playerManager.itemInteractableGameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager pm = other.GetComponent<PlayerManager>();
        if (pm != null)
        {
            base.Interact(pm);

            PickUpItem(pm); //le paso la clase a PickUpItem
        }
    }
}
