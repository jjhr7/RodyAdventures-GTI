using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cerrarCanvas : MonoBehaviour
{
    public static bool canvasIsClosed = false;

    public GameObject canvasLoader;

    public float temporizador = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExampleCoroutine());
        canvasLoader.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(temporizador);
    }


}
