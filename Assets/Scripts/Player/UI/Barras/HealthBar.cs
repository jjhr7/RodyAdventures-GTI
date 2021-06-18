using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class HealthBar : MonoBehaviour
    {
        //HealthBar -> script donde se maneja todo respecto a la vida actual y el maximo/minimo

        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxHealth) //maxima salud
        {
            slider.maxValue = maxHealth; // se lo ponemos como maximo a la barra
            slider.value = maxHealth; 
        }

        public void SetCurrentHealth(int currentHealth) //salud actual
        {
            slider.value = currentHealth; //se la pones a la barra la salud
        }
    }


