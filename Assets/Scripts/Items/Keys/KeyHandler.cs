using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    DoorStats doorStats;
    GameObject[] door;
    private GameObject myDoor;
    public GameObject parent;
    void Start()
    {
        door = GameObject.FindGameObjectsWithTag("Puerta");
        myDoor = door[0];
        doorStats = myDoor.GetComponent<DoorStats>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destruímos si toca al jugador
        if (collision.collider.tag.Equals("Player"))
        {
            doorStats.addKey();
            Destroy(parent);
        }
    }
}
