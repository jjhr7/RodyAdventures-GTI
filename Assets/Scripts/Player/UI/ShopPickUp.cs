using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickUp : Interactable
{
    
    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        openShop(playerManager);
    }

    private void openShop(PlayerManager playerManager)
    {
        
    }
}
