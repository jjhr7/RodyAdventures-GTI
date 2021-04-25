using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EspadaOgro : MovimientoOgro
    {
        PlayerStats ps;
        bool pegando;
        private void Start()
        {
            ps = player.GetComponent<PlayerStats>();
        }
        private void Update()
        {
            if (pegando == true)
            {
                if (ps != null)
                {
                    ps.TakeDamage(damage);
                    pegando = false;
                }
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

}
