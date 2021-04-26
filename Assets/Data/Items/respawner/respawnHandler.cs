using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPosition;



    private void Update()
    {
        Debug.Log(player.position.y - transform.position.y);
        if(player.position.y-transform.position.y <14)
        {
            player.transform.position = respawnPosition.transform.position;
        }
    }

}
