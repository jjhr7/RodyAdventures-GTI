using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //
    //clase base para como es la interaccion con los objetos del juego
    //

    public float radius = 0.6f;
    public string interactableText;

    private void OnDrawGizmosSelected() //dibuja un circulo azul alrededor del objeto
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact(PlayerManager playerManager) //los hijos puedes override este metodo para uso concreto
    {
        //Debug.Log("PickUpITtem");
    }

}
