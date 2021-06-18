using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class mapScript : MonoBehaviour
{

    public static bool mapIsOut = false;

    public GameObject minimap;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            if (mapIsOut)
            {
                minimap.SetActive(false);
                mapIsOut = false;
            }
            else
            {
                minimap.SetActive(true);
                mapIsOut = true;

            }

        }
        
    }

}
