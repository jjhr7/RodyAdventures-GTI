using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStats : MonoBehaviour
{
    public int numeroMaximoLlaves;
    private int numLlaves;
    public Transform pD;
    public Transform pI;
    public Vector3 direccion;
    public Vector3 direccion2;


    private void Start()
    {
        numLlaves = 0;
    }

    private void Update()
    {
        if (numLlaves == numeroMaximoLlaves)
        {
            if (Mathf.Round(pI.localEulerAngles.y)!=80f)
            {
                pI.Rotate(direccion * 0.5f);
                pI.position += pI.right* 1 * Time.deltaTime/4;
            }
            if (Mathf.Round(pD.localEulerAngles.y) != 280f)
            {
                pD.Rotate(direccion2 * 0.5f);
                pD.position += pD.right * -1 * Time.deltaTime/4;

            }
        }
    }

    public void addKey()
    {
        numLlaves++;
    }

}
