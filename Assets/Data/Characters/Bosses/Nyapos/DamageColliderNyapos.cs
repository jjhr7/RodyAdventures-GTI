using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderNyapos : MonoBehaviour
{
    public Transform player;
    PlayerStats ps;
    bool pegando;
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
                if (ps != null)
                {
                    ps.TakeDamage(damage);
                    pegando = false;
                    haspegado = true;
                }
            }
        }
        else
        {
            col.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        pegando = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        pegando = false;

    }
}
