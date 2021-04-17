using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruirbala : MonoBehaviour
{

    public float danyo = .3f;

    public float fuerza = 1f;

    private Rigidbody rb;

    double timer = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);
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
        Destroy(gameObject);
    }
}
