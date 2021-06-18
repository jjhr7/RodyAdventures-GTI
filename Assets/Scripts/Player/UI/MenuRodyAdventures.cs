using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRodyAdventures : MonoBehaviour
{
    public GameObject btnContinuar;
    public GameObject btnAjustes;
    public GameObject btnSalir;

    public void iniciarPartida()
    {
        SceneManager.LoadScene("nivel1");
    }

    public void salirJuego()
    {
        Application.Quit();
    }
}
