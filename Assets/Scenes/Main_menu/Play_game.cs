using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Play_game : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown){
            SceneManager.LoadScene("prueba_main");
        }
    }
}
