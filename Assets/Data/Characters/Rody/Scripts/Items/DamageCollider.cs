using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;

        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }

        public void EnableDamageCollider() //activar damage collider
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider() //desactivar damage collider
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision) //cuando hay collision
        {
            if(collision.tag == "Player") //cuando el tag es igual 
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>(); //obtenemos la clase PStats de la collision

                if(playerStats != null) //si tiene la clase playerStats
                {
                    playerStats.TakeDamage(currentWeaponDamage); //hace danyo
                }
            }

            if(collision.tag == "Enemy") //si es enemigo
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>(); //obtener clase EnemyStats del enemigo
                if (enemyStats != null) //si existe
                {
                    enemyStats.TakeDamage(currentWeaponDamage); //hacer danyo
                }
            }
        }
    }


