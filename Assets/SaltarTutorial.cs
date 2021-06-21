using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltarTutorial : MonoBehaviour
{
    PlayerStats PlayerStats;
    PlayerLocomotion playerLocomotion;
    private float ms;
    PlayerManager playerManager;
    GameObject[] player;
    private GameObject myplayer;
    public int fase;
    public GameObject tutorial;
    public float Rs { get; private set; }
    public tutorialHandler tutHandler;
    private void Start()
    {
        if (tutorial == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            myplayer = player[0];
            playerLocomotion = myplayer.GetComponent<PlayerLocomotion>();
            ms = playerLocomotion.movementSpeed;
            Rs = playerLocomotion.sprintSpeed;
            tutHandler = tutorial.GetComponent<tutorialHandler>();
        }
    }
    public void SaltarTut()
    {
        if (tutorial != null)
        {
            if (fase < 2)
            {
                playerLocomotion.movementSpeed = tutHandler.ms;
                playerLocomotion.sprintSpeed = tutHandler.Rs;
                tutHandler.isJumping = true;
                tutHandler.cajasmonedas.SetActive(true);
                tutHandler.subtitles.SetActive(false);
                tutHandler.audioSource.Stop();
                tutorial.SetActive(false);
                gameObject.SetActive(false);
            }
            else if (fase < 3)
            {
                tutHandler.isJumping = true;
                tutHandler.cajasmonedas.SetActive(true);
                tutHandler.subtitles.SetActive(false);
                tutHandler.audioSource.Stop();
                tutorial.SetActive(false);
                gameObject.SetActive(false);
            } else
            {
                tutHandler.subtitles.SetActive(false);
                tutHandler.audioSource.Stop();
                tutorial.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
}
