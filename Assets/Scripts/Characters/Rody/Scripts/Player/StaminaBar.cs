using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxStamina(int maxStamina) //maxima stamina
    {
        slider.maxValue = maxStamina; // se lo ponemos como maximo a la barra
        slider.value = maxStamina;
    }

    public void SetCurrentStamina(int currentStamina) //stamina actual
    {
        slider.value = currentStamina; //se la pones a la barra de stamina
    }
}

