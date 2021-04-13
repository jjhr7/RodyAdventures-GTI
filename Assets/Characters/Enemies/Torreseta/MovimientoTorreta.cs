using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTorreta : MonoBehaviour
{


    public Transform target;
    public Transform turret;
    public Transform bullet;
    public Transform bulletSpawn;
    double timer = 0.0;
    public double cadencia = 0.25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider objectTriggered)
    {
        timer += Time.deltaTime;
        if (objectTriggered.transform == target)
        {
            turret.transform.LookAt(target);

            if (timer > cadencia)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                timer = 0.0;

            }

        }
    }
}
