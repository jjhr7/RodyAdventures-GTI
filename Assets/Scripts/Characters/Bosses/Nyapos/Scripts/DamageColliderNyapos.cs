using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderNyapos : MonoBehaviour
{
    public Transform player;
    PlayerStats ps;
    public bool pegando;
    public int damage;
    public bool haspegado;
    public Collider col;

    private void Start()
    {
        haspegado = false;
        ps = player.GetComponent<PlayerStats>();

    }
    private void Update()
    {
        if (!haspegado)
        {
            if (pegando == true)
            {
                    ps.TakeDamage(damage);
                    pegando = false;
                    haspegado = true;
            }
        }
        else
        {
            col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player") //cuando el tag es igual 
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>(); //obtenemos la clase PStats de la collision

            if (playerStats != null) //si tiene la clase playerStats
            {
                playerStats.TakeDamage(damage); //hace danyo
            }
        }

        if (collision.tag == "Enemy") //si es enemigo
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>(); //obtener clase EnemyStats del enemigo
            if (enemyStats != null) //si existe
            {
                enemyStats.TakeDamage(damage); //hacer danyo
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        pegando = false;

    }
}
