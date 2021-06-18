using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioEscenaMoto : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            SceneManager.LoadScene("nivel_moto");
        }
    }
}
