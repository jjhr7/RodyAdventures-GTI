using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream

public class cerrarCanvas : MonoBehaviour
{
=======
using UnityEngine.InputSystem;

public class cerrarCanvas : MonoBehaviour
{

>>>>>>> Stashed changes
    public static bool canvasIsClosed = false;

    public GameObject canvasLoader;

<<<<<<< Updated upstream
    public float temporizador = 2.2f;
=======
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        StartCoroutine(ExampleCoroutine());
        canvasLoader.SetActive(false);
=======
        void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (!canvasIsClosed)
                {
                    canvasLoader.SetActive(false);
                }
            }
        }
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream

    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(temporizador);
    }


=======
        
    }
          
  
>>>>>>> Stashed changes
}
