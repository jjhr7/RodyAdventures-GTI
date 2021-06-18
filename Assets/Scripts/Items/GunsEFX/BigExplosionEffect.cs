using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigExplosionEffect : MonoBehaviour
{
    private double Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>2) Destroy(gameObject);
    }
}
