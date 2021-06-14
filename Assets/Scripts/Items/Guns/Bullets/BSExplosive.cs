using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSExplosive : MonoBehaviour
{
    public float velocidad = 3f;
    public int bulletDamage = 1;
    public int bulletDamageFireKepot = 2;


    //LifeLine
    public int maxCollisions;
    public float maxLifeTime;

    public void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        maxLifeTime -= Time.deltaTime;
        if(maxLifeTime <= 0) Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Enemy") //si es enemigo
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>(); //obtener clase EnemyStats del enemigo
            if (enemyStats != null) //si existe
            {
                enemyStats.TakeDamage(bulletDamage); //hacer danyo
                Destroy(gameObject);
                //Debug.Log("Bala destruida por colision contra enemigo");
            }
        }

        if (maxCollisions - 1 == 0)
        {
            Destroy(gameObject);
            //Debug.Log("Bala destruida por colision");
        }
        else
        {
            maxCollisions--;
        }

    }
}
