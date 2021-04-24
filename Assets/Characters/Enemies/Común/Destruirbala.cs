using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class Destruirbala : MonoBehaviour
    {

        public int danyo;

        public float fuerza = 1f;

        private Rigidbody rb;

        double timer = 0.0;


        PlayerStats PlayerStats;
        GameObject[] player;
        private GameObject myplayer;


        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);

            player = GameObject.FindGameObjectsWithTag("Player");
            myplayer = player[0];
            PlayerStats = myplayer.GetComponent<PlayerStats>();

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer > 4)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.collider.tag.Equals("Player"))
            {

                if (PlayerStats != null)
                {
                    PlayerStats.TakeDamage(danyo);
                }

            }

            Destroy(gameObject);


        }
    }

}
