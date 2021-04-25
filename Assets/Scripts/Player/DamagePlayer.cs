using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class DamagePlayer : MonoBehaviour
    {
        //DamagePlayer -> clase donde se controlara el daño al jugador

        public int damage = 25;

        private void OnTriggerEnter(Collider other) //cuando hay collision
        {
           PlayerStats playerStats= other.GetComponent<PlayerStats>(); //instance del collider para usar los metodos de PStats

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }


