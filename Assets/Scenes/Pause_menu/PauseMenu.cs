using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused=false;

    public GameObject pauseMenuUI;

    public GameObject barraMonedas;

    public GameObject barraVida;

    public GameObject canvasLoading;


    private void Start()
    {
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        barraMonedas.SetActive(true);
        barraVida.SetActive(true);
        canvasLoading.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        barraMonedas.SetActive(false);
        barraVida.SetActive(false);
        canvasLoading.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadInventario()
    {
        Debug.Log("Load menu funciona");
        SceneManager.LoadScene("Inventario");
    }

    public void quitMenu()
    {
        Debug.Log("Quit menu funciona");
        SceneManager.LoadScene("Main_menu");
    }
}

