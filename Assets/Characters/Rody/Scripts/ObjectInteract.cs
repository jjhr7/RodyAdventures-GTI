using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    Inventario inventario;
    

    // Start is called before the first frame update
    void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inventario.Cantidad = inventario.Cantidad + 1;
            
            Destroy(gameObject);
        }
        
    }
}
