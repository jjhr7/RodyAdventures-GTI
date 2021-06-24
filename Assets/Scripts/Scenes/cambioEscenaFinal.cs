using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class cambioEscenaFinal : MonoBehaviour
{
    public Text time;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerPrefs.SetString("time", time.text);
            SceneManager.LoadScene("final");
        }
    }


}