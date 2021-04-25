using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Play_game : MonoBehaviour
{
    void Update()
    {
        if ( Keyboard.current.anyKey.wasPressedThisFrame){
            SceneManager.LoadScene("arena_1");
        }
    }
}
