using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class continuarJuego : MonoBehaviour
{
    public void botonContinuar()
    {
        SceneManager.LoadScene("nivel1");
    }
}
