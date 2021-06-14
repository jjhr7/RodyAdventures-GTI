using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barraNyapos : MonoBehaviour
{
    public GameObject Nyapos;

    // Update is called once per frame
    void Update()
    {
        if (Nyapos==null)
        {
            gameObject.SetActive(false);
        }
    }
}
