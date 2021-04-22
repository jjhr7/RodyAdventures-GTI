using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class cambiarEscena : MonoBehaviour
{
    public void teleport()
    {

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("arena_nyapos");
        }

    }
}
